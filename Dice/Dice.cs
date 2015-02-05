using UnityEngine;
using System.Collections;

interface IDice {
	int RollThis();
	float GetAverageThis();
}

[System.Serializable]
public class Dice {
	static public int Roll(int x, int y) {
		if (x <= 0 || y == 0) return 0;
		int r=0;
		for (int i=0; i<x; i++) {
			r += Random.Range(0, y) + 1;
		}
		return r;
	}
	static public float GetAverage(int x, int y) {
		return ((y+1)*0.5f) * x;
	}
	
	public int x=0, y=6;
	
	public int RollThis() {
		return Roll (x, y);
	}
	public float GetAverageThis() {
		return GetAverage (x, y);
	}

	public override string ToString ()
	{
		var xs = (x == 1) ? "" : x.ToString();
		return string.Format ("{0}d{1}", xs, y);
	}
}


[System.Serializable]
public class DiceShift {
	static public int Roll(int x, int y, int z) {
		if (x <= 0 || y == 0) return z;
		int r=0;
		for (int i=0; i<x; i++) {
			r += Random.Range(0, y) + 1;
		}
		return r + z;
	}
	static public float GetAverage(int x, int y, int z) {
		return ((y+1)*0.5f) * x + z;
	}
	
	public int x=0, y=6, z=0;
	
	public int RollThis() {
		return Roll (x, y, z);
	}
	public float GetAverageThis() {
		return GetAverage (x, y, z);
	}

	public override string ToString ()
	{
		var xs = (x == 1) ? "" : x.ToString();
		var zs = (z == 0) ? "" : (z > 0 ) ? "+"+z.ToString() : "-"+z.ToString();
		return string.Format ("{0}d{1}{2}", xs, y, zs);
	}
}
