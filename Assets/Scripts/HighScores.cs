using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class HighScores : MonoBehaviour {

	void Start () {
		
	}

	void Update () {
		
	}

	void OnGUI() {
		string[] keys = PlayerPrefs.GetString ("keys").Split (new char[] { '|' });

		Dictionary<string,int> dictionary = new Dictionary<string,int>();

		foreach ( string key in keys ) {
			if ( key != "" && key != null ) {
				dictionary.Add(key, PlayerPrefs.GetInt(key));
			}
		}

		List<KeyValuePair<string,int>> players = dictionary.OrderByDescending (player => player.Value).Take(10).ToList();

		int count = 1;
		foreach ( KeyValuePair<string,int> player in players ) {
			Text username = GameObject.Find ("top" + count.ToString()).GetComponent<Text>();
			Text score = GameObject.Find ("score" + count.ToString()).GetComponent<Text>();
			username.text = count.ToString() + ". " + player.Key;
			score.text = player.Value.ToString();
			count++;
		}
	}

}