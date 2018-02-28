using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public float timeBetweenSpawns;
	public GameObject[] prefabs;
	public GameObject target;

	[HideInInspector]
	public Color color;
	public float velocity = 3f;
	private float timeSinceLastSpawn;

	public GameObject[] pointPosition = new GameObject[4];
	private Vector3[] points;
	private List<Vector3> pointList = new List<Vector3>();
	private float time = 0f;

	// Use this for initialization
	void Start () 
	{
		points = new Vector3[pointPosition.Length];
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Space))
		{	
			time = 0;
			SetPoints ();
			pointList.Clear ();
		}
		if(time < 1f)
		{
			Calculate ();
			target.transform.position = pointList [pointList.Count - 1];
		}
	}

	void FixedUpdate()
	{
//		timeSinceLastSpawn += Time.deltaTime;
//		if (timeSinceLastSpawn >= timeBetweenSpawns)
//		{
//			Spawn ();
//			timeSinceLastSpawn -= timeBetweenSpawns;
//		}
	}

	void Spawn()
	{
		GameObject prefab = Instantiate (prefabs [Random.Range (0, prefabs.Length-1)]);
		prefab.transform.localPosition = transform.position;
		prefab.GetComponent<Rigidbody> ().velocity = transform.up * velocity;
		prefab.GetComponent<MeshRenderer> ().material.color = color;
	}

	void Calculate()
	{
		time += Time.deltaTime;
		pointList.Add(Bezier (time, pointPosition.Length - 1));
//		pointList.Add(GetPointAtTime (time));
//		pointList.Add(GetPointAtPower (time));
	}

	void SetPoints()
	{
		for (int i = 0; i < points.Length; i++)
		{
			if (i < points.Length)
			{
				points[i] = pointPosition [i].transform.position;	
			}			
		}
	}

	Vector3 GetPointAtTime(float t)
	{
		float u = 1f - t;
		float uu = u * u;
		float uuu = uu * u;
		float tt = t * t;
		float ttt = tt * t;

		Vector3 p = uuu * points [0];
		p += 3 * uu * t * points [1];
		p += 3 * u * tt * points [2];
		p += ttt * points [3];

		return p;
	}

	Vector3 GetPointAtPower(float t)
	{
		float u = 1f - t;
		float uu = u * u;
		float uuu = uu * u;
		float uuuu = uuu * u;
		float tt = t * t;
		float ttt = tt * t;
		float tttt = ttt * t;

		Vector3 p = uuuu * u * points [0];
		p += 5 * uuuu * t * points [1];
		p += 10 * uuu * tt * points [2];
		p += 10 * ttt * uu * points [3];
		p += 5 * tttt * u * points [4];
		p += tttt * t * points [5];

		return p;
	}


	public void OnDrawGizmos()
	{
		Gizmos.color = Color.black;
		foreach (Vector3 v in pointList)
		{
			Gizmos.DrawSphere (v, 0.1f);
		}
	}

	int Factorial(int no)
	{
		if (no <= 1)
		{
			return 1;
		}
		return no * Factorial (no - 1);
	}

	int BinomialCoeeficient(int n, int k)
	{
		int result = Factorial (n) / (Factorial (k) * Factorial (n - k));
		return result;
	}

	Vector3 Bezier(float t, int power)
	{
		float u = 1f - t;
		Vector3 p = Vector3.zero;
		for (int i = 0; i <= power ; i++)
		{
			p += BinomialCoeeficient (power, i) * Mathf.Pow(t, i) * Mathf.Pow (u, power - i) * points [i];
		}
		return p;
	}
}
