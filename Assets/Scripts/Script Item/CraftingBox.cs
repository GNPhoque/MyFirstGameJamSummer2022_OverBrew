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
    private Item _craftingResult;
    private RecipeScriptableObject _matchedRecipe;

    private float _craftingRealTime;
    private bool _crafting;

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
        //check si on porte un ingredient
        if (GameManager.instance.CarriedItem == null)
        {
            Debug.Log("Pas d'ingredient");
        }
        //check si l'object porter est un ingredient, si il n'est pas nul , et si la table n'est pas en train de craft
        if (GameManager.instance.CarriedItem != null && GameManager.instance.CarriedItem is Ingredient && !_crafting)
        {
            Ingredient carriedIngredient = (Ingredient)GameManager.instance.CarriedItem;

            //chack si l'objet n'est pas deja dans la table
            if (currentIngredients.Contains(carriedIngredient) || _craftingResult !=null)
            {
                Debug.Log("Objet deja dedans");
                return;
            }

            else
            {
                currentIngredients.Add(carriedIngredient);
                GameManager.instance.CarriedItem = null;
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

        //check si il y a une potion a prendre et si le joueur n'a pas d'objet dans les mains
        if (GameManager.instance.CarriedItem == null && _craftingResult != null)
        {
            GameManager.instance.CarriedItem = _craftingResult;
            _craftingResult = null;

        }

   
    }

    private void Update()
    {
        if (_crafting)
        {
            CraftingTimeStanding();

            if (_craftingRealTime <= _craftingTime)
            {
                _craftingRealTime -= Time.deltaTime;
            }

            if (_craftingRealTime <= 0)
            {
                _crafting = false;
                currentIngredients.Clear();
                _craftingResult = _matchedRecipe.result;
                _matchedRecipe = null;
                _craftingRealTime = _craftingTimeStandingStill + _craftingTime;
            }
        }
    }

    private void CraftingTimeStanding()
    {
        if (playerInputActions.Player.Action1.ReadValue<float>() > 0)
        {
            _craftingRealTime -= Time.deltaTime;
        }
    }

}
