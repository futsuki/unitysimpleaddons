using UnityEngine;
using System.Collections;


[System.AttributeUsage (System.AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
public class CallToLabelAttribute : PropertyAttribute
{
	public string funcname;
	
	public CallToLabelAttribute (string funcname)
	{
		this.funcname = funcname;
	}
}


