using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
	public void Use();
	public void Release();
	public void Clean();
}
