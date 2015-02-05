using UnityEngine;
using System.Collections;


[System.AttributeUsage (System.AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
public class DisplayConditionAttribute : PropertyAttribute
{
	public string getter;
	
	public DisplayConditionAttribute (string getter)
	{
		this.getter = getter;
	}
}


