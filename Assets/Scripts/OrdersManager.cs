using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class OrdersManager : MonoBehaviour
{
    [SerializeField] GameObject[] availableIngredients;
    [SerializeField] GameObject bread;
    [SerializeField] Plate plate;

    [SerializeField] TMP_Text clientNameText; 
    [SerializeField] TMP_Text addressText; 
    [SerializeField] TMP_Text ingredientsText;

    [SerializeField] RandomUserAPI api;

    public Queue<Order> orders = new();

    public async void GetOrder(){
        await RandomOrderAsync();
    }

    private async Task<Order> RandomOrderAsync(){
        Order order = new()
        {
            client = await api.GetUserAsync(),
            ingredients = new Stack<GameObject>()
        };
        order.ingredients.Push(bread);
        int ingredientsCount = Random.Range(1, 4);
        for (int i = 0; i < ingredientsCount; i++){
            int ingredientIndex = Random.Range(0, ingredientsCount);
            GameObject ingredient = availableIngredients[ingredientIndex];
            order.ingredients.Push(ingredient);
        }
        order.ingredients.Push(bread);
        string orderDetails = string.Empty;
        foreach (var item in order.ingredients)
        {
            orderDetails += item.name + ", ";
        }
        ingredientsText.text = orderDetails;
        clientNameText.text = $"{order.client.name.first} {order.client.name.last}";
        addressText.text = $"{order.client.location.street.name}, {order.client.location.street.number} - {order.client.location.city}";
        orders.Enqueue(order);
        return order;
    }

    public async void Deliver(){
        var orderIngredients = orders.Peek().ingredients.ToArray();
        var plateIngredients = plate.ingredients.ToArray();

        if(orderIngredients.Length != plateIngredients.Length){
            Debug.Log("Wrong order!");
            return;
        }

        for(int i = 0; i < orderIngredients.Length; i++){
            if(!plateIngredients[i].CompareTag(orderIngredients[i].tag))
            {
                Debug.Log("Wrong order!");
                return;
            }
        }

        Debug.Log("Order delivered!");
        orders.Dequeue();
        await RandomOrderAsync();
    }
}
