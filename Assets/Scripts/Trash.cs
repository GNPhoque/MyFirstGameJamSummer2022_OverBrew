using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour, IInteractable
{
	public void Use()
	{
		Destroy(HealerController.instance.CarriedItem.itemTransform.gameObject);
		HealerController.instance.CarriedItem = null;
	}

	public void Release() {}
	public void Clean() {}
}
