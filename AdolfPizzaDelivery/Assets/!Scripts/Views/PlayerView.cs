using UnityEngine;
using UnityEngine.UI;

public class PlayerView : View
{
    public Button pauseBtn;
    public override void Init()
    {
        pauseBtn.onClick.AddListener(PauseBtn);
    }


    public void PauseBtn()
    {
        _ViewManager.Show<PauseView>(true, false);
        pauseBtn.interactable = false;
    }
}
