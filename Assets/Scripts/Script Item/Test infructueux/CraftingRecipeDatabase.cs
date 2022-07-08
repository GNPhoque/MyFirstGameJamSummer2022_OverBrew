using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipeDatabase : MonoBehaviour
{
    public List<CraftRecipe> craftingRecipes = new List<CraftRecipe>();

    private void Awake()
    {
        BuildCraftingDatabase();
    }
    public CraftRecipe FindItem(int id)
    {
        return craftingRecipes.Find(craftingRecipe => craftingRecipe.itemToCraft == id);
    }
    void BuildCraftingDatabase()
    {
        craftingRecipes = new List<CraftRecipe>()
        {
            new CraftRecipe(1, new int[2]{0,1})
        };
    }
}
