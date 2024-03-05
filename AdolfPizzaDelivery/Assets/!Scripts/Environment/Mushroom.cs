using UnityEngine;

public class Mushroom : MonoBehaviour, IUsable
{
    public void Use()
    {
        Player.Instance.ActivateMushroomEffect();
        Destroy(gameObject);
    }
}
