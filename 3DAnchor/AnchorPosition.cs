using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AnchorPosition : MonoBehaviour {
	public static void LoadAnchors() {
		var anchors = Object.FindObjectsOfType<AnchorPosition>();
		currentSceneAnchors = anchors.ToDictionary(e => e.name);
		currentSceneName = Application.loadedLevelName;
	}
	public static AnchorPosition FindAnchor(string name) {
		if (currentSceneName != Application.loadedLevelName) {
			LoadAnchors();
		}
		return currentSceneAnchors[name];
	}
	public static AnchorPosition FindAnchorDynamic(string name) {
		LoadAnchors();
		return currentSceneAnchors[name];
	}
	static Dictionary<string, AnchorPosition> currentSceneAnchors;
	static string currentSceneName;

	public Color color=new Color(1, 1, 0, 0.5f);
	public Vector3 position {
		get {
			return transform.position;
		}
		set {
			transform.position = value;
		}
	}
}
