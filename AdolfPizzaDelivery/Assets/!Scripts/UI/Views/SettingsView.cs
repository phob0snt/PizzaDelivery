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
        _genSlider.value = SettingsManager.Instance.GetVolume(VolumeType.General) * 100;
        _musicSlider.value = SettingsManager.Instance.GetVolume(VolumeType.Music) * 100;
        _soundsSlider.value = SettingsManager.Instance.GetVolume(VolumeType.Sfx) * 100;
        _sensivitySlider.value = SettingsManager.Instance.GetSensivity();
        _applyBtn.onClick.AddListener(() => _viewManager.ShowLast());
        _applyBtn.onClick.AddListener(() => ApplySettings());
    }

    private void ApplySettings()
    {
        SettingsValues settingsValues = new()
        {
            GenVolume = _genSlider.value / 100,
            MusVolume = _musicSlider.value / 100,
            SfxVolume = _soundsSlider.value / 100,
            Sensivity = Mathf.Round(_sensivitySlider.value * 100) * 0.01f
        };
        SettingsManager.Instance.ApplySettings(settingsValues);
    }
}
