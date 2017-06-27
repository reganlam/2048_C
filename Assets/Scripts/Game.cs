using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MoveDirection{
	Left, Right, Up, Down
}

public class Game : MonoBehaviour {

	public GameObject gameOverPanel;						// init in Start()
	public Text gameOverText;

	private Tile[,] allTiles = new Tile[4, 4]; 				// init in Start()
	private List<Tile> emptyTiles = new List<Tile> ();		// init in Start()
	private List<Tile[]> tileCols = new List<Tile[]> ();		// init in Start()
	private List<Tile[]> tileRows = new List<Tile[]> ();		// init in Start()

	// Use this for initialization
	void Start () {
		gameOverPanel.SetActive (false);

		Tile[] tileArray = GameObject.FindObjectsOfType<Tile> ();
		foreach (Tile t in tileArray) {
			t.Number = 0;
			allTiles [t.tileRow, t.tileCol] = t;
			emptyTiles.Add (t);
		}

		tileCols.Add (new Tile[]{ allTiles [0, 0], allTiles [1, 0], allTiles [2, 0], allTiles [3, 0] });
		tileCols.Add (new Tile[]{ allTiles [0, 1], allTiles [1, 1], allTiles [2, 1], allTiles [3, 1] });
		tileCols.Add (new Tile[]{ allTiles [0, 2], allTiles [1, 2], allTiles [2, 2], allTiles [3, 2] });
		tileCols.Add (new Tile[]{ allTiles [0, 3], allTiles [1, 3], allTiles [2, 3], allTiles [3, 3] });

		tileRows.Add (new Tile[]{ allTiles [0, 0], allTiles [0, 1], allTiles [0, 2], allTiles [0, 3] });
		tileRows.Add (new Tile[]{ allTiles [1, 0], allTiles [1, 1], allTiles [1, 2], allTiles [1, 3] });
		tileRows.Add (new Tile[]{ allTiles [2, 0], allTiles [2, 1], allTiles [2, 2], allTiles [2, 3] });
		tileRows.Add (new Tile[]{ allTiles [3, 0], allTiles [3, 1], allTiles [3, 2], allTiles [3, 3] });

		Generate ();
		Generate ();
	}
	
	// Update is called once per frame
	void Update () {
		// Restart 
		if (Input.GetKeyDown (KeyCode.R)) {
			RestartGame ();
		} 
		//

		// Quit
		else if (Input.GetKeyDown ("escape")) {
			Debug.Log ("quit");
			Application.Quit ();
		}
		//

		// Player Movement
		else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			Move (MoveDirection.Right);
		} 
		else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			Move (MoveDirection.Left);
		} 
		else if (Input.GetKeyDown (KeyCode.UpArrow)) {
			Move (MoveDirection.Up);
		} 
		else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			Move (MoveDirection.Down);
		}
		//

	}

	public void RestartGame(){
		Application.LoadLevel (Application.loadedLevel);
	}
		
	// Spawns a tile on an empty tile
	void Generate() {
		
		// Base case:
		if (emptyTiles.Count == 0) {						
			return;
		}
		// 

		int randomTileIndex = Random.Range (0, emptyTiles.Count);
		int randomFourIndex = Random.Range (0, 10);

		if (randomFourIndex == 0) {
			emptyTiles [randomTileIndex].Number = 4;		// 1/10 Probability
		} else {
			emptyTiles [randomTileIndex].Number = 2;
		}
		emptyTiles.RemoveAt (randomTileIndex);
	}

	bool ShuffleUp(Tile[] tileSegment){

		for (int i = 0; i < tileSegment.Length - 1; i++) {
			if (tileSegment [i].Number == 0 && tileSegment [i + 1].Number != 0) {
				tileSegment [i].Number = tileSegment [i + 1].Number;
				tileSegment [i + 1].Number = 0;
				return true;
			}

			else if (tileSegment[i].Number != 0 && tileSegment [i].Number == tileSegment [i + 1].Number && tileSegment[i].canMerge == true && tileSegment[i + 1].canMerge == true) {
				tileSegment [i].Number = tileSegment [i].Number * 2;
				tileSegment [i].canMerge = false;
				tileSegment [i + 1].Number = 0;
				ScoreTracker.Instance.Score += tileSegment [i].Number;
				return true;
			}
		}
		return false;
	}

	bool ShuffleDown(Tile[] tileSegment){
		for (int i = tileSegment.Length - 1; i > 0; i--) {
			if (tileSegment [i].Number == 0 && tileSegment [i - 1].Number != 0) {
				tileSegment [i].Number = tileSegment [i - 1].Number;
				tileSegment [i - 1].Number = 0;
				return true;
			} else if (tileSegment [i].Number != 0 && tileSegment [i].Number == tileSegment [i - 1].Number && tileSegment[i].canMerge == true && tileSegment[i - 1].canMerge == true) {
				tileSegment [i].Number = tileSegment [i].Number * 2;
				tileSegment [i].canMerge = false;
				tileSegment [i - 1].Number = 0;
				ScoreTracker.Instance.Score += tileSegment [i].Number;
				return true;
			}
		}
		return false;
	}

	// updates emptyTiles so new tiles generated will generate at empty tiles
	void updateEmptyTiles(){
		emptyTiles.Clear ();

		foreach (Tile t in allTiles) {
			if (t.Number == 0) {
				emptyTiles.Add (t);
			}
		}
	}

	void resetMergeFlags(){
		foreach (Tile t in allTiles) {
			t.canMerge = true;
		}
	}

	bool canMove(){
		if (emptyTiles.Count > 0) {
			return true;
		}
		else{
			for (int i = 0; i < tileCols.Count; i++) {
				for (int j = 0; j < tileCols [i].Length - 1; j++) {
					if (tileCols [i] [j].Number == tileCols [i] [j + 1].Number) {
						return true;
					}
				}
				for (int k = 0; k < tileRows [i].Length - 1; k++) {
					if (tileRows [i] [k].Number == tileRows [i] [k + 1].Number) {
						return true;
					}
				}
			}
		}

		return false;
	}

	void GameOver(){
		gameOverText.text = ScoreTracker.Instance.Score.ToString();
		gameOverPanel.SetActive (true);
	}

	void Move(MoveDirection direction){
		bool moveMade = false;

		for (int i = 0; i < tileRows.Count; i++) {

			switch (direction) {

			case MoveDirection.Up:
				while (ShuffleUp (tileCols [i])) {
					moveMade = true;
				}
				break;
			case MoveDirection.Down:
				while (ShuffleDown (tileCols [i])) {
					moveMade = true;
				}
				break;
			case MoveDirection.Right:
				while (ShuffleDown (tileRows [i])) {
					moveMade = true;
				}
				break;
			case MoveDirection.Left:
				while (ShuffleUp (tileRows [i])) {
					moveMade = true;
				}
				break;
			}
		}
		if (moveMade) {
			updateEmptyTiles ();
			resetMergeFlags ();
			Generate ();

			if (!canMove ()) {
				GameOver ();
			}
		}
	}
}
