using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private Vector3 movementVector;
	private Rigidbody rb;
	private float rate;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		InvokeRepeating ("Move", 0f, rate);

		if (Input.GetKey (KeyCode.LeftShift))
		{
			movementVector = new Vector3 (Input.GetAxis ("Horizontal") * 3f, 0f, Input.GetAxis ("Vertical") * 3f);
		}
		else
		{
			movementVector = new Vector3 (Input.GetAxis ("Horizontal"), 0f, Input.GetAxis ("Vertical"));
		}

		transform.localRotation *= Quaternion.Euler(0f, Input.GetAxis("Mouse X"),0f);
	}

	void Move()
	{
		rb.velocity = transform.TransformVector(movementVector);
	}

	public void SetLocation(MazeCell cell)
	{
		transform.position = new Vector3 (cell.coordinates.x, 0.5f, cell.coordinates.z);
	}
		
}
