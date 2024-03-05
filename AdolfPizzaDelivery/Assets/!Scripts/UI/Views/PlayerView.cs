using UnityEngine;
using UnityEngine.UI;

public class PlayerView : View
{
    [SerializeField] private Button _pauseBtn;
    [SerializeField] private Button _useBtn;

    public override void Init()
    {
        _pauseBtn.onClick.AddListener(() => ActivatePauseMenu(true));
        _useBtn.onClick.AddListener(Use);
    }

    public void ActivatePauseMenu(bool state)
    {
        if (state)
        {
            _viewManager.Show<PauseView>(true, false);
            _pauseBtn.interactable = false;
        }
        else
        {
            _viewManager.ShowLast();
            _pauseBtn.interactable = true;
        }
    }

    public void ToggleUseBtn(bool state)
    {
        _useBtn.gameObject.SetActive(state);
    }

    public void Use()
    {
        Player.Instance.ActivateMushroomEffect();
        Debug.Log("skushal!");
    }
}
