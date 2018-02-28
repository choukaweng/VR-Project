using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Maze mazePrefab;
	public Player playerPrefab;


	private Maze mazeInstance;
	private Player playerInstance;

	private void Start ()
	{
		BeginGame();
	}
	
	private void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Space)) 
		{
			RestartGame();
		}
	}

	private void BeginGame ()
	{
		Camera.main.clearFlags = CameraClearFlags.Skybox;
		Camera.main.rect = new Rect (0f, 0f, 1f, 1f);
		mazeInstance = Instantiate(mazePrefab) as Maze;
//		StartCoroutine(mazeInstance.Generate());
		mazeInstance.Generate();
		playerInstance = Instantiate<Player> (playerPrefab);
		playerInstance.SetLocation (mazeInstance.GetCell(mazeInstance.RandomPlayerCoordinates));
		Camera.main.rect = new Rect (0f, 0f, 0.5f, 0.5f);
		Camera.main.clearFlags = CameraClearFlags.Depth;
	}

	private void RestartGame () 
	{
		StopAllCoroutines();
		Destroy(mazeInstance.gameObject);
		Destroy(GameObject.FindObjectOfType<Player> ().gameObject);
		BeginGame();
	}
		
}