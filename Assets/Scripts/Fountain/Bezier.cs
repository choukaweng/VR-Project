using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Point(t) = (1-t)^n * P0 + BC(n,1) * (1-t)^(n-1) * t * P1 + ... + BC(n,n-1) * (1-t) * t ^ (n-1) * Pn-1 + t^n * Pn
//BC = Binomial Coeeficient


public class Bezier : MonoBehaviour {
	
	public GameObject[] pointPosition;
	public GameObject target;
	public float delay = 0f;

	private Vector3[] points;
	private List<Vector3> pointList = new List<Vector3>();
	private float time = 1f;

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
			StartCoroutine (Calculate ());
		}

		if (time < 1f)
		{
			target.transform.position = Vector3.Lerp (target.transform.position, pointList [pointList.Count - 1], 0.9f);
		}
		else
		{
			StopAllCoroutines ();
		}
	}

	IEnumerator Calculate()
	{
		while (true)
		{
			time += Time.deltaTime;
			pointList.Add(BezierCurve (time, pointPosition.Length - 1));

			yield return new WaitForSeconds (delay);
		}
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

	Vector3 BezierCurve(float t, int power)
	{
		float u = 1f - t;
		Vector3 p = Vector3.zero;
		for (int i = 0; i <= power ; i++)
		{
			p += BinomialCoeeficient (power, i) * Mathf.Pow(t, i) * Mathf.Pow (u, power - i) * points [i];
		}
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


	//Equation of Bezier curve at power THREE, Cooefficient is 1,3,3,1
	Vector3 BezierPower3(float t)
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

	//Equation of Bezier curve at power FIVE, Cooefficient is 1,5,10,10,5,1
	Vector3 BezierPower5(float t)
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
}
