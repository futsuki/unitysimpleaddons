using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(AnimationMirrorAsset))]
public class AnimationMirrorAssetEditor : Editor {

	/*
	static void CreateAsset()
	{
		var listOfBase = ScriptableObject.CreateInstance ();
		AssetDatabase.CreateAsset(listOfBase, "Assets/ListOfBase.asset");
		AssetDatabase.SaveAssets();
	}
	
	static void CreateSubAsset()
	{
		if (container == null)
			CreateAsset ();
		
		var subAsset = ScriptableObject.CreateInstance ();
		subAsset.name = "Base Sub Asset";
		AssetDatabase.AddObjectToAsset (subAsset, container);
		AssetDatabase.SaveAssets();
		
		//just to show that is working, add the subasset to listOfBase in Container and add a new ListOfBase with the subasset to multilist.
		container.listOfBase.Add (subAsset);
		ListOfBase list = new ListOfBase();
		list.Add(subAsset);
		container.multiList.Add(list);
	}
	*/



	void OnEnable() {

		//serializedObject.FindProperty
	}

	public override void OnInspectorGUI ()
	{
		var asset = (AnimationMirrorAsset)target;
		asset.transform.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
		SerializedProperty clipsProp, mirrorSourceProp;
		clipsProp = serializedObject.FindProperty("clips");
		mirrorSourceProp = serializedObject.FindProperty("mirrorSource");

		bool modifyAsset = false;

		if (GUILayout.Button("Update")) {
			foreach (var anim in ArrayElements(clipsProp)) {
				Object.DestroyImmediate(anim.objectReferenceValue, true);
			}
			modifyAsset = true;
			asset.clips.Clear();
			EditorUtility.SetDirty(asset);

			foreach (var anim in ArrayElements(mirrorSourceProp)) {
				if (anim.objectReferenceValue is AnimationClip) {
					var ac = ClipMakeMirror(anim.objectReferenceValue as AnimationClip, new Vector3(1, 1, -1));
					AssetDatabase.AddObjectToAsset(ac, target);
					AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(ac));
					asset.clips.Add(ac);
				}
			}
			EditorUtility.SetDirty(asset);
			
			/*
			//var thisasset = AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(this), typeof(AnimationMirrorAsset)) as AnimationMirrorAsset;
			var ac = new AnimationClip();
			ac.name = "sss";
			AssetDatabase.AddObjectToAsset(ac, target);
AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(ac));
var clips = serializedObject.FindProperty("clips");
			clips.InsertArrayElementAtIndex(clips.arraySize);
			var elem = clips.GetArrayElementAtIndex(clips.arraySize-1);
			elem.objectReferenceValue = ac;
			serializedObject.ApplyModifiedProperties();
			*/
		}
		
		base.OnInspectorGUI();
		serializedObject.ApplyModifiedProperties();
		if (modifyAsset) {
			AssetDatabase.SaveAssets();
		}
	}

	AnimationClip ClipMakeMirror(AnimationClip srcclip, Vector3 sign) {
		var clip = new AnimationClip();
		clip.name = "Mirrored" + srcclip.name;
		AnimationUtility.SetAnimationType(clip, ModelImporterAnimationType.Generic);
		var bnd = AnimationUtility.GetCurveBindings(srcclip);
		int i=0;
		foreach (var b in bnd) {
			//if (i++ > 10) break;
			var curve = AnimationUtility.GetEditorCurve(srcclip, b);
			if (b.propertyName == "m_LocalPosition.y") {
				curve = 
					CurveMakeMirror(b, curve, sign.z);
			}
			AnimationUtility.SetEditorCurve(clip, b, curve);
			//clip.SetCurve(b.path, b.type, b.propertyName, curve);
		}
		//AnimationUtility.GetAllCurves
		var c = AnimationUtility.GetAnimationClipSettings(clip);
		c.loopTime = true;
		EditorUtility.SetDirty(clip);
		return clip;
		//bnd[0].
		//AnimationUtility.SetObjectReferenceCurve(clip);
		//AnimationUtility.GetObjectReferenceCurve(clip);
	}
	AnimationCurve CurveMakeMirror(EditorCurveBinding bind, AnimationCurve curve, float scale) {
		var newArr = curve.keys.Select(e => {
			e.inTangent *= scale;
			e.outTangent *= scale;
			e.value *= scale;
			return e;
		}).ToArray();
		return new AnimationCurve(newArr);
	}

	static IEnumerable<SerializedProperty> ArrayElements(SerializedProperty prop) {
		for (int i=0; i<prop.arraySize; i++) {
			yield return prop.GetArrayElementAtIndex(i);
		}
	}
}
