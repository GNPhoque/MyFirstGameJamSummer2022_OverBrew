using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBox : MonoBehaviour, IInteractable
{
	[SerializeField]
	Ingredient ingredient;
	[SerializeField] SpriteRenderer _ingredientSpriteRenderer;

	void Awake() {
		if (ingredient?.itemSprite != null)
		{
			_ingredientSpriteRenderer.enabled = true;
			_ingredientSpriteRenderer.sprite = ingredient.itemSprite;
		}
	}
	public void Use()
	{
		if (HealerController.instance.CarriedItem == null) {
			ingredient.itemTransform = Instantiate(_ingredientSpriteRenderer.gameObject, HealerController.instance.transform).transform;
			HealerController.instance.CarriedItem = ingredient;
		}
	}

	public void Release() {}
	public void Clean() {}
}
