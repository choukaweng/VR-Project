using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour {

	public int noOfSpawners;
	public float radius, tiltAngle;
	public Spawner spawnerPrefab;
	private Color[] colorList;

	// Use this for initialization
	void Start () 
	{
		colorList = new Color[]{Color.red, Color.blue, Color.green};
		for (int i = 0; i < noOfSpawners; i++)
		{
			CreateSpawner (i);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void CreateSpawner(int index)
	{
		Transform rotater = new GameObject ("Rotater").transform;
		rotater.SetParent (transform, false);
		rotater.localRotation = Quaternion.Euler (0f, index * 360f / noOfSpawners, 0f);
		Spawner s = Instantiate (spawnerPrefab);
		s.transform.SetParent (rotater, false);	
		s.transform.localPosition = new Vector3 (0f, 0f, -radius * 2f);
		s.transform.localRotation = Quaternion.Euler (tiltAngle, 0f, 0f);
		s.color = colorList [Random.Range (0, colorList.Length)];
	}
}
