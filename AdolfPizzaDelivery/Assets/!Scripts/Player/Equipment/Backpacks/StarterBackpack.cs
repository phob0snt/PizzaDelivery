using UnityEngine;


public class StarterBackpack : Backpack
{
    [SerializeField] private Transform _pizzaSpawnPoint;
    [SerializeField] private GameObject _pizzaBoxPrefab;

    public override int Capability { get; } = 3;

    public override void SpawnPizzaBoxes(int amount)
    {
        int pizzaAmount;
        if (_progressManager.GetLevelMaxOrders() <= Capability && amount <= Capability)
            pizzaAmount = amount;
        else
            pizzaAmount = Capability;

        for (int i = 0; i < pizzaAmount; i++)
        {
            GameObject tempPizza = Instantiate(_pizzaBoxPrefab);
            _pizzas.Add(tempPizza.GetComponent<Pizza>());
            tempPizza.transform.SetParent(_pizzaSpawnPoint, true);
            tempPizza.transform.position = new Vector3(_pizzaSpawnPoint.position.x, _pizzaSpawnPoint.transform.position.y + (i * 0.0225f), _pizzaSpawnPoint.position.z);
            tempPizza.transform.localEulerAngles = new Vector3(-90, -90, 0);
        }
    }
}
