using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class PauseView : View
{
    [Inject] private ViewManager viewManager;
    [SerializeField] private PlayerView playerView;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button resumeBtn;
    [SerializeField] private Button menuBtn;
    public override void Init()
    {
        resumeBtn.onClick.AddListener(() => _ViewManager.ShowLast());
        resumeBtn.onClick.AddListener(() => playerView.pauseBtn.interactable = true);
        settingsBtn.onClick.AddListener(() => viewManager.Show<SettingsView>(true));
        menuBtn.onClick.AddListener(() => SceneManager.LoadScene("Main Menu", LoadSceneMode.Single));
    }
}
