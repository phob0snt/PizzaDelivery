using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    public static Backpack Instance;
    [SerializeField] Transform pizzaSpawnPoint;
    [SerializeField] GameObject pizzaBoxPrefab;
    private const int MAX_PIZZA = 10;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }

    public void SpawnPizzaBoxes(int amount)
    {
        if (amount <= MAX_PIZZA)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject tempPizza = Instantiate(pizzaBoxPrefab);
                Player.Instance.pizzas.Add(tempPizza.GetComponent<Pizza>());
                tempPizza.transform.SetParent(pizzaSpawnPoint, true);
                tempPizza.transform.localPosition = new Vector3(0, pizzaSpawnPoint.transform.localPosition.y + (i * 0.0225f), 0);
                tempPizza.transform.localEulerAngles = new Vector3(-90, 180, 0);
            }
        }
    }
}
