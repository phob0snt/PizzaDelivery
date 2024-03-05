using UnityEngine;
using UnityEngine.UI;

public class SettingsView : View
{
    [SerializeField] private Slider _genSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundsSlider;
    [SerializeField] private Slider _sensivitySlider;
    [SerializeField] private Button _applyBtn;

    public override void Init()
    {
        _genSlider.value = AudioManager.Instance.AudioSettings.GeneralVolume * 100;
        _musicSlider.value = AudioManager.Instance.AudioSettings.MusicVolume * 100;
        _soundsSlider.value = AudioManager.Instance.AudioSettings.SoundsVolume * 100;
        _sensivitySlider.value = SettingsManager.Instance.Settings.Sensivity;
        _applyBtn.onClick.AddListener(() => _viewManager.ShowLast());
        _applyBtn.onClick.AddListener(() => ApplySettings());
    }

    private void ApplySettings()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.ChangeVolume(_genSlider.value / 100, _musicSlider.value / 100, _soundsSlider.value / 100);
        if (SettingsManager.Instance != null)
            SettingsManager.Instance.ChangeSensivity(Mathf.Round(_sensivitySlider.value * 100) * 0.01f);
    }
}
