using UnityEngine;

[CreateAssetMenu(menuName="GameSettings")]
public class SettingsSO : ScriptableObject
{
    [SerializeField] private float _musicVolume;
    [SerializeField] private float _soundsVolume;
    [SerializeField] private float _generalVolume;
    [SerializeField] private float _sensivity;
    public float MusicVolume
    {
        get
        {
            return _musicVolume;
        }
        set
        {
            if (value < 0)
                _musicVolume = 0;
            else if (value > 1)
                _musicVolume = 1;
            else
                _musicVolume = value;
        }
    }
    public float SoundsVolume
    {
        get
        {
            return _soundsVolume;
        }
        set
        {
            if (value < 0)
                _soundsVolume = 0;
            else if (value > 1)
                _soundsVolume = 1;
            else
                _soundsVolume = value;
        }
    }
    public float GeneralVolume
    {
        get
        {
            return _generalVolume;
        }
        set
        {
            if (value < 0)
                _generalVolume = 0;
            else if (value > 1)
                _generalVolume = 1;
            else
                _generalVolume = value;
        }
    }
    
    public float Sensivity
    {
        get
        {
            return _sensivity;
        }
        set
        {
            if (value < 0.5f)
                _sensivity = 0.5f;
            else if (value > 3)
                _sensivity = 3;
            else
                _sensivity = value;
        }
    }
}
