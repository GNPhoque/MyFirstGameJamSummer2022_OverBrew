using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RecipeEntryScript : MonoBehaviour
{
    [SerializeField]
    private Image _itemSprite;
    [SerializeField]
    private Image _additionSprite;
    [SerializeField]
    private Image _equalSprite;

    private RecipeScriptableObject _recipe;

    public RecipeScriptableObject Recipe { set => _recipe = value; }

    private void Start()
    {
        Transform transform = this.transform;
        int ingredientAmount = _recipe.ingredients.Count;

        foreach (Ingredient ingredient in _recipe.ingredients)
        {
            Image ingredientSprite = Instantiate(_itemSprite, transform);
            ingredientSprite.sprite = ingredient.itemSprite;

            ingredientAmount--;
            if (ingredientAmount > 0)
            {
                Instantiate(_additionSprite, transform);
            }
            else
            {
                Instantiate(_equalSprite, transform);
            }
        }

        Image result = Instantiate(_itemSprite, transform);
        result.sprite = _recipe.result.itemSprite;
    }
}
