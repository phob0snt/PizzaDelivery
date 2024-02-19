using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class MainMenuView : View
{
    [Inject] private ViewManager viewManager;
    [SerializeField] private Button playBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button quitBtn;

    public override void Init()
    {
        playBtn.onClick.AddListener(() => SceneManager.LoadScene("Game", LoadSceneMode.Single));
        settingsBtn.onClick.AddListener(() => viewManager.Show<SettingsView>());
        quitBtn.onClick.AddListener(() => Application.Quit());
    }
}
