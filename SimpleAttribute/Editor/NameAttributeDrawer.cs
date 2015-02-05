using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

[CustomPropertyDrawer( typeof ( NameAttribute ) )]
public class NameAttributeDrawer : PropertyDrawer
{
	static System.Reflection.BindingFlags BindFlags = 
		System.Reflection.BindingFlags.Public |
			System.Reflection.BindingFlags.NonPublic |
			System.Reflection.BindingFlags.Instance |
			System.Reflection.BindingFlags.Static;
	
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		var attr = fieldInfo.GetCustomAttributes(true).OfType<NameAttribute>().First();
		var o = property.serializedObject.targetObject;
		var t = o.GetType();
		var method = t.GetMethod(attr.getter, BindFlags);
		var s = method.Invoke(o, new object[]{}) as string;
		EditorGUI.PropertyField(position, property, new GUIContent(s));
	}
	public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
		return EditorGUI.GetPropertyHeight(property);
	}
}


