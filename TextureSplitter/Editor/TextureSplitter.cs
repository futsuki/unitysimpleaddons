using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class TextureSplitter : EditorWindow {
	[MenuItem("Window/Texture Splitter")]
	public static void Open() {
		EditorWindow.GetWindow<TextureSplitter>("TextureSplitter", true);
	}

	Texture2D source;
	int chipx=24, chipy=24;
	int offsetx=0, offsety=0;
	int margin=2;
	bool fillBorder;
	void OnGUI() {
		source = EditorGUILayout.ObjectField("Source Texture", source, typeof(Texture2D), true) as Texture2D;
		chipx = EditorGUILayout.IntField("X Chip Width", chipx);
		chipy = EditorGUILayout.IntField("Y Chip Width", chipy);
		offsetx = EditorGUILayout.IntField("X Chip Offset", offsetx);
		offsety = EditorGUILayout.IntField("Y Chip Offset", offsety);
		margin = EditorGUILayout.IntField("Margin", margin);
		fillBorder = EditorGUILayout.Toggle("Fill Border", fillBorder);

		if (GUILayout.Button("Create")) {
			var file = EditorUtility.SaveFilePanel("Save to", "./Assets", "out.png", "png");
			if (!string.IsNullOrEmpty(file)) {
				make (file);
			}
		}
	}

	void make(string path) {
		var source = this.source;
		source = ConvertPNGWindow.CopyTexture(source);
		var s = source.GetPixels32();
		var newt = new Texture2D(source.width + Mathf.CeilToInt(source.width/chipx) * (margin*2),
		                         source.height + Mathf.CeilToInt(source.height/chipy) * (margin*2));
		var d = newt.GetPixels32().Select(e => new Color32(0, 0, 0, 0)).ToArray();

		for (int y=0; y<source.height; y++) {
			for (int x=0; x<source.width; x++) {
				var inp = y*source.width + x;
				var isLeft = x%chipx == 0;
				var isRight = x%chipx == chipx-1;
				var isDown = y%chipy == 0;
				var isUp = y%chipy == chipy-1;
				var ax = Mathf.FloorToInt(((float)x-offsetx)/chipx) * (margin*2) + margin;
				var ay = Mathf.FloorToInt(((float)y-offsety)/chipy) * (margin*2) + margin;
				Rect fillRect = new Rect(x+ax, y+ay, 1, 1);
				if (inp < 0 || inp >= s.Length) {
					continue;
					//Debug.Log ("inp "+inp+" "+x+","+y);
				}
				if (fillBorder) {
					if (isLeft) fillRect.xMin -= margin;
					if (isRight) fillRect.xMax += margin;
					if (isDown) fillRect.yMin -= margin;
					if (isUp) fillRect.yMax += margin;
				}

				for (int ry=Mathf.RoundToInt(fillRect.yMin); ry<fillRect.yMax; ry++) {
					for (int rx=Mathf.RoundToInt(fillRect.xMin); rx<fillRect.xMax; rx++) {
						var outp = (ry)*newt.width + (rx);
						if (outp < 0 || outp >= d.Length) {
							continue;
							//Debug.Log ("outp "+outp+" "+x+","+y+","+ax+","+ay);
						}
						d[outp] = s[inp];
					}
				}
				//d[outp] = s[inp];
			}
		}
		newt.SetPixels32(d);
		newt.Apply ();


		System.IO.File.WriteAllBytes(path, newt.EncodeToPNG());
	}
}
