using TMPro;
using UnityEngine;
using Zenject;

public class MoneyManager : MonoBehaviour
{
    private int _money;
    private int _orderMoney;
    private int _earnedMoney;

    [SerializeField] private TMP_Text _playerMoneyText;
    [SerializeField] private TMP_Text _earnedMoneyText;
    [SerializeField] private TMP_Text _orderMoneyText;
    [Inject] private ProgressManager _progressManager;

    private void Awake()
    {
        _playerMoneyText.text = _money.ToString() + "$";
        _orderMoneyText.text = _orderMoney.ToString() + "$";
        _earnedMoneyText.text = _earnedMoney.ToString() + "$";
    }

    private void OnEnable()
    {
        DeliveryManager.OnCompletedOrder.AddListener(IncreaseOrderMoney);
        DeliveryManager.OnCompletedChain.AddListener(IncreasePlayerMoney);
        DeliveryManager.OnCompletedChain.AddListener(ClearOrderMoney);
    }

    private void OnDisable()
    {
        DeliveryManager.OnCompletedOrder.RemoveListener(IncreaseOrderMoney);
        DeliveryManager.OnCompletedChain.RemoveListener(IncreasePlayerMoney);
        DeliveryManager.OnCompletedChain.RemoveListener(ClearOrderMoney);
    }
    public void IncreaseOrderMoney(DeliveryOrder _)
    {
        _orderMoney += (int)(Random.Range(8, 20) * _progressManager.GetLevelEarnMultiplier());
        _earnedMoney += (int)(Random.Range(2, 5) * _progressManager.GetLevelEarnMultiplier());
        _orderMoneyText.text = _orderMoney.ToString() + "$";
        _earnedMoneyText.text = _earnedMoney.ToString() + "$";
    }

    public void ClearOrderMoney()
    {
        _orderMoney = 0;
        _earnedMoney = 0;
        _orderMoneyText.text = _orderMoney.ToString() + "$";
        _earnedMoneyText.text = _earnedMoney.ToString() + "$";
    }

    public void IncreasePlayerMoney()
    {
        _money += GetOrderMoney()[1];
        _playerMoneyText.text = _money.ToString() + "$";
    }

    public int GetMoney()
    {
        return _money;
    }

    public int[] GetOrderMoney()
    {
        return new int[] {_orderMoney, _earnedMoney };
    }
}
