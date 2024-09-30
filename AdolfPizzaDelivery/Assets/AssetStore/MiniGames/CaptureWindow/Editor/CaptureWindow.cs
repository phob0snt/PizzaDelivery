/*
 * Copyright (c) 2023 MiniGames
 *
 * Check out how to use it here.
 * https://www.youtube.com/channel/UCrLZAN_rgpW7i84gDAHHH1g
 *
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the
 * "Software"), to deal in the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to
 * permit persons to whom the Software is furnished to do so, subject to
 * the following conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
 * CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

#if UNITY_EDITOR

using System.IO;
using UnityEngine;

namespace Tiny
{
	using UnityEditor;
    public class CaptureWindow : EditorWindow
    {
        [MenuItem("Window/Tiny/Mini Capture", false, 500)]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow<CaptureWindow>();
        }

		Vector2Int size = new Vector2Int(512, 512);

		public enum AntiAliasing{
			_Off = 1,
			_2 = 2,
			_4 = 4,
			_8 = 8
		};

		int scale = 1;
		AntiAliasing antiAliasing = AntiAliasing._8;

		int gameScale = 1;
		AntiAliasing gameAntiAliasing = AntiAliasing._8;
		bool transparency = false;
		bool ui = false;

		private void OnEnable()
        {
            if (!SceneView)
                return;
			Vector2 size = SceneView.position.size;
			this.size.x = (int)size.x;
			this.size.y = (int)size.y;			      
        }

        SceneView SceneView{ get{ return SceneView.currentDrawingSceneView ? SceneView.currentDrawingSceneView : (SceneView.lastActiveSceneView ? SceneView.lastActiveSceneView : null); } }

        private void OnGUI()
        {
			EditorGUILayout.HelpBox("Project settings may be required when using anti-aliasing.", MessageType.Info);

			EditorGUILayout.LabelField("Export Path");
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(EditorPrefs.GetString("SceneCapture/ExportPath", Directory.GetCurrentDirectory()));
			if (GUILayout.Button("...", GUILayout.Width(25f)))
			{
				string path = EditorUtility.SaveFolderPanel("Export Directory", EditorPrefs.GetString("SceneCapture/ExportPath", Directory.GetCurrentDirectory()), "");

				if (!string.IsNullOrEmpty(path))
					EditorPrefs.SetString("SceneCapture/ExportPath", path);
			}

			if (GUILayout.Button("▲", GUILayout.Width(25f)))
			{
				OpenInFileBrowser.Open(EditorPrefs.GetString("SceneCapture/ExportPath", Directory.GetCurrentDirectory()));
			}

			EditorGUILayout.EndHorizontal();

			EditorGUILayout.LabelField("________________________________________________________________________________________________________________________________________________________________________________");
			EditorGUILayout.LabelField("Scene View");

			size.x = EditorGUILayout.IntField("Width", size.x);
            size.y = EditorGUILayout.IntField("Height", size.y);
			antiAliasing = (AntiAliasing)EditorGUILayout.EnumPopup("AntiAliasing", antiAliasing);

			if (size.x > 0 && size.y > 0 && SceneView)
			{
				if (GUILayout.Button("Capture"))
				{
					string path = ExportPath;
					if (string.IsNullOrEmpty(path))
						return;
					Texture2D texture = SaveCapture(SceneView.camera, size, false, (int)antiAliasing);

					string filePath = Path.Combine(path, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png");
					File.WriteAllBytes(filePath, texture.EncodeToPNG());

					DestroyImmediate(texture);
					AssetDatabase.Refresh();

					Debug.Log(filePath + " - Captured.");
				}
			}

			GUILayout.Space(5f);

			EditorGUILayout.LabelField("________________________________________________________________________________________________________________________________________________________________________________");
			EditorGUILayout.LabelField("Game View");

			gameScale = Mathf.Max(1, EditorGUILayout.IntField("Scale", gameScale));
			ui = EditorGUILayout.Toggle("Includes UI", ui);
			if (!ui)
			{
				transparency = EditorGUILayout.Toggle("Transparency", transparency);
				gameAntiAliasing = (AntiAliasing)EditorGUILayout.EnumPopup("AntiAliasing", gameAntiAliasing);
			}			

			if (GUILayout.Button("Capture"))
			{
				string filePath = ExportPath;
				if (string.IsNullOrEmpty(filePath))
					return;

				filePath = Path.Combine(filePath, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png");

				if (ui)
				{
					ScreenCapture.CaptureScreenshot(filePath, scale);
				}
				else
				{
					Camera main = Camera.main;
					if (!main)
					{
						Debug.LogError("Main camera not found.");
						return;
					}

					Vector2 viewSize = Handles.GetMainGameViewSize() * gameScale;
					Vector2Int size = new Vector2Int((int)viewSize.x, (int)viewSize.y);

					Texture2D texture = SaveCapture(main, size, transparency, (int)gameAntiAliasing);
					
					File.WriteAllBytes(filePath, texture.EncodeToPNG());

					DestroyImmediate(texture);
				}

				AssetDatabase.Refresh();
				Debug.Log(filePath + " - Captured.");
			}
		}

        string ExportPath {
            get{
                string path = EditorPrefs.GetString("SceneCapture/ExportPath", Directory.GetCurrentDirectory());
				if(string.IsNullOrEmpty(path))
					path = EditorUtility.SaveFolderPanel("Export Directory", path, "");

				if (string.IsNullOrEmpty(path))
					return string.Empty;

                EditorPrefs.SetString("SceneCapture/ExportPath", path);

                if(!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                return path;
            } 
        }

		private Texture2D SaveCapture(Camera camera, Vector2Int size, bool transparency, int antiAliasing)
        {
            RenderTexture rt = new RenderTexture(size.x, size.y, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB) { useMipMap = false, antiAliasing = antiAliasing };

            RenderTexture currentActiveRT = RenderTexture.active;
            RenderTexture.active = rt;
            Texture2D texture = new Texture2D(size.x, size.y, TextureFormat.ARGB32, false, false);
            var targetTexture = camera.targetTexture;
            camera.targetTexture = rt;

			if (!transparency)
			{
				camera.Render();
				texture.ReadPixels(new Rect(0, 0, size.x, size.y), 0, 0);

				camera.targetTexture = targetTexture;
				RenderTexture.active = currentActiveRT;
				DestroyImmediate(rt);
				return texture;
			}

			Color background = camera.backgroundColor;
			CameraClearFlags clearFlags = camera.clearFlags;
            camera.backgroundColor = Color.black;
			camera.clearFlags = CameraClearFlags.SolidColor;

			camera.Render();
            texture.ReadPixels(new Rect(0, 0, size.x, size.y), 0, 0);
            Color32[] blackPixels = texture.GetPixels32();

            camera.backgroundColor = Color.white;
            camera.Render();
            texture.ReadPixels(new Rect(0, 0, size.x, size.y), 0, 0);
            Color32[] whitePixels = texture.GetPixels32();

            for (int i = -1; ++i < blackPixels.Length;)
                blackPixels[i].a = (byte)(255 - (whitePixels[i].r - blackPixels[i].r));
            texture.SetPixels32(blackPixels);

            camera.targetTexture = targetTexture;
            camera.backgroundColor = background;
			camera.clearFlags = clearFlags;
			RenderTexture.active = currentActiveRT;
            DestroyImmediate(rt);
            return texture;
        }
    }
}

#endif