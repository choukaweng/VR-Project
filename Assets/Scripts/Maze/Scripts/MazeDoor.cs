using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeDoor : MazePassage {

	public Transform hinge;
	private Quaternion originalRotation;

	private MazeDoor OtherSideOfDoor
	{
		get
		{ 
			return otherCell.GetEdge (direction.GetOpposite ()) as MazeDoor;
		}
	}

	public override void Initialize(MazeCell primary, MazeCell other, MazeDirection direction)
	{

		base.Initialize (primary, other, direction);
//		transform.localPosition += Vector3.forward * 0.5f;
		if (OtherSideOfDoor != null)
		{
			hinge.localRotation = Quaternion.Euler (0f, 180f, 0f);
			Vector3 p = hinge.localPosition;
			p.x = -p.x;
			hinge.localPosition = p;
			originalRotation = hinge.localRotation;
		}

		MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer> ();
		foreach (MeshRenderer r in renderers)
		{
			if (r.transform.parent != hinge)
			{
				r.material = cell.room.settings.wallMaterial;
			}
		}
	}

	public void OnTriggerEnter(Collider col)
	{
		if (col.transform.parent.GetComponent<Player> () != null)
		{
			OpenDoor ();
		}
	}

	public void OnTriggerExit(Collider col)
	{
		if (col.transform.parent.GetComponent<Player> () != null)
		{
			CloseDoor ();
		}
	}

	public void OpenDoor()
	{
		hinge.localRotation *= Quaternion.Euler (0f, -90f, 0f);
	}

	public void CloseDoor()
	{
//		hinge.localRotation = originalRotation;
		hinge.localRotation *= Quaternion.Inverse(Quaternion.Euler (0f, -90f, 0f));

	}

}
