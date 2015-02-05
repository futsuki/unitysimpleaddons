using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MeshedSprite : MonoBehaviour {
	[SerializeField] Sprite _sprite;
	public float pixelToUnits=100;
	public Material material;
	public Sprite sprite {
		get {
			return _sprite;
		}
		set {
			_sprite = value;
			OnValidate();
		}
	}
	MeshFilter mf;
	MeshRenderer mr;

	void OnValidate() {
		if (!mf)
			mf = GetComponent<MeshFilter>();
		if (!mr)
			mr = GetComponent<MeshRenderer>();
		if (!mr) return;
		if (!mf) return;
		if (!material) return;
		if (!sprite) return;

		var newMat = new Material(material);
		newMat.mainTexture = sprite.texture;
		mr.sharedMaterial = newMat;
		if (Application.isPlaying)
			mr.material = newMat;

		Mesh mesh;
		if (mf && mf.sharedMesh && mf.sharedMesh.name == "(MeshedSprite)" && mf.sharedMesh.vertexCount == 4) {
			mesh = mf.sharedMesh;
		}
		else {
			mesh = new Mesh();
			mesh.name = "(MeshedSprite)";
		}
		var vs = new Vector3[4];
		var w2 = sprite.rect.width/pixelToUnits * 0.5f;
		var h2 = sprite.rect.height/pixelToUnits * 0.5f;
		vs[0] = new Vector3(-w2, -h2, 0);
		vs[1] = new Vector3( w2, -h2, 0);
		vs[2] = new Vector3( w2,  h2, 0);
		vs[3] = new Vector3(-w2,  h2, 0);
		vs = vs.Concat(vs).ToArray();
		var uvs = new Vector2[4];
		uvs[0] = new Vector2(sprite.rect.xMin/sprite.texture.width, sprite.rect.yMin/sprite.texture.height);
		uvs[1] = new Vector2(sprite.rect.xMax/sprite.texture.width, sprite.rect.yMin/sprite.texture.height);
		uvs[2] = new Vector2(sprite.rect.xMax/sprite.texture.width, sprite.rect.yMax/sprite.texture.height);
		uvs[3] = new Vector2(sprite.rect.xMin/sprite.texture.width, sprite.rect.yMax/sprite.texture.height);
		var colors = new Color[4]{ Color.white, Color.white, Color.white, Color.white };
		uvs = uvs.Concat(uvs).ToArray();
		colors = colors.Concat(colors).ToArray();
		var tris = new int[12]{ 0, 1, 2, 0, 2, 3, 4+2, 4+1, 4+0, 4+3, 4+2, 4+0 };
		mesh.vertices = vs;
		mesh.colors = colors;
		mesh.uv = uvs;
		mesh.SetTriangles(tris, 0);
		mesh.RecalculateNormals();
		mf.sharedMesh = mesh;
		if (Application.isPlaying)
			mf.mesh = mesh;
	}

	// Use this for initialization
	void Awake () {
		OnValidate ();
	}
}
