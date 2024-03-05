using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Volume))]
public class VolumeController : MonoBehaviour
{
    [SerializeField] private MushroomEffect effect;
    private Volume _volume;
    private ColorCurves _volumeCurves;
    private readonly float _curvesInterpTime = 15;

    private void Awake()
    {
        _volume = GetComponent<Volume>();
    }
    public void SetEffect()
    {
        Bloom volumeBloom = _volume.profile.components.Find(x => x is Bloom) as Bloom;
        volumeBloom.tint.Override(effect.BloomTintColor);
        volumeBloom.intensity.Override(effect.BloomIntensity);
        volumeBloom.threshold.Override(effect.BloomThreshold);
        _volumeCurves = _volume.profile.components.Find(x => x is ColorCurves) as ColorCurves;
        StartCoroutine(SwingCurves());

    }

    private IEnumerator SwingCurves()
    {
        float interpTime = 0.1f;
        if (!_volumeCurves.master.overrideState)
            _volumeCurves.master.overrideState = true;
        TextureCurve startValue = _volumeCurves.master.value;
        if (startValue.length == 2)
            startValue.AddKey(0.5f, 0.5f);
        float startKeyValue = startValue[1].value;
        while (interpTime < _curvesInterpTime)
        {
            Keyframe currFrame = new();
            currFrame.time = effect.ColorCurvesMasterCurve[1].time;
            currFrame.value = Mathf.Lerp(startKeyValue, effect.ColorCurvesMasterCurve[1].value, interpTime / _curvesInterpTime);
            startValue.MoveKey(1, currFrame);
            _volumeCurves.master.Override(startValue);
            interpTime += Time.deltaTime;
            Debug.Log(interpTime / _curvesInterpTime);
            yield return null;
        }
    }
}
