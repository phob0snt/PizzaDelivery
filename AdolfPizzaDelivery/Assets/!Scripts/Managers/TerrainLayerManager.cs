using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainLayerManager : MonoBehaviour
{
    [SerializeField] private List<Terrain> _terrains;
    private readonly Vector4 _wetMinMaskRemap = new(0.4f, 0, 0, 0.2f);

    public IEnumerator ChangeSurfaceWetness(bool isWet)
    {
        float lerpSeconds = 60;
        float timeElapsed = 0;
        while (timeElapsed < lerpSeconds)
        {
            foreach (var terrain in _terrains)
            {
                TerrainLayer[] tLayers = terrain.terrainData.terrainLayers;
                foreach (var terrainLayer in tLayers)
                {
                    if (isWet)
                    {
                        terrainLayer.maskMapRemapMin = new Vector4(Mathf.Lerp(0, _wetMinMaskRemap.x, timeElapsed / lerpSeconds), 0, 0, Mathf.Lerp(0, _wetMinMaskRemap.w, timeElapsed / lerpSeconds));
                    }
                    else
                    {
                        terrainLayer.maskMapRemapMin = new Vector4(Mathf.Lerp(_wetMinMaskRemap.x, 0, timeElapsed / lerpSeconds), 0, 0, Mathf.Lerp(_wetMinMaskRemap.w, 0, timeElapsed / lerpSeconds));
                    }
                }
            }   
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private void OnApplicationQuit()
    {
        foreach (var terrain in _terrains)
        {
            TerrainLayer[] tLayers = terrain.terrainData.terrainLayers;
            foreach (var terrainLayer in tLayers)
            {
                terrainLayer.maskMapRemapMin = new Vector4(0, 0, 0, 0);
            }
        }
    }
}
