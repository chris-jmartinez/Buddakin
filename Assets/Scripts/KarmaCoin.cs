using UnityEngine;
using System.Collections;

public class KarmaCoin : MonoBehaviour {

	public float healValue;
	public int points;
	public int level;

	Player player;

	void Start () {
		player = FindObjectOfType<Player>();
	}
	
	private void OnTriggerEnter2D (Collider2D other){

		if (other.tag == "Player") {
			SoundManager.Instance.CollectCoin ();
			GameManagerL1.Instance.Scored (points);
			player.CollectKarmaCoin (points);
			if (player.health + healValue > 100)
				player.RestartHealth ();
			else
				player.Heal (healValue);
			gameObject.SetActive (false);
		}
	}
}
