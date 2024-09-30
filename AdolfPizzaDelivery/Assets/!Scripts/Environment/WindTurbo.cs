using UnityEngine;

public class WindTurbo : MonoBehaviour
{
    [SerializeField] private Transform _bladesParent;

    private const int ROTATION_SPEED = 45;
    void Update()
    {
        _bladesParent.Rotate(new Vector3(Time.deltaTime * ROTATION_SPEED, 0, 0));
    }
}
