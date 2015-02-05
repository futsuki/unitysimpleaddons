using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SearchWindow : EditorWindow {
	[MenuItem("Window/SearchWindow")]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(SearchWindow));
	}


	GameObject[] gos;
	Vector2 scroll;

	string tag="";
	List<GameObject> tagObjs = new List<GameObject>();
	void DrawTagSearch() {
		EditorGUI.BeginChangeCheck();
		tag = EditorGUILayout.TextField("TagName", tag);
		if (EditorGUI.EndChangeCheck()) {
			var gos = GameObject.FindObjectsOfType<GameObject>();
			tagObjs = gos.Where(e => e.tag.Contains(tag)).ToList();
		}
		scroll = GUILayout.BeginScrollView(scroll, false, true);
		foreach (var go in tagObjs) {
			EditorGUILayout.ObjectField(go, typeof(GameObject), true);
		}
		GUILayout.EndScrollView();
	}

	string[] gridLabels = new string[]{
		"Tag"
	};
	System.Action[] gridActions;

	int gridSelect;

	void OnGUI () {
		if (gridActions == null) {
			gridActions = new System.Action[] {
				DrawTagSearch
			};
		}
		gridSelect = GUILayout.SelectionGrid(gridSelect, gridLabels, 3);
		gridActions[gridSelect].Invoke();
	}
}
