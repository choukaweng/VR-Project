using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureCreator : MonoBehaviour {

	[Range(2,512)]
	public int resolution = 256;
	public FilterMode filterMode;
	private Texture2D texture;

	private void OnEnable()
	{
		if (texture == null)
		{
			texture = new Texture2D (resolution, resolution, TextureFormat.RGB24, true);
			texture.name = "Procedural Texture";
			texture.wrapMode = TextureWrapMode.Clamp;

			texture.anisoLevel = 9;
			GetComponent<MeshRenderer> ().material.mainTexture = texture;
		}
		FillTexture ();
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//TV Snow Effect xDD
//		InvokeRepeating ("RandomizeResolution", 0f, 0.0001f);
//		FillTexture ();
	}

	void RandomizeResolution()
	{
		resolution = Random.Range (200, 290);
	}

	public void FillTexture()
	{
		if (texture.width != resolution)
		{
			texture.Resize (resolution, resolution);
		}

		float stepSize = 1f / resolution;
		Random.seed = 42;
		for (int y = 0; y < resolution; y++)
		{
			for (int x = 0; x < resolution; x++)
			{
////	Repeat same pattern over surface
				texture.SetPixel (x, y, new Color((x + 0.5f) * stepSize % 0.1f, 0f, (y + 0.5f) * stepSize % 0.1f) * 10f);
//				texture.SetPixel (x, y, Color.white * Random.value);
			}
		}
		texture.filterMode = filterMode; //How colors are blended (Point = pixelated, Bilinear = Bleanded, Trilinear = Blended without sharp pixels)
		texture.Apply ();
	}
}
