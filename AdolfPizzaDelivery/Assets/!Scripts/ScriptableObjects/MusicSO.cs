using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName="MusicContainer")]
public class MusicSO : ScriptableObject
{
    public List<AssetReference> MusicMix = new();
    public AudioClip MainMenuMusic;
}
