using UnityEngine;
using UnityEditor;
using System.Collections;

public class ObjectCopyWindow : EditorWindow {
	[MenuItem ("Window/Object Copy Window")]
	static void Init () {
		// Get existing open window or if none, make a new one:		
		var window = EditorWindow.GetWindow<ObjectCopyWindow>();
	}

	Object source;
	Object[] destination = new Object[0];
	bool clearSourceOnCopy;
	bool[] clearDestOnCopy = new bool[0];


	// Use this for initialization
	void OnGUI() {
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Copy")) {
			for (int i=0; i<destination.Length; i++) {
				var name = destination[i].name;
				EditorUtility.CopySerialized(source, destination[i]);
				destination[i].name = name;
				EditorUtility.SetDirty(destination[i]);
				if (clearDestOnCopy[i]) {
					ArrayUtility.RemoveAt(ref destination, i);
					ArrayUtility.RemoveAt(ref clearDestOnCopy, i);
					i--;
				}
			}
			Repaint();
		}
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Clear")) {
			source = null;
			destination = new Object[0];
		}
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		clearSourceOnCopy = GUILayout.Toggle(clearSourceOnCopy, GUIContent.none, GUILayout.Width(16));
		source = EditorGUILayout.ObjectField("Source", source, typeof(Object), true);
		GUILayout.EndHorizontal();
		for (int i=0; i<destination.Length; i++) {
			GUILayout.BeginHorizontal();
			clearDestOnCopy[i] = GUILayout.Toggle(clearDestOnCopy[i], GUIContent.none, GUILayout.Width(16));
			destination[i] = EditorGUILayout.ObjectField("Destination "+i, destination[i], typeof(Object), true);
			GUILayout.EndHorizontal();
		}
		var o = EditorGUILayout.ObjectField("Destination "+destination.Length, null, typeof(Object), true);
		if (o != null) {
			ArrayUtility.Add(ref destination, o);
			ArrayUtility.Add(ref clearDestOnCopy, true);
		}
	}
}
