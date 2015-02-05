using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Description))]
public class DescriptionEditor : Editor {
	bool editing=false;


	public override void OnInspectorGUI() {
		var desc = target as Description;
		if (!desc) return;

		if (!editing) {
			GUILayout.BeginHorizontal();
			GUILayout.Label(desc.description??"");
			if (GUILayout.Toggle(false, "edit", EditorStyles.miniButton, GUILayout.Width(40))) {
				editing = true;
			}
			GUILayout.EndHorizontal();
		}
		else {
			GUILayout.BeginHorizontal();
			EditorGUI.BeginChangeCheck();
			desc.description = GUILayout.TextArea(desc.description??"", GUILayout.Height(100));
			if (EditorGUI.EndChangeCheck()) {
				EditorUtility.SetDirty(desc);
			}
			if (!GUILayout.Toggle(true, "edit", EditorStyles.miniButton, GUILayout.Width(40))) {
				editing = false;
			}
			GUILayout.EndHorizontal();
		}
	}
}
