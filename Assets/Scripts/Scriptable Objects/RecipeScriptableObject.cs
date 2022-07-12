using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Recipe")]
public class RecipeScriptableObject : ScriptableObject
{
    public List<Ingredient> ingredients;
    public Item result;
    
}
