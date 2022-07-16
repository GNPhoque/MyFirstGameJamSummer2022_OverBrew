using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeLinesScript : MonoBehaviour
{
    public RecipeScriptableObject[] recipes;
    public GameObject recipeEntry;

    private void Awake()
    {
        Transform transform = this.transform;

        foreach (RecipeScriptableObject recipe in recipes)
        {
            GameObject entry =  Instantiate(recipeEntry, transform);
            RecipeEntryScript EntryScript = entry.GetComponent<RecipeEntryScript>();
            EntryScript.Recipe = recipe;
        }
    }
}
