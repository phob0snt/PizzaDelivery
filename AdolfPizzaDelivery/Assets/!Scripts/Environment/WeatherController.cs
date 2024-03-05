using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

#region WeatherClasses
public abstract class Weather
{
    public abstract TOD_WeatherManager.AtmosphereType Atmosphere { get; }
    public abstract TOD_WeatherManager.CloudType Clouds { get; }
    public abstract TOD_WeatherManager.RainType Rain { get; }

}
public class Rainy : Weather
{
    public override TOD_WeatherManager.AtmosphereType Atmosphere => TOD_WeatherManager.AtmosphereType.Dust;
    public override TOD_WeatherManager.CloudType Clouds => TOD_WeatherManager.CloudType.Broken;
    public override TOD_WeatherManager.RainType Rain => TOD_WeatherManager.RainType.Light;
}

public class Clear : Weather
{
    public override TOD_WeatherManager.AtmosphereType Atmosphere => TOD_WeatherManager.AtmosphereType.Clear;
    public override TOD_WeatherManager.CloudType Clouds => TOD_WeatherManager.CloudType.Few;
    public override TOD_WeatherManager.RainType Rain => TOD_WeatherManager.RainType.None;
}

public class Foggy : Weather
{
    public override TOD_WeatherManager.AtmosphereType Atmosphere => TOD_WeatherManager.AtmosphereType.Fog;
    public override TOD_WeatherManager.CloudType Clouds => TOD_WeatherManager.CloudType.Scattered;
    public override TOD_WeatherManager.RainType Rain => TOD_WeatherManager.RainType.None;
}

public class Stormy : Weather
{
    public override TOD_WeatherManager.AtmosphereType Atmosphere => TOD_WeatherManager.AtmosphereType.Storm;
    public override TOD_WeatherManager.CloudType Clouds => TOD_WeatherManager.CloudType.Overcast;
    public override TOD_WeatherManager.RainType Rain => TOD_WeatherManager.RainType.Heavy;
}

public class Cloudly : Weather
{
    public override TOD_WeatherManager.AtmosphereType Atmosphere => TOD_WeatherManager.AtmosphereType.Clear;
    public override TOD_WeatherManager.CloudType Clouds => TOD_WeatherManager.CloudType.Broken;
    public override TOD_WeatherManager.RainType Rain => TOD_WeatherManager.RainType.None;
}
#endregion

[RequireComponent(typeof(TOD_WeatherManager))]
public class WeatherController : MonoBehaviour
{
    [Range(2, 10)]
    [SerializeField] private int _WeatherChangeMinutesRate = 5;
    private const int WEATHER_RANDOM_MINS = 2;
    private TOD_WeatherManager _weatherManager;
    private readonly List<Weather> _weatherList = new() { new Rainy(), new Clear(), new Foggy(), new Stormy(), new Cloudly() };
    private Weather _currentWeather;

    [Inject] TerrainLayerManager _layerManager;
    
    private void Awake()
    {
        _weatherManager = GetComponent<TOD_WeatherManager>();
        StartCoroutine(WeatherRandomizer());
    }

    private IEnumerator WeatherRandomizer()
    {
        yield return new WaitForSeconds(Random.Range(_WeatherChangeMinutesRate * 60 - (60 * WEATHER_RANDOM_MINS), _WeatherChangeMinutesRate * 60 + (60 * WEATHER_RANDOM_MINS)));
        Weather tempWeather = _weatherList[Random.Range(0, _weatherList.Count)];
        while (_currentWeather == tempWeather)
        {
            tempWeather = _weatherList[Random.Range(0, _weatherList.Count)];
        }
        SetWeather(tempWeather);
        if (tempWeather is Rainy || tempWeather is Stormy)
            StartCoroutine(_layerManager.ChangeSurfaceWetness(true));
        else
            StartCoroutine(_layerManager.ChangeSurfaceWetness(false));
        StartCoroutine(WeatherRandomizer());
    }

    private void SetWeather(Weather weatherType)
    {
        _weatherManager.Atmosphere = weatherType.Atmosphere;
        _weatherManager.Clouds = weatherType.Clouds;
        _weatherManager.Rain = weatherType.Rain;
        _currentWeather = weatherType;
    }
}
