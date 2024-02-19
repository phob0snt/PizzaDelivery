using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public List<Pizza> pizzas = new();
    [SerializeField] private Backpack backpack;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(Instance);
    }
    public void PutPizza(Transform pizzaPos)
    {
        Pizza takenPizza = pizzas[pizzas.Count - 1];
        takenPizza.transform.SetParent(pizzaPos);
        takenPizza.transform.localPosition = Vector3.zero;
        pizzas.Remove(takenPizza);
    }
}
