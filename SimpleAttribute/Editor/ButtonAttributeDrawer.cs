using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

[CustomPropertyDrawer( typeof ( ButtonAttribute ) )]
public class ButtonAttributeDrawer : PropertyDrawer
{
	static System.Reflection.BindingFlags BindFlags = 
		System.Reflection.BindingFlags.Public |
		System.Reflection.BindingFlags.NonPublic |
			System.Reflection.BindingFlags.Instance |
			System.Reflection.BindingFlags.Static;

	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		//var attr = (ButtonAttribute)attribute;
		var attrs = fieldInfo.GetCustomAttributes(true).OfType<ButtonAttribute>().ToList();
		float yMax = 0;
		int i=-1;
		foreach (var attr in attrs) {
			i++;
			var p1 = position;
			p1.yMin += 2;
			p1.width = position.width / attrs.Count;
			p1.x += p1.width * i;
			p1.width -= 2;
			p1.height = GUI.skin.button.CalcHeight(GUIContent.none, 1);
			yMax = Mathf.Max(yMax, p1.yMax);
			if (GUI.Button(p1, attr.label)) {
				var types = (attr.param == null) ? new System.Type[]{} : attr.param.Select(e => e.GetType()).ToArray();
				foreach (var obj in property.serializedObject.targetObjects) {
					var method = obj.GetType().GetMethod(attr.methodName, BindFlags, null, types, null);
					if (method == null) continue;
					var o = (method.IsStatic) ? null : obj;
					Undo.RecordObject(obj, attr.label+" Button");
					var ret = method.Invoke(o, attr.param);
					if (method.ReturnType != typeof(void)) {
						Debug.Log (attr.methodName+"(): "+ret.ToString());
					}
				}
			}
		}


		var p2 = position;
		p2.yMin = yMax + 2;
		EditorGUI.PropertyField(p2, property, label, true);
		property.serializedObject.ApplyModifiedProperties();
	}
	public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
		return 
			GUI.skin.button.CalcHeight(GUIContent.none, 1) +
				EditorGUI.GetPropertyHeight(property) +
				+ 4;
	}
}
