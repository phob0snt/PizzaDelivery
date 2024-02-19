using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingsView : View
{
    [SerializeField] private Slider genSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundsSlider;
    [SerializeField] private Slider sensivitySlider;
    [SerializeField] private Button applyBtn;
    [Inject] private ViewManager viewManager;

    public override void Init()
    {
        genSlider.value = AudioManager.Instance.AudioSettings.GeneralVolume * 100;
        musicSlider.value = AudioManager.Instance.AudioSettings.MusicVolume * 100;
        soundsSlider.value = AudioManager.Instance.AudioSettings.SoundsVolume * 100;
        //sensivitySlider.value = SettingsManager.Instance.Settings.Sensivity;
        applyBtn.onClick.AddListener(() => viewManager.ShowLast());
        applyBtn.onClick.AddListener(() => ApplySettings());
    }

    private void ApplySettings()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.ChangeVolume(genSlider.value / 100, musicSlider.value / 100, soundsSlider.value / 100);
        if (SettingsManager.Instance != null)
            SettingsManager.Instance.ChangeSensivity(Mathf.Round(sensivitySlider.value * 100) * 0.01f);
    }
}
