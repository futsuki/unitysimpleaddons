using UnityEngine;
using System.Collections;

public class BasicImages {
	public enum Image {
		Up, Right, Down, Left
	}
	public enum ColorType {
		White, Black
	}

	static public ColorType defaultColor {
		get {
#if UNITY_EDITOR
			if (UnityEditor.EditorGUIUtility.isProSkin) {
				return ColorType.White;
			}
			else {
				return ColorType.Black;
			}
#else
			return ColorType.Black;
#endif
		}
	}

	static Texture2D _ArrowUpBlack;
	static public Texture2D ArrowUpBlack { get { return LoadAuto(ref _ArrowUpBlack, "Icon/ArrowUpBlack"); } }
	static Texture2D _ArrowLeftBlack;
	static public Texture2D ArrowLeftBlack { get { return LoadAuto(ref _ArrowLeftBlack, "Icon/ArrowLeftBlack"); } }
	static Texture2D _ArrowDownBlack;
	static public Texture2D ArrowDownBlack { get { return LoadAuto(ref _ArrowDownBlack, "Icon/ArrowDownBlack"); } }
	static Texture2D _ArrowRightBlack;
	static public Texture2D ArrowRightBlack { get { return LoadAuto(ref _ArrowRightBlack, "Icon/ArrowRightBlack"); } }

	static Texture2D _ArrowUpWhite;
	static public Texture2D ArrowUpWhite { get { return LoadAuto(ref _ArrowUpWhite, "Icon/ArrowUpWhite"); } }
	static Texture2D _ArrowLeftWhite;
	static public Texture2D ArrowLeftWhite { get { return LoadAuto(ref _ArrowLeftWhite, "Icon/ArrowLeftWhite"); } }
	static Texture2D _ArrowDownWhite;
	static public Texture2D ArrowDownWhite { get { return LoadAuto(ref _ArrowDownWhite, "Icon/ArrowDownWhite"); } }
	static Texture2D _ArrowRightWhite;
	static public Texture2D ArrowRightWhite { get { return LoadAuto(ref _ArrowRightWhite, "Icon/ArrowRightWhite"); } }

	static public Texture2D Load(Image img) {
		return Load(img, defaultColor);
	}
	static public Texture2D Load(Image img, ColorType type) {
		if (type == ColorType.Black) {
			switch (img) {
			default:
			case Image.Down:  return ArrowDownBlack;
			case Image.Left:  return ArrowLeftBlack;
			case Image.Right: return ArrowRightBlack;
			case Image.Up:    return ArrowUpBlack;
			}
		}
		else if (type == ColorType.White) {
			switch (img) {
			default:
			case Image.Down:  return ArrowDownWhite;
			case Image.Left:  return ArrowLeftWhite;
			case Image.Right: return ArrowRightWhite;
			case Image.Up:    return ArrowUpWhite;
			}
		}
		return null;
	}

	static T LoadAuto<T>(ref T vari, string path)
		where T : Object
	{
		if (!vari)
			vari = Resources.Load<T>(path);
		return vari;
	}

}
