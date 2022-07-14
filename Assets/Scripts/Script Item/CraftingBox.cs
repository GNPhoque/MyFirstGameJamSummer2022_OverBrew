using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CraftingBox : MonoBehaviour, IInteractable
{
    [SerializeField] private float _craftingTimeStandingStill;
    [SerializeField] private float _craftingTime;
    [SerializeField] private RecipeScriptableObject[] craftingRecipeArray;
    [SerializeField] private List<Ingredient> currentIngredients;
    [SerializeField] private Transform[] _ingredientSlots;
    [SerializeField] private bool acceptOnlyBaseIngredient;
    private Item _craftingResult;
    private RecipeScriptableObject _matchedRecipe;

    private float _craftingRealTime;
    private bool _crafting;
    private bool _boxInUse;

    PlayerInputActions playerInputActions;

    public bool Crafting { get => _crafting; set => _crafting = value; }

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
                AddItemInSlot(carriedIngredient, false);
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
            _craftingResult.itemTransform.parent = HealerController.instance.transform;
            _craftingResult.itemTransform.localPosition = Vector3.zero;
            HealerController.instance.CarriedItem = _craftingResult;
            _craftingResult = null;

        }

   
    }

    public void Release() {
        _boxInUse = false;
    }

    private void AddItemInSlot(Item item, bool isCraftingResult) {        
        for (int i = 0; i < _ingredientSlots.Length; i++)
        {
            if (_ingredientSlots[i].childCount == 0) {
                // add game object in slot ingredient
                if (item.itemTransform != null) {
                    item.itemTransform.parent = _ingredientSlots[i];
                    item.itemTransform.localPosition = Vector3.zero;
                } else {
                    GameObject ingredientGo = Instantiate(new GameObject(), _ingredientSlots[i]);
                    SpriteRenderer ingredientSpriteRenderer = ingredientGo.AddComponent<SpriteRenderer>();
                    ingredientSpriteRenderer.sprite = item.itemSprite;
                    ingredientSpriteRenderer.sortingOrder = 1;
                    
                    item.itemTransform = ingredientGo.transform;
                }

                // add ingredient in list or Ingredient/potion as crafting result
                if (!isCraftingResult) {
                    currentIngredients.Add((Ingredient)item);
                } else {
                    _craftingResult = item;
                }
                HealerController.instance.CarriedItem = null;
                break;
            }
        }
    }


    public void Clean() {
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
            if (_boxInUse) _craftingRealTime -= Time.deltaTime;

            if (_craftingRealTime <= _craftingTime)
            {
                _craftingRealTime -= Time.deltaTime;
            }

            if (_craftingRealTime <= 0)
            {
                Clean();
                AddItemInSlot(_matchedRecipe.result, true);
            }
        }
    }

}
