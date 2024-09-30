using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerInteractor))]
public class Player : Singleton<Player>
{
    [SerializeField] private Backpack _backpack;
    public Backpack Backpack { get { return _backpack; } }

    [SerializeField] VolumeController _volumeController;
    public PlayerController PlayerController => GetComponent<PlayerController>();
    public PlayerInteractor PlayerInteractor => GetComponent<PlayerInteractor>();

    public void SpawnPizza(int amount)
    {
        _backpack.SpawnPizzaBoxes(amount);
    }

    public void PutPizza(Transform pizzaPos)
    {
        Pizza takenPizza = _backpack.GetPizza();
        takenPizza.transform.SetParent(pizzaPos);
        takenPizza.transform.localPosition = Vector3.zero;
    }

    public void ActivateMushroomEffect()
    {
        Debug.Log("MLGGGGG");
        _volumeController.SetEffect();
    }

    public void DarkenScreen(float duration)
    {
        _volumeController.DarkenScreen(duration);
    }
}

