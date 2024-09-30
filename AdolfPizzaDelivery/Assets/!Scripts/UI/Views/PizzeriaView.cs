using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PizzeriaView : View
{
    [SerializeField] private GameObject _orderChainWindow;

    [SerializeField] private TMP_Text _ordersCompleted;
    [SerializeField] private TMP_Text _currentLevel;
    [SerializeField] private TMP_Text _levelProgress;

    [SerializeField] private Slider _levelProgressSlider;

    [SerializeField] private Button _goBack;
    [SerializeField] private Button _levelInfo;
    [SerializeField] private Button _getOrderChain;
    [SerializeField] private Button _completeOrderChain;
    [SerializeField] private Button _map;

    [Inject] private ProgressManager _progressManager;
    [Inject] private DeliveryManager _deliveryManager;

    private void OnEnable()
    {
        DeliveryManager.OnCompletedChain.AddListener(Update_levelProgress);
    }

    private void OnDisable()
    {
        DeliveryManager.OnCompletedChain.RemoveListener(Update_levelProgress);
    }

    public override void Init()
    {
        _goBack.onClick.AddListener(() =>
        {
            if (_viewManager.IsCurrentView<MapView>())
                _viewManager.ShowLast();
            if (_viewManager.IsCurrentView<LevelInfoView>())
                _viewManager.ShowLast();
            _viewManager.ShowLast();
        });
        _map.onClick.AddListener(() => TryShow<MapView>());
        _levelInfo.onClick.AddListener(() => TryShow<LevelInfoView>());
        _getOrderChain.onClick.AddListener(GetOrderChain);
        _completeOrderChain.onClick.AddListener(CompleteOrderChain);
        Update_levelProgress();
    }

    public void Update_levelProgress()
    {
        _currentLevel.text = $"Level {_progressManager.GetLevel()}";
        _levelProgress.text = $"( {_progressManager.GetProgress()[0]} / {_progressManager.GetProgress()[1]} )";
        if (_progressManager.GetProgress()[0] != 0)
            _levelProgressSlider.value = 1f / ((float)_progressManager.GetProgress()[1] / (float)_progressManager.GetProgress()[0]);
        else
            _levelProgressSlider.value = 0;
    }

    public override void Show()
    {
        base.Show();
        ViewManager.OnPaused.Invoke(true);
        switch (_deliveryManager.OrderState)
        {
            case OrderState.Default:
                _getOrderChain.gameObject.SetActive(true);
                break;
            case OrderState.Active:
                _orderChainWindow.SetActive(true);
                _ordersCompleted.text = $"{_deliveryManager.CurrentChain.OrdersCompleted}/{_deliveryManager.CurrentChain.orders.Count} delivered";
                break;
        } 
    }

    public override void Hide()
    {
        base.Hide();
        ViewManager.OnPaused.Invoke(false);
        _getOrderChain.gameObject.SetActive(false);
        _orderChainWindow.SetActive(false);
    }

    private void GetOrderChain()
    {
        _deliveryManager.CreateOrderChain();
        _viewManager.ShowLast();
    }

    private void TryShow<T>() where T: View
    {
        if (!_viewManager.IsCurrentView<T>())
            _viewManager.Show<T>(true, false);
        else
            _viewManager.ShowLast();
    }

    private void CompleteOrderChain()
    {
        if (_deliveryManager.CurrentChain.OrdersCompleted == _deliveryManager.CurrentChain.orders.Count)
        {
            _deliveryManager.CompleteOrderChain();
            _viewManager.ShowLast();
            _viewManager.Show<PizzeriaView>();
        }
    }
}
