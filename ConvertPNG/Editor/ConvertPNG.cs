using UnityEngine;
using UnityEditor;
using System.Collections;

public class ConvertPNGWindow : EditorWindow {
	[MenuItem("Window/PNG Converter")]
	public static void Open() {
		EditorWindow.GetWindow<ConvertPNGWindow>("PNG Converter", true);
	}

	public Texture2D source;

	void OnGUI() {
		source = EditorGUILayout.ObjectField("Source Texture", source, typeof(Texture2D), true) as Texture2D;

		if (GUILayout.Button("Create")) {
			make ();
		}
	}

	void make() {
		var copy = CopyTexture(source);
		var path = AssetDatabase.GetAssetPath(source);
		var f = System.IO.Path.GetFileNameWithoutExtension(path)+"_png.png";
		path = System.IO.Path.GetDirectoryName(path)+"/"+f;
		Debug.Log ("save to "+path);
		System.IO.File.WriteAllBytes(path, copy.EncodeToPNG());
	}

	static public Texture2D CopyTexture(Texture2D tex) {
		var r = GetReadable(tex);
		SetReadable(tex, true);
		Texture2D nt;
		try {
			nt = new Texture2D(tex.width, tex.height, TextureFormat.RGBA32, false);
			nt.SetPixels32(tex.GetPixels32());
		}
		finally {
			SetReadable(tex, r);
		}
		return nt;
	}

	static public bool GetReadable(Texture2D tex) {
		var path = AssetDatabase.GetAssetPath(tex);
		TextureImporter ti = (TextureImporter) TextureImporter.GetAtPath(path);
		return ti.isReadable;
	}
	static public void SetReadable(Texture2D tex, bool val) {
		var path = AssetDatabase.GetAssetPath(tex);
		TextureImporter ti = (TextureImporter) TextureImporter.GetAtPath(path);
		if (ti.isReadable != val) {
			ti.isReadable = val;
			AssetDatabase.ImportAsset(path);
		}
	}
}
