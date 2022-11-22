using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Restaurant Things/Recipe")]
public class RecipeData : ScriptableObject
{
    public string label;
    [TextArea]
    public string description;
    public List<IngredientData> ingredients;
    public float timeToCook;
    public FoodData result;
}
