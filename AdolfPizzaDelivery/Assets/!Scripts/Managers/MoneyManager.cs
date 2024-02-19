using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private int _Money;
    private int _OrderMoney;
    private int _EarnedMoney;

    [SerializeField] private TMP_Text playerMoneyText;
    [SerializeField] private TMP_Text earnedMoneyText;
    [SerializeField] private TMP_Text orderMoneyText;

    private void Awake()
    {
        playerMoneyText.text = _Money.ToString() + "$";
        orderMoneyText.text = _OrderMoney.ToString() + "$";
        earnedMoneyText.text = _EarnedMoney.ToString() + "$";
    }
    private void OnEnable()
    {
        DeliveryManager.OnCompletedOrder.AddListener(IncreaseOrderMoney);
    }

    private void OnDisable()
    {
        DeliveryManager.OnCompletedOrder.RemoveListener(IncreaseOrderMoney);
    }
    public void IncreaseOrderMoney(int pizzaMoney, int tipMoney)
    {
        _OrderMoney += pizzaMoney;
        _EarnedMoney += tipMoney;
        orderMoneyText.text = _OrderMoney.ToString() + "$";
        earnedMoneyText.text = _EarnedMoney.ToString() + "$";
    }

    public void ClearOrderMoney()
    {
        _OrderMoney = 0;
        _EarnedMoney = 0;
        orderMoneyText.text = _OrderMoney.ToString() + "$";
        earnedMoneyText.text = _EarnedMoney.ToString() + "$";
    }

    public void IncreaseMoney(int money)
    {
        _Money += money;
        playerMoneyText.text = _Money.ToString() + "$";
    }

    public int GetMoney()
    {
        return _Money;
    }

    public int[] GetOrderMoney()
    {
        return new int[] {_OrderMoney, _EarnedMoney };
    }
}
