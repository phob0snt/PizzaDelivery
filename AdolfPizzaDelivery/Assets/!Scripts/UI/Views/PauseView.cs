using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseView : View
{
    [SerializeField] private Button _settingsBtn;
    [SerializeField] private Button _resumeBtn;
    [SerializeField] private Button _menuBtn;

    public override void Init()
    {
        _resumeBtn.onClick.AddListener(() => _viewManager.GetView<PlayerView>().ActivatePauseMenu(false));
        _settingsBtn.onClick.AddListener(() => _viewManager.Show<SettingsView>(true));
        _menuBtn.onClick.AddListener(() => SceneManager.LoadScene("Main Menu", LoadSceneMode.Single));
    }
}
