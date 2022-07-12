using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingBox : MonoBehaviour, IInteractable
{ 
    [SerializeField] private float _craftingTimeStandingStill;
    [SerializeField] private float _craftingTime;
    private float _craftingRealTime;
    private bool _crafting;
    [SerializeField] private RecipeScriptableObject craftingRecipe;

    [SerializeField] private List<Ingredient> currentIngredients;

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
        if (GameManager.instance.Ingredient == Ingredient.None)
        {
            Debug.Log("Pas d'ingredient");
        }

        if (GameManager.instance.Ingredient != Ingredient.None)
        {

            if (currentIngredients.Contains(GameManager.instance.Ingredient))
            {
                Debug.Log("Objet deja dedans");
            }
            else
            {
                currentIngredients.Add(GameManager.instance.Ingredient);
                GameManager.instance.Ingredient = Ingredient.None;
            }

            //check si l'ingredient est compatible avec la table de craft et demarrer le timer sauf pour le grinder
           

            //et dans ce timer verifier une fois le timer terminé quel est le resultat de la transformation
            //il peut recupéré l'ingredients
            //et que l'action est finie alors resultat bouillie;


            
        }
        if(GameManager.instance.Ingredient == Ingredient.None)
        {
            if(currentIngredients.Count == craftingRecipe.ingredients.Count)
            {
                foreach (Ingredient ingredient in currentIngredients)
                {
                    if (craftingRecipe.ingredients.Contains(ingredient))
                    {
                        _crafting = true;
                    }
                }
            }
            else
            {
                Debug.Log("Pas de recette avec ces éléments");
            }
        }


        if (GameManager.instance.Ingredient == Ingredient.None && currentIngredients[0] != Ingredient.None)
        {
            GameManager.instance.Ingredient = currentIngredients[0];

        }


        /*if (GameManager.instance.Ingredient == Ingredient.None && currentIngredient == Ingredient.HealingPotion)
        {
            GameManager.instance.Ingredient = Ingredient.HealingPotion;
        }*/
    }
    ///si ingredient. == null alors rien
    ///si ingredient. ... != null alors transformation
    ///si ingredient. ..!= null && craftingbox.ingredient != null alors tranformatioon en un item
    ///si transformation fini alors on recupere l'item
    private void Update()
    {
        if (_crafting)
        {
            CraftingTime();
        }
        if (_craftingRealTime <= 0)
        {
            _crafting = false;
            currentIngredients.Clear();
            currentIngredients.Add(craftingRecipe.result);
            _craftingRealTime = _craftingTimeStandingStill + _craftingTime;
        }
    }

    private void CraftingTime()
    {
        if (playerInputActions.Player.Action1.ReadValue<float>() > 0)
        {
            _craftingRealTime -= Time.deltaTime;
        }
    }
}
