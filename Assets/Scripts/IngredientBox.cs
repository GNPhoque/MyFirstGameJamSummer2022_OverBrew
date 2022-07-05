using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBox : MonoBehaviour, IInteractable
{
	[SerializeField]
	Ingredient ingredient;

	public void Use()
	{
		if (GameManager.instance.Ingredient == Ingredient.None)
			GameManager.instance.Ingredient = ingredient;
	}
}
