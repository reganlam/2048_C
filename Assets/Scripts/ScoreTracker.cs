using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour {

	public static ScoreTracker Instance;
	public Text currentScore;
	public Text bestScore;
	private int score;

	public int Score {
		get{
			return score;
		}
		set{
			score = value;
			currentScore.text = score.ToString ();

			if (PlayerPrefs.GetInt ("HighScore") < score) {
				PlayerPrefs.SetInt ("HighScore", score);
				bestScore.text = score.ToString ();
			}
		}
	}

	void Awake () {
		Instance = this;

		if (!PlayerPrefs.HasKey ("HighScore")) {
			PlayerPrefs.SetInt ("HighScore", 0);
		}

		currentScore.text = 0.ToString();
		bestScore.text = PlayerPrefs.GetInt ("HighScore").ToString ();
	}
}
