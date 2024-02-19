// Name: Robert MacGillivray
// File: PrefabChecker.cs
// Date: Sept.07.2021

// Last Updated: Sept.07.2021 by Robert MacGillivray

using UnityEngine;
using UnityEditor;

// Part of the complicated nested prefab checks
# if UNITY_2018_3_OR_NEWER

#endif

/// <summary>
/// Simple class to hold an extension method that will determine whether or not a GameObject is instanced in a real scene or not
/// </summary>
public static class PrefabChecker
{
    public static bool IsSceneObject(this GameObject go)
    {
#if UNITY_2018_3_OR_NEWER
        //Complicated check to account for new nested prefabs.
        if ((PrefabUtility.GetPrefabInstanceStatus(go) != PrefabInstanceStatus.NotAPrefab ||
             PrefabUtility.GetPrefabAssetType(go) == PrefabAssetType.NotAPrefab) &&
             UnityEditor.SceneManagement.PrefabStageUtility.GetPrefabStage(go) == null)
#else
            if (PrefabUtility.GetPrefabType(go) != PrefabType.Prefab)
#endif
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
