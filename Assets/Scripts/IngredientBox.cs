using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBox : MonoBehaviour, IInteractable
{
	[SerializeField]
	Ingredient ingredient;

	public void Use()
	{
		if (GameManager.instance.CarriedItem == null)
			GameManager.instance.CarriedItem = ingredient;
	}

	public void Release() {}
	public void Clean() {}
}
