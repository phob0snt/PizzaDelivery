using UnityEngine;
using Zenject;

public enum VolumeType { General, Music, Sfx }

public struct SettingsValues
{
    public float GenVolume;
    public float MusVolume;
    public float SfxVolume;
    public float Sensivity;
}

public class SettingsManager : Singleton<SettingsManager>
{
    private SettingsSO _settings;
    private TouchCameraRotation _cameraRotation;

    protected override void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        base.Awake();
        Init();
        Application.targetFrameRate = 60;
    }

    private void Init()
    {
        _settings = Resources.Load<SettingsSO>("GameSettings");
        Debug.Log("Init!");
    }

    public float GetVolume(VolumeType volumeType)
    {
        switch (volumeType)
        {
            case VolumeType.Music:
                return _settings.MusicVolume;
            case VolumeType.Sfx:
                return _settings.SoundsVolume;
            default:
            case VolumeType.General:
                return _settings.GeneralVolume;
        }
    }

    public float GetSensivity()
    {
        return _settings.Sensivity;
    }

    public void SetCameraRotation(TouchCameraRotation cam)
    {
        _cameraRotation = cam;
        SetSettingsValues();
    }

    public void ApplySettings(SettingsValues settingsValues)
    {
        _settings.GeneralVolume = settingsValues.GenVolume;
        _settings.MusicVolume = settingsValues.MusVolume;
        _settings.SoundsVolume = settingsValues.SfxVolume;
        _settings.Sensivity = settingsValues.Sensivity;
        SetSettingsValues();
    }

    private void SetSettingsValues()
    {
        AudioManager.Instance.UpdateSourcesVol();
        Debug.Log(_settings.Sensivity);
        Debug.Log(_cameraRotation);
        if (_cameraRotation != null)
        {
            _cameraRotation.Sensivity = _settings.Sensivity;
        }
    }
}
