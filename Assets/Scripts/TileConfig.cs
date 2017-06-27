using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Stores the tileColor, numberColor for corresponding number 2, 4, 8, ..., 2048 in array of class TileStyle

[System.Serializable]
public class TileStyle{
	public int number;
	public Color32 tileColor;
	public Color32 numberColor;
}

public class TileConfig : MonoBehaviour {

	public TileStyle[] TileStyles;

	public static TileConfig Instance;
	void Awake(){
		Instance = this;
	}
}
