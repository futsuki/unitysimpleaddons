using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ILinkedListNode<T>
	where T : class, ILinkedListNode<T>
{
	T prev { get; set; }
	T next { get; set; }
}

static public class ILinkedListNodeExt {
	static public T Last<T>(this ILinkedListNode<T> that) 
		where T : class, ILinkedListNode<T>
	{
		var p = (T)that;
		while (p.next != null) {
			p = p.next;
		}
		return p;
	}
	static public T First<T>(this ILinkedListNode<T> that) 
		where T : class, ILinkedListNode<T>
	{
		var p = (T)that;
		while (p.prev != null) {
			p = p.prev;
		}
		return p;
	}
	static public IEnumerable<T> Enum<T>(this ILinkedListNode<T> _that) 
		where T : class, ILinkedListNode<T>
	{
		var that = (T)_that;
		var p = that;
		while (p != null) {
			yield return p;
			p = p.next;
		}
	}
	static public IEnumerable<T> EnumReverse<T>(this ILinkedListNode<T> _that) 
		where T : class, ILinkedListNode<T>
	{
		var that = (T)_that;
		var p = that;
		while (p != null) {
			yield return p;
			p = p.prev;
		}
	}
	
	static public void Remove<T>(this ILinkedListNode<T> _that) 
		where T : class, ILinkedListNode<T>
	{
		var that = (T)_that;
		if (that.next != null) {
			var n = that.next;
			that.next.prev = that.prev;
		}
		if (that.prev != null) {
			that.prev.next = that.next;
		}
		that.next = null;
		that.prev = null;
	}
	static public void Add<T>(this ILinkedListNode<T> _that, T newValue) 
		where T : class, ILinkedListNode<T>
	{
		var that = (T)_that;
		newValue.prev = that;
		newValue.next = that.next;
		if (that.next != null) {
			that.next.prev = newValue;
		}
		that.next = newValue;
	}
	static public void Insert<T>(this ILinkedListNode<T> _that, T newValue) 
		where T : class, ILinkedListNode<T>
	{
		var that = (T)_that;
		newValue.prev = that.prev;
		newValue.next = that;
		if (that.prev != null) {
			that.prev.next = newValue;
		}
		that.prev = newValue;
	}
}
