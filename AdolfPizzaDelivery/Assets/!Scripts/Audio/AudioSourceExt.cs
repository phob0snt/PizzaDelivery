using Enums;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceExt : MonoBehaviour
{
    [SerializeField] private SoundType _soundType;

    private void OnEnable()
    {
        AudioManager.Instance.AddSource(GetComponent<AudioSource>(), _soundType);
    }

    private void OnDisable()
    {
        if (AudioManager.Instance !=  null)
            AudioManager.Instance.RemoveSource(GetComponent<AudioSource>(), _soundType);
    }
}
