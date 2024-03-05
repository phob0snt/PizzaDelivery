using UnityEngine;

public class SettingsManager : Singleton<SettingsManager>
{
    public SettingsSO Settings { get; private set; }
    [HideInInspector] public TouchCameraRotation CameraRotation;

    protected override void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        base.Awake();
        Init();
    }

    private void Init()
    {
        Settings = Resources.Load<SettingsSO>("GameSettings"); 
    }


    public void ChangeSensivity(float Sensivity)
    {
        Settings.Sensivity = Sensivity;
        if (CameraRotation != null)
        {
            CameraRotation.Sensivity = Sensivity;
        }
    }
}
