using UnityEngine;
using System.Collections;

public class PrefabChildren : MonoBehaviour {
	public GameObject[] prefabs;

	// Use this for initialization
	void Awake () {
		if (LevelSerializer.IsDeserializing) return;
		foreach (var p in prefabs) {
			var go = (GameObject)Instantiate(p);
			go.transform.parent = transform;
		}
	}
}
