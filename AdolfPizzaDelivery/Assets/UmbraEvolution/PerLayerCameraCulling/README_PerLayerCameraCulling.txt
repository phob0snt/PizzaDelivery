Per-Layer Camera Culling v3.2.3
--------------------------------

-DESCRIPTION-

This asset is intented to let users easily set up different
culling planes for each layer in their project using plane
or spherical distance based culling. This can save resources
in situations where occlusion culling is not appropriate and
when there are large distance disparities between objects.

--------------------------------

-SETUP-

The package is pretty easy to implement. 
All you have to do is grab the script 
"PerLayerCulling" from Assets/UmbraEvolution/PerLayerCameraCulling/Scripts
and drop it onto the camera that you want to use it on.

1) Set the "Default Cull Distance" to a value appropriate to you.
   By default it will match the camera's "Far Clipping Plane" when
   it is initialized.
2) Leave the "Distance Multiplier" at 1.0.
   - Later, you can use this to scale everything up or down quickly,
     without having to edit all of the values.
   - This is particularly useful when trying to adjust performance at runtime
3) Set "Use Spherical Culling" as necessary
   - See https://docs.unity3d.com/ScriptReference/Camera-layerCullSpherical.html
     for more information.
4) Set a custom "Cull Distance" value for any layer you want to deviate
   from the default.
   - You must uncheck "Use Default Cull Distance" to do this.
   - Note that if you increase a specific layer's distance to be larger than the
     default, the camera's Far Clipping Plane will update to match
   - Now when you edit "Default Cull Distance" anything that doesn't have
     "Use Default Cull Distance" checked won't automatically update.
   - You can hit the "Reset All to Default" button at the bottom to quickly
     lock everything back to the "Default Cull Distance"

--------------------------------

- Other Notes -

Changes to the PerLayerCulling component during runtime won't be applied unless
PerLayerCulling.TriggerUpdate() is explicitly called.

By default, gizmos for each layer aren't shown. They must be enabled in the 
"Layer Cull Info Array" if you want to see a visual representation of the gizmo.
You can also force them to be enabled without having to select the camera by
checking the "Always Show Gizmos" box.