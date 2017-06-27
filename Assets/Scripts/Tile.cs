using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour {

	private int number;
	private Text tileNumber;
	private Image tileImage;

	public int tileRow;
	public int tileCol;
	public bool canMerge = true;

	void Awake () {
		tileNumber = GetComponentInChildren<Text> ();
		tileImage = GetComponentInChildren<Image> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Method to change the appearance of the tile whenever its number changes
	public int Number {
		get{
			return number;
		}
		set{
			number = value;
			SetTileStyle (number);
		}
	}

	// Tile number and colors changes based on TileStyle's configurations
	void ApplyTileConfigStyle (int index) {
		//tileNumber.text = TileConfig.Instance.TileStyles [index].number.ToString ();
		tileNumber.text = number.ToString();
		tileNumber.color = TileConfig.Instance.TileStyles [index].numberColor;
		tileImage.color = TileConfig.Instance.TileStyles [index].tileColor;
	}

	// Tile number switch cases mapped to TileConfig's TileStyle[] TileStyles
	void SetTileStyle (int number) {
		switch (number) {
		case 0:
			ApplyTileConfigStyle (0);
			break;
		case 2:
			ApplyTileConfigStyle (1);
			break;
		case 4:
			ApplyTileConfigStyle (2);
			break;
		case 8:
			ApplyTileConfigStyle (3);
			break;
		case 16:
			ApplyTileConfigStyle (4);
			break;
		case 32:
			ApplyTileConfigStyle (5);
			break;
		case 64:
			ApplyTileConfigStyle (6);
			break;
		case 128:
			ApplyTileConfigStyle (7);
			break;
		case 256:
			ApplyTileConfigStyle (8);
			break;
		case 512:
			ApplyTileConfigStyle (9);
			break;
		case 1024:
			ApplyTileConfigStyle (10);
			break;
		case 2048:
			ApplyTileConfigStyle (11);
			break;
		case 4096:
			ApplyTileConfigStyle (12);
			break;
		default:
			ApplyTileConfigStyle (13);
			break;
		}
	}

}
