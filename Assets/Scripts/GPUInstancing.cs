using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GPUInstancing : MonoBehaviour {

	public GameObject prefab;
	public int quantity = 5000;
	public float radius = 50f;

	// Use this for initialization
	void Start () 
	{
		MaterialPropertyBlock properties = new MaterialPropertyBlock ();
		for (int i = 0; i < quantity; i++)
		{
			GameObject t = Instantiate (prefab);
			t.transform.localPosition = Random.insideUnitSphere * radius;
			t.transform.parent = transform;
		
			properties.SetColor ("_Color", new Color (Random.value, Random.value, Random.value));
			t.GetComponent<MeshRenderer> ().SetPropertyBlock (properties);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
				
}
