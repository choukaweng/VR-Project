using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefyGravity : MonoBehaviour {

	Rigidbody rb;
	Vector3 speed;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		speed = rb.velocity;
	}
	
	// Update is called once per frame
	void Update () 
	{	
		GameObject.Find ("Plane").transform.position += new Vector3 (-0.03f, 0f, 0f);
		if (Input.GetKeyDown (KeyCode.Space))
		{
			rb.velocity = transform.up * 3f;
		}
	}
}
