using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CustomPropertyDrawer( typeof ( TagFieldAttribute ) )]
public class TagFieldAttributeDrawer : PropertyDrawer
{
	static System.Reflection.BindingFlags BindFlags = 
		System.Reflection.BindingFlags.Public |
			System.Reflection.BindingFlags.NonPublic |
			System.Reflection.BindingFlags.Instance |
			System.Reflection.BindingFlags.Static;
	
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
		if (property.stringValue == "Untagged") property.stringValue = "";
		property.serializedObject.ApplyModifiedProperties();
	}
	public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
		return EditorGUI.GetPropertyHeight(property) + 4;
	}
}