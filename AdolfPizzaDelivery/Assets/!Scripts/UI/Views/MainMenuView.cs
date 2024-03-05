using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuView : View
{
    [SerializeField] private Button _playBtn;
    [SerializeField] private Button _settingsBtn;
    [SerializeField] private Button _quitBtn;

    public override void Init()
    {
        _playBtn.onClick.AddListener(() => SceneManager.LoadScene("Game", LoadSceneMode.Single));
        _settingsBtn.onClick.AddListener(() => _viewManager.Show<SettingsView>());
        _quitBtn.onClick.AddListener(() => Application.Quit());
    }
}
