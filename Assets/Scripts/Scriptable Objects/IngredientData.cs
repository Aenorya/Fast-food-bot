using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Restaurant Things/Ingredient")]
public class IngredientData : ScriptableObject
{
    public string label;
    public int quantity;
    public GameObject prefab;
}
