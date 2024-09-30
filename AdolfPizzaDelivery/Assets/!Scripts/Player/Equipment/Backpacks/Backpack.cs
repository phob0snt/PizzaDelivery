using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Backpack : MonoBehaviour
{
    protected List<Pizza> _pizzas = new();

    [Inject] protected ProgressManager _progressManager;
    [Inject] private TouchCameraRotation cam;

    public abstract int Capability { get; }

    public Pizza GetPizza()
    {
        Pizza pizza = _pizzas[_pizzas.Count - 1];
        _pizzas.Remove(pizza);
        return pizza;
    }

    public abstract void SpawnPizzaBoxes(int amount);
}


