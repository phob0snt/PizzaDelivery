using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerInteractor))]
public class Player : Singleton<Player>
{
    [SerializeField] private Backpack _backpack;

    [SerializeField] VolumeController _volumeController;

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
        //_volume.profile.components.Add(ScriptableObject.CreateInstance<Bloom>());
        //ColorCurves a = _volume.profile.components.Find(x => x is ColorCurves) as ColorCurves;
        //a.master.
        //TextureCurve m = new TextureCurve();
        //Bloom m = _volume.profile.components.Find(x => x is Bloom) as Bloom;
        //m.active = true;
        //m.threshold.Override(0.6f);
        //m.intensity.Override(2);
    }
}

