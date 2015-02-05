using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

[CustomPropertyDrawer( typeof ( DisplayConditionAttribute ) )]
public class DisplayConditionAttributeDrawer : PropertyDrawer
{
	static System.Reflection.BindingFlags BindFlags = 
		System.Reflection.BindingFlags.Public |
			System.Reflection.BindingFlags.NonPublic |
			System.Reflection.BindingFlags.Instance |
			System.Reflection.BindingFlags.Static;

	static bool GetBoolean(object o, System.Type t, string name) {
		bool eq = false;
		while (!string.IsNullOrEmpty(name) && name.StartsWith("!")) {
			name = name.Substring(1);
			eq = !eq;
		}
		var b = GetBooleanProc(o, t, name);
		return b != eq;
	}
	static bool GetBooleanProc(object o, System.Type t, string name) {
		var field = t.GetField(name, BindFlags);
		if (field != null) {
			if (field.IsStatic) o = null;
			if (System.Convert.ToBoolean(field.GetValue(o)))
				return true;
			return false;
		}
		var prop = t.GetProperty(name, BindFlags);
		if (prop != null) {
			if (System.Convert.ToBoolean(prop.GetValue(o, null)))
				return true;
			return false;
		}
		var method = t.GetMethod(name, BindFlags);
		if (method != null) {
			if (method.IsStatic) o = null;
			if (System.Convert.ToBoolean(method.Invoke(o, null)))
				return true;
			return false;
		}
		return false;
	}

	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		var attr = fieldInfo.GetCustomAttributes(true).OfType<DisplayConditionAttribute>().First();
		var o = property.serializedObject.targetObject;
		var t = o.GetType();

		if (GetBoolean(o, t, attr.getter))
			EditorGUI.PropertyField(position, property, label);

	}
	public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
		var attr = fieldInfo.GetCustomAttributes(true).OfType<DisplayConditionAttribute>().First();
		var o = property.serializedObject.targetObject;
		var t = o.GetType();

		if (GetBoolean(o, t, attr.getter))
			return EditorGUI.GetPropertyHeight(property);
		else
			return -2;
	}
}


