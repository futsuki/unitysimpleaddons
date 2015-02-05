using UnityEngine;
using UnityEditor;
using System.Collections;

public class NewGUID {
	[MenuItem("Edit/Copy New GUID")]
	static void CopyGUID () {
		EditorGUIUtility.systemCopyBuffer = System.Guid.NewGuid().ToString();
	}
}
