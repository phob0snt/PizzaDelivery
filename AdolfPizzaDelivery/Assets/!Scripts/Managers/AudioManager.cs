using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager>
{
    private List<AudioSource> musicSources = new();
    private List<AudioSource> _soundSources = new();
    private List<AssetReference> _musicGameMix = new();
    private AsyncOperationHandle _currentMusicOperationHandler;
    public MusicState MusicState = MusicState.MainMenu;
    private AudioClip _musicResult;

    public SettingsSO AudioSettings { get; private set; }

    protected override void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject.transform.parent);
        base.Awake();
        Init();
    }

    private void Init()
    {
        var musicSO = Resources.Load<MusicSO>("MusicContainer");
        _musicGameMix.AddRange(musicSO.MusicMix);
        Debug.Log(_musicGameMix.Count);
        AudioSettings = Resources.Load<SettingsSO>("GameSettings");
    }

    public void AddSource(AudioSource source, SoundType type)
    {
        if (type == SoundType.Music)
        {
            musicSources.Add(source);
            if (SceneManager.GetActiveScene().name == "Game")
                StartCoroutine(PlayMusicMix());
        }
        else if (type == SoundType.Sound)
            _soundSources.Add(source);
        UpdateSourcesVol();
    }

    private IEnumerator SetMusicInternal(int musicIndex)
    {
        if (_currentMusicOperationHandler.IsValid())
        {
            Addressables.Release(_currentMusicOperationHandler);
        }

        var musicReference = _musicGameMix[musicIndex];
        _currentMusicOperationHandler = musicReference.LoadAssetAsync<AudioClip>();
        yield return _currentMusicOperationHandler;
        _musicResult = _currentMusicOperationHandler.Result as AudioClip;
    }

    private IEnumerator PlayMusicMix()
    {
        int index = Random.Range(0, _musicGameMix.Count);
        yield return SetMusicInternal(index);
        musicSources[0].clip = _musicResult;
        musicSources[0].Play();
        while (musicSources[0].isPlaying)
            yield return null;
        StartCoroutine(PlayMusicMix());
    }

    public void RemoveSource(AudioSource source, SoundType type)
    {
        if (type == SoundType.Music)
            musicSources.Remove(source);
        else if (type == SoundType.Sound)
            _soundSources.Remove(source);
    }

    private void UpdateSourcesVol()
    {
        AudioListener.volume = AudioSettings.GeneralVolume;
        if (musicSources != null)
        {
            foreach (var source in musicSources)
                source.volume = AudioSettings.MusicVolume;
        }
        if (_soundSources != null)
        {
            foreach (var source in _soundSources)
                source.volume = AudioSettings.SoundsVolume;
        }
    }

    public void ChangeVolume(float gen, float mus, float snd)
    {
        AudioSettings.GeneralVolume = gen;
        AudioSettings.MusicVolume = mus;
        AudioSettings.SoundsVolume = snd;
        UpdateSourcesVol();
    }

    //public void ToggleMenuMusic(bool state)
    //{
    //    menuMusic.volume = AudioSettings.MusicVolume * AudioSettings.GeneralVolume;
    //    if (state)
    //        menuMusic.Play();
    //    else
    //        menuMusic.Stop();
    //}
}
