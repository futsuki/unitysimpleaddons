using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using Object = UnityEngine.Object;

static public class UnityExt {
	public static IEnumerable<Transform> RecursiveChildren(Transform t) {
		var st = new Stack<Transform>();
		st.Push(t);
		while (st.Count > 0) {
			var c = st.Pop();
			yield return c;
			foreach (var child in c.Cast<Transform>()) {
				st.Push(child);
			}
		}
	}

	static List<char> UnityCapitalize_sb = new List<char>();
	public static string UnityCapitalize(this string s) {
		UnityCapitalize_sb.Clear();
		bool oldcIsUpper=false;
		bool first=true;
		foreach (var c in s) {
			if (char.IsUpper(c)) {
				if (!oldcIsUpper)
					UnityCapitalize_sb.Add(' ');
				UnityCapitalize_sb.Add(c);
				oldcIsUpper = true;
			}
			else if (first) {
				UnityCapitalize_sb.Add(char.ToUpper(c));
			}
			else {
				UnityCapitalize_sb.Add(c);
				oldcIsUpper = false;	
			}
			first = false;
		}
		return new string(UnityCapitalize_sb.ToArray());
	}

	public static IEnumerable<T> ConcatOne<T>(this IEnumerable<T> ie, T v) {
		foreach (var t in ie)
			yield return t;
		yield return v;
	}
	public static IEnumerable<T> OneConcat<T>(this T v, IEnumerable<T> ie) {
		yield return v;
		foreach (var t in ie)
			yield return t;
	}


	public static T FindComponent<T>(string path) 
		where T : Component
	{
		var o = GameObject.Find(path);
		if (o == null) return null;
		return o.GetComponent<T>();
	}
	public static T FindComponent<T>(this Transform t, string path) 
		where T : Component
	{
		if (t == null)
			return FindComponent<T>(path);
		else {
			var o = t.FindChild(path);
			if (o == null) return null;
			return o.GetComponent<T>();
		}
	}
	public static T FindComponent<T>(this Component c, string path) 
		where T : Component
	{
		if (c == null)
			return FindComponent<T>(path);
		else
			return FindComponent<T>(c.transform, path);
	}
	public static T FindComponent<T>(this GameObject go, string path) 
		where T : Component
	{
		if (go == null)
			return FindComponent<T>(path);
		else
			return FindComponent<T>(go.transform, path);
	}

	public static Component FindComponent(string path, Type type) 
	{
		var o = GameObject.Find(path);
		if (o == null) return null;
		return o.GetComponent(type);
	}
	public static Component FindComponent(this Transform t, string path, Type type) 
	{
		if (t == null)
			return FindComponent(path, type);
		else {
			var o = t.FindChild(path);
			if (o == null) return null;
			return o.GetComponent(type);
		}
	}
	public static Component FindComponent(this Component c, string path, Type type) 
	{
		if (c == null)
			return FindComponent(path, type);
		else
			return FindComponent(c.transform, path, type);
	}
	public static Component FindComponent(this GameObject go, string path, Type type) 
	{
		if (go == null)
			return FindComponent(path, type);
		else
			return FindComponent(go.transform, path, type);
	}

	public static Component FindComponent(string path, string type) 
	{
		var o = GameObject.Find(path);
		if (o == null) return null;
		return o.GetComponent(type);
	}
	public static Component FindComponent(this Transform t, string path, string type) 
	{
		if (t == null)
			return FindComponent(path, type);
		else {
			var o = t.FindChild(path);
			if (o == null) return null;
			return o.GetComponent(type);
		}
	}
	public static Component FindComponent(this Component c, string path, string type) 
	{
		if (c == null)
			return FindComponent(path, type);
		else
			return FindComponent(c.transform, path, type);
	}
	public static Component FindComponent(this GameObject go, string path, string type) 
	{
		if (go == null)
			return FindComponent(path, type);
		else
			return FindComponent(go.transform, path, type);
	}
}
