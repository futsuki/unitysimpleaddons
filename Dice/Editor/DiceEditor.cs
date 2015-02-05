using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(Dice))]
public class DiceEditor : PropertyDrawer {
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		var valpos = position;
		position.width = EditorGUIUtility.labelWidth;
		valpos.xMin += EditorGUIUtility.labelWidth;
		//EditorGUI.PrefixLabel(position, label);
		var indent = EditorGUI.indentLevel;
		var xprop = property.FindPropertyRelative("x");
		var yprop = property.FindPropertyRelative("y");
		var avepos = valpos;
		//var lpos = valpos;
		//valpos.xMin += 70;
		//lpos.xMax = valpos.xMin;
		var w3 = (valpos.width - 24)/3;
		valpos.xMax -= w3;
		avepos.xMin = valpos.xMax;
		var vp1 = valpos;
		var vp2 = valpos;
		var vpi = valpos;
		vp1.xMax -= (valpos.width - 12)/2 + 12;
		vp2.xMin += (valpos.width - 12)/2 + 12;
		vpi.xMin = vp1.xMax;
		vpi.xMax = vp2.xMin;
		var box = new GUIStyle(GUI.skin.box);
		box.fontSize -= EditorStyles.miniLabel.fontSize;
		//GUI.Label(lpos, "Wheel to edit", EditorStyles.miniLabel);
		GUI.Label(avepos, "="+Dice.GetAverage(xprop.intValue, yprop.intValue), EditorStyles.miniLabel);
		EditorGUI.PropertyField(vp1, xprop, label);
		EditorGUI.indentLevel = 0;
		EditorGUI.PrefixLabel(vpi, new GUIContent("d"));
		EditorGUI.PropertyField(vp2, yprop, GUIContent.none);
		if (Event.current.type == EventType.ScrollWheel) {
			var down = (Event.current.delta.y > 0);
			var n = down ? -1 : 1;
			if (vp1.Contains(Event.current.mousePosition)) {
				xprop.intValue += n;
				Event.current.Use();
				GUI.FocusControl("");
			}
			if (vp2.Contains(Event.current.mousePosition)) {
				yprop.intValue += n;
				Event.current.Use();
				GUI.FocusControl("");
			}
		}
		EditorGUI.indentLevel = indent;
		property.serializedObject.ApplyModifiedProperties();
	}
	
}

[CustomPropertyDrawer(typeof(DiceShift))]
public class DiceShiftEditor : PropertyDrawer {
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		var valpos = position;
		EditorGUI.PrefixLabel(position, label);
		var labelwidth = EditorGUIUtility.labelWidth;
		position.width = EditorGUIUtility.labelWidth;
		valpos.xMin += EditorGUIUtility.labelWidth;
		var indent = EditorGUI.indentLevel;
		var xprop = property.FindPropertyRelative("x");
		var yprop = property.FindPropertyRelative("y");
		var zprop = property.FindPropertyRelative("z");
		var avepos = valpos;
		var lpos = valpos;
		//valpos.xMin += 70;
		//lpos.xMax = valpos.xMin;
		var w4 = (valpos.width - 36)/4;
		valpos.xMax -= w4;
		avepos.xMin = valpos.xMax;
		var vp1 = valpos;
		var vp2 = valpos;
		var vp3 = valpos;
		var vpi = valpos;
		var vps = valpos;
		vp1.xMax -= (valpos.width - 24)/3 + 12;
		vp1.xMax -= (valpos.width - 24)/3 + 12;
		vp2.xMin += (valpos.width - 24)/3 + 12;
		vp2.xMax -= (valpos.width - 24)/3 + 12;
		vp3.xMin += (valpos.width - 24)/3 + 12;
		vp3.xMin += (valpos.width - 24)/3 + 12;
		vpi.xMin = vp1.xMax;
		vpi.xMax = vp2.xMin;
		vps.xMin = vp2.xMax;
		vps.xMax = vp3.xMax;
		var box = new GUIStyle(GUI.skin.box);
		box.fontSize -= EditorStyles.miniLabel.fontSize;
		//GUI.Label(lpos, "Wheel to edit", EditorStyles.miniLabel);
		GUI.Label(avepos, "="+DiceShift.GetAverage(xprop.intValue, yprop.intValue, zprop.intValue), EditorStyles.miniLabel);
		//vp1.xMin = position.xMin;
		//EditorGUI.PropertyField(vp1, xprop, label);
		EditorGUI.indentLevel = 0;
		EditorGUI.PropertyField(vp1, xprop, GUIContent.none);
		EditorGUIUtility.labelWidth = vpi.width;
		//EditorGUI.PrefixLabel(vpi, new GUIContent("d"));
		vp2.xMin = vpi.xMin;
		EditorGUI.PropertyField(vp2, yprop, new GUIContent("d"));
		//EditorGUI.PrefixLabel(vps, new GUIContent("+"));
		vp3.xMin = vps.xMin;
		EditorGUI.PropertyField(vp3, zprop, new GUIContent("+"));
		if (Event.current.type == EventType.ScrollWheel) {
			var down = (Event.current.delta.y > 0);
			var n = down ? -1 : 1;
			if (vp1.Contains(Event.current.mousePosition)) {
				xprop.intValue += n;
				Event.current.Use();
				GUI.FocusControl("");
			}
			if (vp2.Contains(Event.current.mousePosition)) {
				yprop.intValue += n;
				Event.current.Use();
				GUI.FocusControl("");
			}
			if (vp3.Contains(Event.current.mousePosition)) {
				zprop.intValue += n;
				Event.current.Use();
				GUI.FocusControl("");
			}
		}
		EditorGUI.indentLevel = indent;
		EditorGUIUtility.labelWidth = labelwidth;
		property.serializedObject.ApplyModifiedProperties();
	}
	
}
