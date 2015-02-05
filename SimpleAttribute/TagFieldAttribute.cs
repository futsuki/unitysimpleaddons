using UnityEngine;
using System.Collections;


[System.AttributeUsage (System.AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
public class TagFieldAttribute : PropertyAttribute
{
	public TagFieldAttribute ()
	{
		
	}
}