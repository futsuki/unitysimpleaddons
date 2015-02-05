using UnityEngine;
using UnityEditor;
using System.Collections;

[InitializeOnLoad]
public class EditorIME : EditorWindow
{
	static EditorIME() {
		Input.imeCompositionMode = IMECompositionMode.On;
	}

	[MenuItem("Window/IME Config")]
	static public void OpenWindow()
	{
		EditorWindow.GetWindow<EditorIME>(false, "IME Config", true);
	}
	
	private string _text="";
	
	void OnGUI()
	{
		GUILayout.BeginHorizontal();
		
		GUILayout.Label( "Mode" );
		
		string[] selString = System.Enum.GetNames( typeof(IMECompositionMode) );
		
		Input.imeCompositionMode = (IMECompositionMode)GUILayout.SelectionGrid( (int)Input.imeCompositionMode, selString, selString.Length );
		
		GUILayout.EndHorizontal();
		
		_text = EditorGUILayout.TextArea( _text,GUILayout.Height(80f) );
	}
}