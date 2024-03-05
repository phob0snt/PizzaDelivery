using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Backpack : MonoBehaviour
{
    protected List<Pizza> _pizzas = new();

    protected Player _player;

    [Inject] protected ProgressManager _progressManager;

    protected abstract int _capability { get; }

    public Pizza GetPizza()
    {
        Pizza pizza = _pizzas[_pizzas.Count - 1];
        _pizzas.Remove(pizza);
        return pizza;
    }

    protected virtual void Awake()
    {
        _player = GetComponent<Player>();
    }

    public abstract void SpawnPizzaBoxes(int amount);
}


