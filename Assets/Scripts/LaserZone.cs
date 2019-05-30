using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserZone : MonoBehaviour {

	public float damage = 1f;
	public float distance;
	public bool vertical;
	public int time;

	Player player;

	void Start () {
		player = FindObjectOfType<Player> ();

		iTween.MoveAdd(gameObject, iTween.Hash(vertical ? "y" : "x",  distance, "time", time, "looptype", iTween.LoopType.pingPong, "easetype", iTween.EaseType.linear, "delay", Random.Range(0, .4f)));
	}

	void OnTriggerStay2D (Collider2D other) {
		if (other.tag == "Player") {
			player.Damage (damage);
		}
	}
}
