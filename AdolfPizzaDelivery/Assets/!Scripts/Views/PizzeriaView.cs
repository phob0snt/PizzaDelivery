using UnityEngine;
using Enums;
using TMPro;
using UnityEngine.UI;
using Zenject;

public class PizzeriaView : View
{
    [SerializeField] private GameObject chooseOrderUI;
    [SerializeField] private GameObject completeOrderUI;
    [SerializeField] private TMP_Text ordersCompletedText;
    [SerializeField] private Slider levelProgressSlider;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text levelProgressText;
    [SerializeField] private Button backBtn;

    [Inject] private DeliveryManager deliveryManager;
    [Inject] private ViewManager viewManager;

    public override void Init()
    {
        DeliveryManager.OnCompletedChain.AddListener(UpdateLevelProgress);
        backBtn.onClick.AddListener(viewManager.ShowLast);
        UpdateLevelProgress();
    }

    public void UpdateLevelProgress()
    {
        levelText.text = $"Level {deliveryManager.Level.GetLevel()}";
        levelProgressText.text = $"( {deliveryManager.Level.GetProgress()[0]} / {deliveryManager.Level.GetProgress()[1]} )";
        if (deliveryManager.Level.GetProgress()[0] != 0)
            levelProgressSlider.value = 1f / ((float)deliveryManager.Level.GetProgress()[1] / (float)deliveryManager.Level.GetProgress()[0]);
        else
            levelProgressSlider.value = 0;
    }

    public override void Show()
    {
        ViewManager.OnPaused.Invoke(true);
        gameObject.SetActive(true);
        switch (deliveryManager.orderState)
        {
            case OrderState.Default:
                chooseOrderUI.SetActive(true);
                break;
            case OrderState.Active:
                completeOrderUI.SetActive(true);
                ordersCompletedText.text = $"{deliveryManager.currentChain.ordersCompleted}/{deliveryManager.currentChain.ordersInChain} delivered";
                break;
        } 
    }

    public override void Hide()
    {
        ViewManager.OnPaused.Invoke(false);
        gameObject.SetActive(false);
        chooseOrderUI.SetActive(false);
        completeOrderUI.SetActive(false);
    }

    public void GetOrder()
    {
        deliveryManager.CreateOrderChain();
        viewManager.ShowLast();
    }

    public void CompleteOrder()
    {
        if (deliveryManager.currentChain.ordersCompleted == deliveryManager.currentChain.ordersInChain)
        {
            deliveryManager.CompleteOrderChain();
            viewManager.ShowLast();
            viewManager.Show<PizzeriaView>();
        }
    }
}
