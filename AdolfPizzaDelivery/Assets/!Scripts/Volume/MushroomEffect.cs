using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(menuName = "VolumeEffects/MushroomEffect")]
public class MushroomEffect : VolumeEffect
{
    [Header("Bloom settings")]
    public float BloomThreshold = 0;
    public float BloomIntensity = 0;
    public Color BloomTintColor = new();

    [Header("Chromatic Abberation settings")]
    public float ChromAbberIntensity = 0;

    [Header("Color Curves settings")]
    public TextureCurve ColorCurvesMasterCurve;

}
