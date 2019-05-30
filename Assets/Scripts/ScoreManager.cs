using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	[Header("Save Score Screen")]
	public GameObject scoreScreen;
	public Text scoreText;

	void Start () {
		
	}
	
	void Update () {
		
	}

	public void PromptScore() {
		scoreScreen.SetActive (true);
		scoreText.text = "You got " + GameManagerL1.Instance.CurrentScorePlayer.ToString() + " points!";
	}

	public void SaveScore() {
		string username = GameObject.Find("UsernameText").GetComponent<Text>().text;
		int score = GameManagerL1.Instance.CurrentScorePlayer;

		if ( PlayerPrefs.HasKey (username) ) {
			score = PlayerPrefs.GetInt (username) > score ? PlayerPrefs.GetInt (username) : score;
		} else {
			PlayerPrefs.SetString ("keys", username + "|" + PlayerPrefs.GetString("keys"));
		}

		PlayerPrefs.SetInt (username, score);
		PlayerPrefs.Save ();
		Camera.main.GetComponent<MenuControls> ().restart ();
	}

}