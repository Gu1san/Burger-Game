using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] Stack<GameObject> ingredients = new();

    public void AddIngredient(GameObject ingredient)
    {
        ingredients.Push(Instantiate(ingredient, transform.position, Quaternion.identity, transform));
    }
}
