using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Volume))]
public class VolumeController : MonoBehaviour
{
    [SerializeField] private VolumeEffect effect;
    private Volume _volume;
    private readonly float _effectInterpTime = 60;
    private readonly float _effectDuration = 320;

    private bool _curvesActive;
    private bool _bloomActive;
    private bool _chromActive;
    private bool _lensActive;


    private void Awake()
    {
        _volume = GetComponent<Volume>();
    }
    public void SetEffect()
    {
        StartCoroutine(SwingLensDist(effect.LensDistortionIntensity));
        StartCoroutine(SwingChromAbber(effect.ChromAbberIntensity));
        StartCoroutine(SwingBloom(effect.BloomTintColor, effect.BloomIntensity, effect.BloomThreshold));
        StartCoroutine(SwingCurves(effect.ColorCurvesMasterCurve));
    }

    public void DarkenScreen(float duration)
    {
        Vignette vignette = _volume.profile.components.Find(x => x is Vignette) as Vignette;
        vignette.center.overrideState = true;
        vignette.intensity.overrideState = true;
        vignette.center = new Vector2Parameter(new Vector2(1.5f, 0.5f));
        StartCoroutine(DarkenProcess(duration, vignette));
    }

    private IEnumerator DarkenProcess(float duration, Vignette vignette)
    {
        float interpTime = 0f;
        while (interpTime < duration)
        {
            vignette.intensity.value = Mathf.Lerp(0, 1, interpTime / duration);
            interpTime += Time.deltaTime;
            yield return null;
        }
        interpTime = 0f;
        while (interpTime < duration)
        {
            vignette.intensity.value = Mathf.Lerp(1, 0, interpTime / duration);
            interpTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator SwingCurves(TextureCurve curve)
    {
        ColorCurves _volumeCurves = _volume.profile.components.Find(x => x is ColorCurves) as ColorCurves;
        if (curve.length != _volumeCurves.master.value.length)
        {
            Debug.LogError("Effect ColorCurve must have the same amount of keys as Volume ColorCurve");
            yield break;
        }
        float interpTime = 0f;
        if (!_volumeCurves.master.overrideState)
            _volumeCurves.master.overrideState = true;
        TextureCurve startCurve = _volumeCurves.master.value;

        Keyframe[] startKeyFrames = { };
        for (int i = 0; i < _volumeCurves.master.value.length; i++)
        {
            startKeyFrames = startKeyFrames.Append(startCurve[i]).ToArray();
        }
        TextureCurve defaultCurve = new TextureCurve(startKeyFrames, 0, false, new Vector2());
        while (interpTime < _effectInterpTime)
        {
            for (int i = 0; i < _volumeCurves.master.value.length; i++)
            {
                Keyframe currKey = new()
                {
                    time = Mathf.Lerp(startKeyFrames[i].time, curve[i].time, interpTime / _effectInterpTime),
                    value = Mathf.Lerp(startKeyFrames[i].value, curve[i].value, interpTime / _effectInterpTime),
                    inTangent = Mathf.Lerp(startKeyFrames[i].inTangent, curve[i].inTangent, interpTime / _effectInterpTime),
                    outTangent = Mathf.Lerp(startKeyFrames[i].outTangent, curve[i].outTangent, interpTime / _effectInterpTime)
                };
                _volumeCurves.master.value.MoveKey(i, currKey);
            }
            _volumeCurves.master.Override(startCurve);
            interpTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(_effectDuration);
        if (!_curvesActive)
        {
            _curvesActive = true;
            StartCoroutine(SwingCurves(defaultCurve));
        }
        else
            yield break;
    }

    private IEnumerator SwingBloom(Color tint, float intens, float thresold)
    {
        float interpTime = 0;
        Bloom volumeBloom = _volume.profile.components.Find(x => x is Bloom) as Bloom;
        if (!volumeBloom.tint.overrideState)
            volumeBloom.tint.overrideState = true;
        if (!volumeBloom.intensity.overrideState)
            volumeBloom.intensity.overrideState = true;
        if (!volumeBloom.threshold.overrideState)
            volumeBloom.threshold.overrideState = true;
        Color startTintValue = volumeBloom.tint.value;
        float startIntensValue = volumeBloom.intensity.value;
        float startThresoldValue = volumeBloom.threshold.value;
        while (interpTime < _effectInterpTime)
        {
            volumeBloom.tint.Interp(startTintValue, tint, interpTime / _effectInterpTime);
            volumeBloom.intensity.Interp(startIntensValue, intens, interpTime / _effectInterpTime);
            volumeBloom.threshold.Interp(startThresoldValue, thresold, interpTime / _effectInterpTime);
            interpTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(_effectDuration);
        if (!_bloomActive)
        {
            _bloomActive = true;
            StartCoroutine(SwingBloom(startTintValue, startIntensValue, startThresoldValue));
        }
        else
            yield break;
    }
    
    private IEnumerator SwingChromAbber(float intens)
    {
        float interpTime = 0;
        ChromaticAberration chromAbber = _volume.profile.components.Find(x => x is ChromaticAberration) as ChromaticAberration;
        if (!chromAbber.intensity.overrideState)
            chromAbber.intensity.overrideState = true;
        float startIntensValue = chromAbber.intensity.value;
        while (interpTime < _effectInterpTime)
        {
            chromAbber.intensity.Interp(startIntensValue, intens, interpTime / _effectInterpTime);
            interpTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(_effectDuration);
        if (!_chromActive)
        {
            _chromActive = true;
            StartCoroutine(SwingChromAbber(startIntensValue));
        }
        else
            yield break;
    }

    private IEnumerator SwingLensDist(float intens)
    {
        float interpTime = 0;
        LensDistortion distortion = _volume.profile.components.Find(x => x is LensDistortion) as LensDistortion;
        if (!distortion.intensity.overrideState)
            distortion.intensity.overrideState = true;
        float startIntensValue = distortion.intensity.value;
        while (interpTime < _effectInterpTime)
        {
            distortion.intensity.Interp(startIntensValue, intens, interpTime / _effectInterpTime);
            interpTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(_effectDuration);
        if (!_lensActive)
        {
            _lensActive = true;
            StartCoroutine(SwingLensDist(startIntensValue));
        }
        else
            yield break;
    }
}
