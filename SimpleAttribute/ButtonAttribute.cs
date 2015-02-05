using UnityEngine;
using System.Collections;

[System.AttributeUsage (System.AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
public class ButtonAttribute : PropertyAttribute
{
	public string label;
	public string methodName;
	public object[] param;
	
	public ButtonAttribute (string label, string methodname, params object[] param)
	{
		this.label = label;
		this.methodName = methodname;
		this.param = param;
	}
	public ButtonAttribute (string label, string methodname) : this(label, methodname, null)
	{

	}
	public ButtonAttribute (string methodname) : this(methodname, methodname, null)
	{

	}
}