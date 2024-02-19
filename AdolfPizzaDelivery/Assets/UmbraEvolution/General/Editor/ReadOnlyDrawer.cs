//Name: Robert MacGillivray
//File: ReadOnlyDrawer.cs
//Date: Apr.21.2015
//Purpose: To create a property drawer for my read only attribute

//Last Updated: Sept.07.2021 by Robert MacGillivray

using UnityEngine;
using UnityEditor;

namespace UmbraEvolution
{
    /// <summary>
    /// A property drawer used by my ReadOnlyInInspector attribute which makes a public property visible, but not editable in the inspector
    /// </summary>
    [CustomPropertyDrawer(typeof(ReadOnlyInInspectorAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // accounts for foldouts on serialized classes
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // disables elements in the gui
            GUI.enabled = false;
            // creates the property, which will be disabled because of the above line
            EditorGUI.PropertyField(position, property, label, true);
            // re-enables the gui so that not all properties are greyed out
            GUI.enabled = true;
        }
    }
}
