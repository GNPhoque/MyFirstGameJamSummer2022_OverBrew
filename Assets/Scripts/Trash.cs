using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour, IInteractable
{
	public void Use()
	{
		GameManager.instance.CarriedItem = null;
	}

	public void Release() {}
	public void Clean() {}
}
