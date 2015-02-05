using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

[CustomPropertyDrawer( typeof ( CallToLabelAttribute ) )]
public class CallToLabelAttributeDrawer : PropertyDrawer
{
	static System.Reflection.BindingFlags BindFlags = 
		System.Reflection.BindingFlags.Public |
			System.Reflection.BindingFlags.NonPublic |
			System.Reflection.BindingFlags.Instance |
			System.Reflection.BindingFlags.Static;

	string val="";

	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		var attr = attribute as CallToLabelAttribute;
		var ctlpos = position;
		ctlpos.height = GUI.skin.label.CalcHeight(new GUIContent(val), Screen.width);
		position.yMin += ctlpos.height;
		EditorGUI.LabelField(ctlpos, attr.funcname+"() =", val);
		EditorGUI.PropertyField(position, property, true);
	}
	public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
		var attr = attribute as CallToLabelAttribute;
		//var attr = fieldInfo.GetCustomAttributes(true).OfType<CallToLabelAttribute>().First();
		var o = property.serializedObject.targetObject;
		var t = o.GetType();
		var method = t.GetMethod(attr.funcname, BindFlags);
		if (method.IsStatic) o = null;
		val = System.Convert.ToString(method.Invoke(o, new object[]{}));

		return EditorGUI.GetPropertyHeight(property) + GUI.skin.label.CalcHeight(new GUIContent(val), Screen.width);
	}
}


