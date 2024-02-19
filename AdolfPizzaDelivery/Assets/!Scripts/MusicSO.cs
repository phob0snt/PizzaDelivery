using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName="MusicContainer")]
public class MusicSO : ScriptableObject
{
    public List<AssetReference> musicMix = new();
    public AudioClip mainMenuMusic;
}
