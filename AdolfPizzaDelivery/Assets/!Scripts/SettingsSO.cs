using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[CreateAssetMenu(menuName="GameSettings")]
public class SettingsSO : ScriptableObject
{
    [SerializeField] private float _MusicVolume;
    [SerializeField] private float _SoundsVolume;
    [SerializeField] private float _GeneralVolume;
    [SerializeField] private float _Sensivity;
    public float MusicVolume
    {
        get
        {
            return _MusicVolume;
        }
        set
        {
            if (value < 0)
                _MusicVolume = 0;
            else if (value > 1)
                _MusicVolume = 1;
            else
                _MusicVolume = value;
        }
    }
    public float SoundsVolume
    {
        get
        {
            return _SoundsVolume;
        }
        set
        {
            if (value < 0)
                _SoundsVolume = 0;
            else if (value > 1)
                _SoundsVolume = 1;
            else
                _SoundsVolume = value;
        }
    }
    public float GeneralVolume
    {
        get
        {
            return _GeneralVolume;
        }
        set
        {
            if (value < 0)
                _GeneralVolume = 0;
            else if (value > 1)
                _GeneralVolume = 1;
            else
                _GeneralVolume = value;
        }
    }
    
    public float Sensivity
    {
        get
        {
            return _Sensivity;
        }
        set
        {
            if (value < 0.5f)
                _Sensivity = 0.5f;
            else if (value > 3)
                _Sensivity = 3;
            else
                _Sensivity = value;
        }
    }
}
