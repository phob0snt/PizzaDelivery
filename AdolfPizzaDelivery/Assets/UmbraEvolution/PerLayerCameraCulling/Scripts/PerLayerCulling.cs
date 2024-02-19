//Name: Robert MacGillivray
//File: PerLayerCulling.cs
//Date: Apr.18.2015

//Last Updated: Jul.01.2022 by Robert MacGillivray

using UnityEngine;

namespace UmbraEvolution.PerLayerCameraCulling
{
    /// <summary>
    /// Helps users access certain camera properties using the inspector that are typically only accessible via scripting
    /// </summary>
    #if UNITY_2019_1_OR_NEWER
    // conditional compilation for newer versions of Unity that lets us get an OnDestroy callback
    [ExecuteAlways]
    #else
    // conditional compilation for older versions of Unity that lets us get an OnDestroy callback
    [ExecuteInEditMode]
    #endif
    [RequireComponent(typeof(Camera))]
    public class PerLayerCulling : MonoBehaviour
    {
        #region Constants
        // May need to be updated with future versions of Unity if they move the mask up to a 64-bit integer or something
        public const int NUMBER_OF_UNITY_LAYERS = 32;
        public const float NON_ZERO_OFFSET = 0.01f;
        #endregion

        #region Public Fields (Visible to Inspector)
        [Tooltip("Set this as the default distance you want culling to occur across the majority of layers.")]
        [GreaterThanFloat(0, false)]
        public float DefaultCullDistance;

        [Tooltip("A multiplier applied to all distances you've entered for an easy way to modify performance dynamically")]
        [GreaterThanFloat(0, false)]
        public float DistanceMultiplier = 1f;

        [Tooltip("Eliminates the potential for objects to pop in and out of culling range when rotating the camera in place. Has a small performance cost.")]
        public bool UseSphericalCulling;

        [Tooltip("If true, the enabled gizmos for this PerLayerCulling component will always be drawn, even when the camera object isn't selected.")]
        public bool AlwaysShowGizmos = false;

        [Tooltip("In order to work properly, this must always have 32 elements, each corrisponding to the index of your layers. Set each index to set culling distance of a layer. For example: set Element 0 to 1000 and all objects on the default layer will cull at 1000 units from this camera. The layer names should automatically be appropriately assigned for you though, so you don't have to look them up.")]
        public LayerCullInfo[] LayerCullInfoArray = new LayerCullInfo[32];
        #endregion

        #region Read-only Properties
        private Camera _myCamera;
        public Camera MyCamera
        {
            get
            {
                if (_myCamera == null)
                {
                    _myCamera = GetComponent<Camera>();
                    if (_myCamera == null)
                    {
                        Debug.LogError("A PerLayerCulling component requires a sibling Camera component.");
                    }
                }

                return _myCamera;
            }
        }
        #endregion

        #region Unity Methods (Automatically Called)
        /// <summary>
        /// Force OnValidate() to be called at runtime to make sure everything initializes properly
        /// since it may not have been initialized yet
        /// </summary>
        private void Awake()
        {
            OnValidate();
        }

        /// <summary>
        /// Some cleanup for when the script is removed to set everything back to normal
        /// </summary>
        private void OnDestroy()
        {
            // use _myCamera field because the whole GameObject may be being deleted right now,
            // in which case we can't actually reset anything
            if (_myCamera != null)
            {
                _myCamera.layerCullSpherical = false;
                _myCamera.layerCullDistances = new float[NUMBER_OF_UNITY_LAYERS];
            }
        }

        /// <summary>
        /// Makes sure that all values are appropriate and valid. Called automatically in the editor when a value is changed in the inspector.
        /// </summary>
        private void OnValidate()
        {
            // Small initialization check since the DefaultCullDistance can't be zero except when the script is first loaded
            // We don't want to mess with this when validating during gameplay
            if (DefaultCullDistance == 0f)
            {
                DefaultCullDistance = MyCamera.farClipPlane;
            }

            TriggerUpdate();
        }

        /// <summary>
        /// Called when the object is selected in the editor and the editor requests gizmos to be drawn
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            DrawGizmos();
        }

        /// <summary>
        /// Called every time the editor requests gizmos to be drawn
        /// </summary>
        private void OnDrawGizmos()
        {
            if (AlwaysShowGizmos)
            {
                DrawGizmos();
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Called when settings changes need to be validated and applied.
        /// NOTE: Can be used as a way for runtime changes to settings to be applied.
        /// </summary>
        public void TriggerUpdate()
        {
            ValidateAll();
            ApplyCurrentSettings();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Draws a visual representation of layer culling in the editor (as per the show gizmo settings)
        /// </summary>
        private void DrawGizmos()
        {
            foreach (LayerCullInfo cullInfo in LayerCullInfoArray)
            {
                if (cullInfo.ShowGizmo)
                {
                    // aligns the gizmo to the local object's transform
                    Gizmos.matrix = transform.localToWorldMatrix;
                    // sets the gizmo colour as specified in the inspector
                    Gizmos.color = cullInfo.GizmoColor;

                    // draw a camera frustum gizmo to show typical culling bounds
                    Gizmos.DrawFrustum(Vector3.zero, MyCamera.fieldOfView, cullInfo.CullDistance * DistanceMultiplier, MyCamera.nearClipPlane, MyCamera.aspect);

                    // draw a spherical gizmo to show spherical culling bounds
                    if (UseSphericalCulling)
                    {
                        Gizmos.DrawWireSphere(Vector3.zero, cullInfo.CullDistance * DistanceMultiplier);
                    }
                }
            }
        }

        /// <summary>
        /// Validates all settings on this component.
        /// NOTE: Makes changes as necessary rather than simply throwing/logging errors.
        /// </summary>
        private void ValidateAll()
        {
            ValidateLayerCullInfoArray();
            UpdateLayerNames();

            UpdateForDefaultDistanceSetting();

            ValidateAgainstNearClipPlane();
            ValidateAgainstFarClipPlane();
        }

        /// <summary>
        /// Resize the LayerCullInfo array and initialize any layers that may be null as necessary
        /// </summary>
        private void ValidateLayerCullInfoArray()
        {
            //makes sure there are always the right number of elements in the layerCullInfo array
            if (LayerCullInfoArray.Length != NUMBER_OF_UNITY_LAYERS)
            {
                //when the above isn't true, we need to reset the array, but we want to save as much information as possible, so we create a temporary array
                LayerCullInfo[] newLayerCullInfoArray = new LayerCullInfo[NUMBER_OF_UNITY_LAYERS];

                //store all salvageable data in the temporary array
                for (int index = 0; index < LayerCullInfoArray.Length; index++)
                {
                    newLayerCullInfoArray[index] = LayerCullInfoArray[index];
                }

                //copy the temporary array into the regular array so that it now has the right number of elements again
                LayerCullInfoArray = newLayerCullInfoArray;
            }

            // if the array was resized to be smaller than the correct size, fill the remaining slots in the temp array with defaults
            // can also happen if this is the first validation pass and some layers weren't initialized yet
            for (int index = 0; index < NUMBER_OF_UNITY_LAYERS; index++)
            {
                if (LayerCullInfoArray[index] == null)
                {
                    LayerCullInfoArray[index] = new LayerCullInfo();
                    LayerCullInfoArray[index].CullDistance = DefaultCullDistance;
                }
            }
        }

        /// <summary>
        /// Sets the layer names appropriately in the LayerCullInfo array
        /// </summary>
        public void UpdateLayerNames()
        {
            for (int index = 0; index < NUMBER_OF_UNITY_LAYERS; index++)
            {
                if (LayerCullInfoArray[index] != null)
                {
                    if (!string.IsNullOrEmpty(LayerMask.LayerToName(index)))
                    {
                        LayerCullInfoArray[index].LayerName = LayerMask.LayerToName(index);
                    }
                    else
                    {
                        LayerCullInfoArray[index].LayerName = "Layer Not Defined";
                    }
                }
                else
                {
                    Debug.LogError("Null layer in LayerCullInfoArray. Call ValidateLayerCullInfoArray() first, or make sure there are no null layers.");
                }
            }
        }

        /// <summary>
        /// Nothing can be closer to the camera than the near clip plane, so we have to make sure that's true
        /// </summary>
        public void ValidateAgainstNearClipPlane()
        {
            //have to make sure that the default distance is not less than the near clip plane (since nothing should be culled closer than that)
            if (DefaultCullDistance < MyCamera.nearClipPlane)
            {
                DefaultCullDistance = MyCamera.nearClipPlane + NON_ZERO_OFFSET;
            }

            //makes sure the cull distance for each layer isn't smaller than the near clip plane
            foreach (LayerCullInfo cullInfo in LayerCullInfoArray)
            {
                if (cullInfo.CullDistance < MyCamera.nearClipPlane)
                {
                    cullInfo.CullDistance = MyCamera.nearClipPlane + NON_ZERO_OFFSET;
                }
            }

            //Makes sure the distance multiplier won't make any of the cull distances smaller than the near clip plane
            float nearestCullDistance = LayerCullInfoArray[0].CullDistance;
            foreach (LayerCullInfo cullInfo in LayerCullInfoArray)
            {
                if (cullInfo.CullDistance < nearestCullDistance)
                {
                    nearestCullDistance = cullInfo.CullDistance;
                }
            }
            if (nearestCullDistance * DistanceMultiplier < (MyCamera.nearClipPlane + NON_ZERO_OFFSET))
            {
                DistanceMultiplier = (MyCamera.nearClipPlane + NON_ZERO_OFFSET) / nearestCullDistance;
            }
        }

        /// <summary>
        /// Nothing can be farther from the camera than the far clip plane, so we have to make sure that's true
        /// </summary>
        public void ValidateAgainstFarClipPlane()
        {
            //searches the array for the furthest distance
            //culling distances cannot be set to a value larger than the far clip plane on the camera, so we simply increase the far clip plane to match
            float furthestCullDistance = LayerCullInfoArray[0].CullDistance;
            foreach (LayerCullInfo cullInfo in LayerCullInfoArray)
            {
                if (cullInfo.CullDistance > furthestCullDistance)
                {
                    furthestCullDistance = cullInfo.CullDistance;
                }
            }
            MyCamera.farClipPlane = furthestCullDistance * DistanceMultiplier;
        }

        /// <summary>
        /// Runs through the LayerCullInfo array and updates flagged objects to match their CullDistance to the default
        /// </summary>
        public void UpdateForDefaultDistanceSetting()
        {
            foreach (LayerCullInfo cullInfo in LayerCullInfoArray)
            {
                if (cullInfo.UseDefaultCullDistance)
                {
                    cullInfo.CullDistance = DefaultCullDistance;
                }
            }
        }

        /// <summary>
        /// Take the current data from this instance of PerLayerCulling and apply it to the sibling camera
        /// </summary>
        public void ApplyCurrentSettings()
        {
            //we can only assign culling plane distances with a float array, so we'll take the float values from the custom class and put them into one after applying our multiplier
            float[] cullFloatArray = new float[NUMBER_OF_UNITY_LAYERS];
            for (int index = 0; index < NUMBER_OF_UNITY_LAYERS; index++)
            {
                cullFloatArray[index] = LayerCullInfoArray[index].CullDistance * DistanceMultiplier;
            }

            // assigns culling distances to all layers
            MyCamera.layerCullDistances = cullFloatArray;

            // apply the spherical culling setting
            MyCamera.layerCullSpherical = UseSphericalCulling;
        }

        /// <summary>
        /// Resets all culling distances to the current default
        /// </summary>
        public void ResetAllToDefault()
        {
            foreach (LayerCullInfo cullDistance in LayerCullInfoArray)
            {
                cullDistance.UseDefaultCullDistance = true;
                cullDistance.CullDistance = DefaultCullDistance;
            }
        }
        #endregion

        #region Nested Classes
        /// <summary>
        /// Class to hold some info that's relevant to layer-specific culling and related editor functionality
        /// Note: If the first public variable in a serializable class is a string, any public arrays of this class will have
        ///       elements renamed in the inspector. This makes it easy to see what layer you're setting culling distances for in the inspector
        /// </summary>
        [System.Serializable]
        public class LayerCullInfo
        {
            [ReadOnlyInInspector]
            [Tooltip("Layer that these settings apply to.")]
            public string LayerName;

            [Tooltip("If true, editing CullDistance will be useless, since this will always equal the PerLayerCulling default that you set.")]
            public bool UseDefaultCullDistance = true;

            [GreaterThanFloat(0, false)]
            [Tooltip("The distance at which to start culling objects on this layer.")]
            public float CullDistance;

            [Tooltip("If true, the gizmo for this layer can be drawn in the editor.")]
            public bool ShowGizmo = false;

            [Tooltip("The color of the gizmo.")]
            public Color GizmoColor = Color.cyan;
        }
        #endregion
    }
}
