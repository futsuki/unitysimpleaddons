using UnityEngine;
using System.Collections;


[System.AttributeUsage (System.AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
public class NameAttribute : PropertyAttribute
{
	public string getter;
	
	public NameAttribute (string getter)
	{
		this.getter = getter;
	}
}


