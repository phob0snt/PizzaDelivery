using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapView : View
{
    [SerializeField] private GameObject[] _markPositions;
    [SerializeField] private Button _mapLabel;
    private List<int> _pointsToMark = new();

    public override void Init()
    {
        _mapLabel.onClick.AddListener(_viewManager.ShowLast);
        DeliveryManager.OnCreatedChain.AddListener(MarkPoints);
        DeliveryManager.OnCompletedOrder.AddListener(RemoveMark);
        DeliveryManager.OnCompletedChain.AddListener(_pointsToMark.Clear);
    }
    private void OnDestroy()
    {
        _mapLabel.onClick.RemoveListener(_viewManager.ShowLast);
        DeliveryManager.OnCreatedChain.RemoveListener(MarkPoints);
        DeliveryManager.OnCompletedOrder.RemoveListener(RemoveMark);
        DeliveryManager.OnCompletedChain.RemoveListener(_pointsToMark.Clear);
    }
    
    public void AddPointToMark(int pointNum)
    {
        _pointsToMark.Add(pointNum);
    }

    private void MarkPoints()
    {
        for (int i = 0; i < _pointsToMark.Count; i++)
        {
            _markPositions[_pointsToMark[i]].SetActive(true);
        }
    }

    private void RemoveMark(DeliveryOrder order)
    {
        _markPositions[order.Destination.PointNumber].SetActive(false);
    }
}
