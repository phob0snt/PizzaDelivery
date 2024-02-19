using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class DestinationArrow : MonoBehaviour
{
    [SerializeField] private Transform rotationCenter;
    public Transform markerPos;
    private float currentAngle;
    private float rotation;

    void Update()
    {
        if (markerPos != null)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            rotation = currentAngle + transform.eulerAngles.y;
            Debug.Log(Vector3.Distance(transform.position, markerPos.position));
            if (Vector3.Distance(transform.position, markerPos.position) > 4.5f)
            {
                transform.RotateAround(rotationCenter.position, Vector3.up, rotation);
                currentAngle -= rotation;
            } 
            transform.LookAt(markerPos.position);
        }
        else
            transform.GetChild(0).gameObject.SetActive(false);
    }

}
