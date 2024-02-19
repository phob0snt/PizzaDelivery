//Name: Robert MacGillivray
//File: PerLayerCullingEditor.cs
//Date: Jun.01.2015

//Last Updated: Jul.01.2022 by Robert MacGillivray

using UnityEngine;
using UnityEditor;

namespace UmbraEvolution.PerLayerCameraCulling
{
    /// <summary>
    /// Used to make the PerLayerCulling component more user-friendly in the Unity Editor.
    /// Also adds a button to reset and lock culling distances to the current default.
    /// </summary>
    [CustomEditor(typeof(PerLayerCulling))]
    public class PerLayerCullingEditor : Editor
    {
        // Reference to the PerLayerCulling script that this editor script is attached to
        private PerLayerCulling _perLayerCulling;

        private void OnEnable()
        {
            // Collect a reference to the script so that we can modify values if necessary
            _perLayerCulling = (PerLayerCulling)target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // creates an info box in the inspector to explain something important
            EditorGUILayout.HelpBox("Note: Editing these values via scripting won't update the camera settings unless PerLayerCulling.TriggerUpdate() " +
                "is also called from your script. Editing them via the inspector, however, automatically calls PerLayerCulling.TriggerUpdate().", MessageType.Info);

            // draws the default inspector that would normally be generated automatically
            DrawDefaultInspector();

            // create a button that, when pressed, triggers a method in the script that resets all layers to the current default
            GUIContent resetDefaultsButton = new GUIContent("Reset All to Default", "Press this button to reset all layers to the default culling distance");
            if (GUILayout.Button(resetDefaultsButton))
            {
                // record this action so that the scene is marked as dirty and we can use an undo operation on it
                Undo.RecordObject(_perLayerCulling, "Reset All to Default");
                _perLayerCulling.ResetAllToDefault();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
