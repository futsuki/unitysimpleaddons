using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

static public class CreateScriptableAsset
{
	static public T InstantiateScriptableObject<T>(string name, string outputdirectory="Assets")
		where T : ScriptableObject
	{
		return (T)InstantiateScriptableObject(typeof(T), name, outputdirectory);
	}
	static public ScriptableObject InstantiateScriptableObject(System.Type type, string name, string outputdirectory="Assets")
	{
		string path = getSavePath2(outputdirectory, name);
		var o = ScriptableObject.CreateInstance(type);
		AssetDatabase.CreateAsset (o, path);
		
		ScriptableObject sobj = AssetDatabase.LoadAssetAtPath (path, typeof(ScriptableObject)) as ScriptableObject;
		AssetDatabase.SetLabels (sobj, labels);
		EditorUtility.SetDirty (sobj);
		
		return sobj;
	}

	static string getSavePath2 (string dir, string file)
	{
		string objectName = file;
		if (dir[dir.Length-1] == '/') dir = dir.Substring(0, dir.Length-1);
		string dirPath = dir;
		string path = string.Format ("{0}/{1}.asset", dirPath, objectName);
		
		if (File.Exists (path))
		for (int i=1;; i++) {
			path = string.Format ("{0}/{1}({2}).asset", dirPath, objectName, i);
			if (! File.Exists (path))
				break;
		}
		
		return path;
	}

	readonly static string[] labels = {"Data", "ScriptableObject"};
	
	[MenuItem("Assets/Instantiate Asset")]
	static void Crate ()
	{
		foreach (Object selectedObject in Selection.objects) {
			// get path
			string path = getSavePath (selectedObject);
			if (selectedObject is MonoScript) {
				CreateByMonoScript((MonoScript)selectedObject, path);
			}
			else {
				Debug.Log (selectedObject.name+" is not MonoScript: typeof "+selectedObject.GetType());
			}
		}
	}

	public static Object CreateByMonoScript (MonoScript ms, string path)
	{
		// create instance
		ScriptableObject obj = ScriptableObject.CreateInstance (ms.name);
		if (obj == null) {
			var go = new GameObject(ms.name);
			go.AddComponent(ms.name);
			path = System.IO.Path.ChangeExtension(path, "prefab");
			path = AssetDatabase.GenerateUniqueAssetPath(path);
			var preCreated = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
			if (preCreated == null)
				preCreated = PrefabUtility.CreateEmptyPrefab(path);
			PrefabUtility.ReplacePrefab(go, preCreated, ReplacePrefabOptions.ReplaceNameBased);
			GameObject.DestroyImmediate(go);
			EditorApplication.RepaintHierarchyWindow();
		}
		else {
			path = AssetDatabase.GenerateUniqueAssetPath(path);
			AssetDatabase.CreateAsset (obj, path);
		}
		
		// add label
		var sobj = AssetDatabase.LoadAssetAtPath (path, typeof(Object)) as Object;
		AssetDatabase.SetLabels (sobj, labels);
		EditorUtility.SetDirty (sobj);
		AssetDatabase.Refresh();
		return sobj;
	}

	[MenuItem("Assets/Duplicate ScriptableObject")]
	static void Duplicat ()
	{
		foreach (Object selectedObject in Selection.objects) {
			// get path
			string path = getSavePath (selectedObject);

			if (selectedObject is ScriptableObject) {
				// create instance
				ScriptableObject obj = ScriptableObject.CreateInstance (selectedObject.GetType());
				EditorUtility.CopySerialized(selectedObject, obj);
				AssetDatabase.CreateAsset (obj, path);
				
				// add label
				ScriptableObject sobj = AssetDatabase.LoadAssetAtPath (path, typeof(ScriptableObject)) as ScriptableObject;
				AssetDatabase.SetLabels (sobj, labels);
				EditorUtility.SetDirty (sobj);
			}
			else {
				Debug.Log (selectedObject.name+" is not ScriptableObject: typeof "+selectedObject.GetType());
			}
		}
	}
	
	static string getSavePath (Object selectedObject)
	{
		string objectName = selectedObject.name;
		string dirPath = Path.GetDirectoryName (AssetDatabase.GetAssetPath (selectedObject));
		string path = string.Format ("{0}/{1}.asset", dirPath, objectName);
		
		if (File.Exists (path))
		for (int i=1;; i++) {
			path = string.Format ("{0}/{1}({2}).asset", dirPath, objectName, i);
			if (! File.Exists (path))
				break;
		}
		
		return path;
	}
}