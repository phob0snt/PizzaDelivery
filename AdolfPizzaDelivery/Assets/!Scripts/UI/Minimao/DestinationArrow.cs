using System.Collections.Generic;
using UnityEngine;

public class DestinationArrow : MonoBehaviour
{
    [SerializeField] private Transform _rotationCenter;
    public List<Transform> Markers = new();
    private float _currentAngle;
    private float _rotation;

    private void OnEnable()
    {
        DeliveryManager.OnCompletedChain.AddListener(() => Markers = new());
    }

    private void OnDisable()
    {
        DeliveryManager.OnCompletedChain.RemoveListener(() => Markers = new());
    }

    void Update()
    {
        Transform nearestMarker = null;
        float minMarkerDistance = Mathf.Infinity;
        foreach (var marker in Markers)
        {
            if (Vector3.Distance(transform.position, marker.position) < minMarkerDistance)
            {
                minMarkerDistance = Vector3.Distance(transform.position, marker.position);
                nearestMarker = marker;
            }
        }
        if (nearestMarker != null)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            _rotation = _currentAngle + transform.eulerAngles.y;
            if (Vector3.Distance(transform.position, nearestMarker.position) > 4.5f)
            {
                transform.RotateAround(_rotationCenter.position, Vector3.up, _rotation);
                _currentAngle -= _rotation;
            } 
            transform.LookAt(nearestMarker.position);
        }
        else
            transform.GetChild(0).gameObject.SetActive(false);
    }

}
