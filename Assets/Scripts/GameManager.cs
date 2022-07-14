using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance { get; private set; }
	/*public Item CarriedItem { get => carriedItem; set { carriedItem = value; *.text = "Current item : " + carriedItem; } }

	[SerializeField]
	TMPro.TMP_Text currentItemText;

	private Item carriedItem;*/

	private void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
	}
}
