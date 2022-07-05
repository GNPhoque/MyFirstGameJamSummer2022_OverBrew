using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance { get; private set; }
	public Ingredient Ingredient { get => ingredient; set { ingredient = value; currentItemText.text = "Current item : " + ingredient; } }

	[SerializeField]
	TMPro.TMP_Text currentItemText;

	private Ingredient ingredient = Ingredient.None;

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
