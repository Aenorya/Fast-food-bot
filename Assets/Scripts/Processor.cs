using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Processor : MonoBehaviour
{
    public string label;
    public List<RecipeData> recipes = new List<RecipeData>();
    public int maxIngredients = 3;

    public ProcessorStates state;
    public Transform spawnPoint;

    private List<IngredientData> ingredientsInProcessor = new List<IngredientData>();

    private GameObject spawnPrefab,showCooked;

    public RecipeData recipeToCook;

    public void AddIngredient(IngredientData ingredient)
    {
        ingredientsInProcessor.Add(ingredient);
        RecipeData foundRecipe = CheckIfCookable();
        if(foundRecipe != null)
        {
            state = ProcessorStates.WaitingToCook;
            recipeToCook = foundRecipe;
        }
        else
        {
            Debug.Log("No recipe found");
        }
    }

    public RecipeData CheckIfCookable()
    {
        bool found = false;
        for(int r = 0; r < recipes.Count; r++)
        {
            if(ingredientsInProcessor.Count == recipes[r].ingredients.Count)
            {
                found = true;
                for(int i = 0; i < recipes[r].ingredients.Count; i++)
                {
                    if (!ingredientsInProcessor.Contains(recipes[r].ingredients[i]))
                    {
                        found = false;
                    }
                }
                if (found)
                {
                    return recipes[r];
                }
            }
        }
        return null;
    }

    public void Cook()
    {
        if (recipeToCook != null)
        {
            //Instantiate food prefab
            spawnPrefab = recipeToCook.result.prefab;
            state = ProcessorStates.Cooking;
            Invoke("SpawnFood", recipeToCook.timeToCook);
        }
    }

    private void SpawnFood()
    {
        state = ProcessorStates.Done;
        showCooked = Instantiate(spawnPrefab, spawnPoint);
    }

    public void GiveFood()
    {
        Destroy(showCooked);
        FindObjectOfType<PlayerInteraction>().HoldObject(spawnPrefab);
        state = ProcessorStates.Idle;
    }
}
