using TMPro;
using UnityEngine;
using Zenject;

public class LevelInfoView : View
{
    [SerializeField] private TMP_Text _maxOrdersText;
    [SerializeField] private TMP_Text _earnMultiplierText;
    [Inject] private ProgressManager _progressManager;

    private void OnEnable()
    {
        DeliveryManager.OnCompletedChain.AddListener(UpdateLevelInfo);
    }

    private void OnDisable()
    {
        DeliveryManager.OnCompletedChain.RemoveListener(UpdateLevelInfo);
    }

    public override void Init()
    {
        UpdateLevelInfo();
    }

    public void UpdateLevelInfo()
    {
        _maxOrdersText.text = $"Max Orders: {_progressManager.GetLevelMaxOrders()} " +
                             $"(+{_progressManager.GetLevelMaxOrders(_progressManager.GetLevel() + 1) - _progressManager.GetLevelMaxOrders()} " +
                             $"on Level {_progressManager.GetLevel() + 1})";
        _earnMultiplierText.text = $"Income Multiplier: {_progressManager.GetLevelEarnMultiplier()}\r\n" +
                                  $"(+{_progressManager.GetLevelEarnMultiplier(_progressManager.GetLevel() + 1) - _progressManager.GetLevelEarnMultiplier()}x " +
                                  $"on Level {_progressManager.GetLevel() + 1})";
    }
}
