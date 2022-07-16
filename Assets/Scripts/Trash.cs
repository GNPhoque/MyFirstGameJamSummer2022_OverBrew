using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour, IInteractable
{
	public void Use()
	{
		HealerController.instance.DropItem();
	}

	public void Release() {}
	public void Clean() {}
}
