using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class StreetLight : MonoBehaviour
{
    private Light _light;
    private void Awake()
    {
        _light = GetComponent<Light>();
        _light.enabled = false;
    }

    private void OnEnable()
    {
        StartCoroutine(SubscribeOnTOD());
    }

    private void OnDisable()
    {
        //TOD_Sky.Instance.Components.Time.OnSunrise -= () => ToggleLight(false);
        //TOD_Sky.Instance.Components.Time.OnSunset -= () => ToggleLight(true);
    }

    private void OnApplicationQuit()
    {
        TOD_Sky.Instance.Components.Time.OnSunrise -= () => ToggleLight(false);
        TOD_Sky.Instance.Components.Time.OnSunset -= () => ToggleLight(true);
    }

    private void ToggleLight(bool state)
    {
        _light.enabled = state;
    }

    private IEnumerator SubscribeOnTOD()
    {
        yield return new WaitForSeconds(1);
        TOD_Sky.Instance.Components.Time.OnSunrise += () => ToggleLight(false);
        TOD_Sky.Instance.Components.Time.OnSunset += () => ToggleLight(true);
    }
}
