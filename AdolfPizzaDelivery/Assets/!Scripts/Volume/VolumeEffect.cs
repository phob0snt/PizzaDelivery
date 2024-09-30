using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "VolumeEffect")]
public class VolumeEffect : ScriptableObject
{
    [Header("Bloom settings")]
    public float BloomThreshold = 0;
    public float BloomIntensity = 0;
    public Color BloomTintColor = new();

    [Header("Chromatic Abberation settings")]
    [Range(0, 1)]
    public float ChromAbberIntensity = 0;

    [Header("Color Curves settings")]
    [Header("Set length according on your amount of keys on curve.\nAmount of keys must be equal to keys amount on volume ColorCurves")]
    public TextureCurve ColorCurvesMasterCurve;

    [Header("Lens Distortion settings")]
    [Range(-1, 1)]
    public float LensDistortionIntensity;
}
