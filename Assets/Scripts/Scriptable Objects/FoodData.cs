using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Restaurant Things/Food")]
public class FoodData : ScriptableObject
{
    public string label;
    public string description;
    public GameObject prefab;
    public int price;
}
