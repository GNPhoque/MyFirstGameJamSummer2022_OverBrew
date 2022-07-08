using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingBox : MonoBehaviour, IInteractable
{
    [SerializeField] private CraftingTableType _tableType;
    [SerializeField] private Ingredient _result;
    [SerializeField] private float _craftingTime;
    private float _craftingRealTime;
    private bool _crafting;

    [SerializeField] public Ingredient currentIngredient;

    PlayerInputActions playerInputActions;
    public bool Crafting { get => _crafting; set => _crafting = value; }

    private void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        _crafting = false;
        _craftingRealTime = _craftingTime;
    }


    public void Use()
    {
        if (GameManager.instance.Ingredient == Ingredient.None)
        {
            Debug.Log("Pas d'ingredient");
        }

        if (GameManager.instance.Ingredient != Ingredient.None)
        {
            //check si l'ingredient est compatible avec la table de craft et demarrer le timer sauf pour le grinder
            //et dans ce timer verifier une fois le timer terminé quel est le resultat de la transformation
            //il peut recupéré l'ingredients
            //et que l'action est finie alors resultat bouillie;
            if (_tableType == CraftingTableType.Mortar)
            {
                if (GameManager.instance.Ingredient == Ingredient.Bone)
                {
                    _crafting = true;
                   
                }
            }

            //transformation de l'item

            //currentIngredient = GameManager.instance.Ingredient;
        }

        if (GameManager.instance.Ingredient != Ingredient.None && currentIngredient == Ingredient.Bone)
        {
            //transformation de l'item avec timer
            //currentIngredient = Ingredient.HealingPotion;
        }

        if (GameManager.instance.Ingredient == Ingredient.None && currentIngredient == Ingredient.HealingPotion)
        {
            GameManager.instance.Ingredient = Ingredient.HealingPotion;
        }
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
            GameManager.instance.Ingredient = _result;
            _craftingRealTime =_craftingTime ;
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
