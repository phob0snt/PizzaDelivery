using UnityEngine;
using UnityEngine.UI;

public class PlayerView : View
{
    [SerializeField] private Button _pauseBtn;
    [SerializeField] private Button _useBtn;
    private IUsable _interactionTarget;

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

    public void ToggleUseBtn(bool state, IUsable obj = null)
    {
        _useBtn.gameObject.SetActive(state);
        if (state)
            _interactionTarget = obj;
        else
            _interactionTarget = null;
    }

    public void Use()
    {
        _interactionTarget.Use();
    }
}
