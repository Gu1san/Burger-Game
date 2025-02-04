using TMPro;
using UnityEngine;

public class OrderCard : MonoBehaviour
{
    [SerializeField] TMP_Text clientName;
    [SerializeField] TMP_Text ingredientsList;
    [SerializeField] TMP_Text address;

    private Order order;

    public void FillOrder(Order newOrder)
    {
        order = newOrder;
        clientName.text = $"{order.client.name.first} {order.client.name.last}";
        address.text = $"{order.client.location.street.name}, {order.client.location.street.number} - {order.client.location.city}";
        foreach (var item in order.ingredients)
        {
            ingredientsList.text += item.name + ", ";
        }
    }
}
