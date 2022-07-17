using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class CraftingBox : MonoBehaviour, IInteractable
{
    [SerializeField] private CraftingBoxType _craftingBoxType;
    [SerializeField] private float _craftingTimeStandingStill;
    [SerializeField] private float _craftingTime;
    [SerializeField] private RecipeScriptableObject[] craftingRecipeArray;
    [SerializeField] private List<Ingredient> currentIngredients;
    [SerializeField] private Transform[] _ingredientSlots;
    [SerializeField] private Transform _craftingResultSlot;
    [SerializeField] private GameObject _ingredientGraphicPrefab;
    [SerializeField] private GameObject _craftProgressBar;
    [SerializeField] private bool acceptOnlyBaseIngredient;
    private Item _craftingResult;
    private RecipeScriptableObject _matchedRecipe;

    private float _craftingRealTime;
    private bool _crafting;
    private bool _boxInUse;

    PlayerInputActions playerInputActions;

    public bool Crafting { get => _crafting; set => _crafting = value; }

    SoundManager soundManager;

    void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        currentIngredients = new List<Ingredient>();
        _crafting = false;
        _craftingRealTime = _craftingTimeStandingStill + _craftingTime;
    }

    public void Use()
    {
        _boxInUse = true;
        //check si on porte un ingredient
        if (HealerController.instance.CarriedItem == null)
        {
            Debug.Log("Pas d'ingredient");
        }
        //check si l'object porter est un ingredient, si il n'est pas nul , et si la table n'est pas en train de craft
        if (HealerController.instance.CarriedItem != null && HealerController.instance.CarriedItem is Ingredient && !_crafting)
        {
            if (acceptOnlyBaseIngredient && !(HealerController.instance.CarriedItem is BaseIngredient)) return;
            Ingredient carriedIngredient = (Ingredient)HealerController.instance.CarriedItem;

            //chack si l'objet n'est pas deja dans la table ou s'il y a resultat de craft
            if (currentIngredients.Contains(carriedIngredient) || _craftingResult !=null)
            {
                Debug.Log("Objet deja dedans");
                return;
            }

            else
            {
                AddItemInSlot(carriedIngredient);
                soundManager.SoundWaterPlouf(_craftingBoxType);
            }

            //check si la recette est valide apres avoir mis l'ingredient qu'il avait dans les mains

            foreach (RecipeScriptableObject recipe in craftingRecipeArray)
            {

                if (currentIngredients != null && recipe != null)
                {
                    List<Ingredient> currentIngredientRecipe = recipe.ingredients;
                    if (currentIngredients.Count == currentIngredientRecipe.Count)
                    {
                        int count = 0;
                        for (int i = 0; i < currentIngredients.Count; i++)
                        {
                            for (int j = 0; j < currentIngredientRecipe.Count; j++)
                            {
                                if(currentIngredients[i] == currentIngredientRecipe[j])
                                {
                                    count++;
                                    break;
                                }
                            }
                        }
                        
                        if (count == currentIngredientRecipe.Count)
                        {
                            _crafting = true;
                            _matchedRecipe = recipe;
                            soundManager.SoundRecipeCrafting(_craftingBoxType);
                            break;
                        }
                    }
                    else
                    {
                        Debug.Log("Pas de recette avec ces �l�ments");
                    }
                }
            }
        }

        //check si il y a une potion/ingredient a prendre et si le joueur n'a pas d'objet dans les mains
        if (HealerController.instance.CarriedItem == null && _craftingResult != null)
        {
            //HealerController.instance.TakeItem(_craftingResult);
            _craftingResult.itemTransform.parent = HealerController.instance.transform;
            _craftingResult.itemTransform.localPosition = Vector3.zero;
            HealerController.instance.CarriedItem = _craftingResult;
            _craftingResult = null;

        }

   
    }

    public void Release() {
        _boxInUse = false;
    }

    private void AddItemInSlot(Ingredient ingredient) {        
        for (int i = 0; i < _ingredientSlots.Length; i++)
        {
            if (_ingredientSlots[i].childCount == 0) {
                // add game object in slot ingredient
                ingredient.itemTransform.parent = _ingredientSlots[i];
                ingredient.itemTransform.localPosition = Vector3.zero;
                currentIngredients.Add((Ingredient)ingredient);
                HealerController.instance.CarriedItem = null;
                break;
            }
        }
    }

    private void AddResultInSlot(Item result) {
        GameObject ingredientGraphic = Instantiate(_ingredientGraphicPrefab, _craftingResultSlot);
        SpriteRenderer ingredientSpriteRenderer = ingredientGraphic.GetComponent<SpriteRenderer>();
        ingredientSpriteRenderer.sprite = result.itemSprite;
        /*ingredientSpriteRenderer.sortingOrder = 1;
        ingredientGraphic.transform.localScale = new Vector3(2,2,1);*/
        soundManager.SoundRecipeCrafted(_craftingBoxType);
        result.itemTransform = ingredientGraphic.transform;
        _craftingResult = result;
    }


    public void Clean() {
            _craftProgressBar.SetActive(false);
            currentIngredients.Clear();
            _craftingResult = null;
            _crafting = false;
            //_matchedRecipe = null;
            _craftingRealTime = _craftingTimeStandingStill + _craftingTime;

            for (int i = 0; i < _ingredientSlots.Length; i++)
            {
                foreach (Transform child in _ingredientSlots[i])
                {
                    Destroy(child.gameObject);
                }
            }
    }

    private void Update()
    {
        if (_crafting)
        {

            if (!_craftProgressBar.activeSelf) _craftProgressBar.SetActive(true);
            if (_boxInUse) _craftingRealTime -= Time.deltaTime;

            if (_craftingRealTime <= _craftingTime)
            {
                _craftingRealTime -= Time.deltaTime;
            }

            _craftProgressBar.transform.GetChild(0).GetComponent<Image>().fillAmount = _craftingRealTime;

            if (_craftingRealTime <= 0)
            {
                Clean();
                AddResultInSlot(_matchedRecipe.result);
            }
        }
    }

}
