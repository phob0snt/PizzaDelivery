using UnityEngine;
using Enums;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceExt : MonoBehaviour
{
    
    [SerializeField] private SoundType soundType;

    private void OnEnable()
    {
        AudioManager.Instance.AddSource(GetComponent<AudioSource>(), soundType);
    }

    private void OnDisable()
    {
        if (AudioManager.Instance !=  null)
            AudioManager.Instance.RemoveSource(GetComponent<AudioSource>(), soundType);
    }
}
