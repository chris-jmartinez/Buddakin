using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour {

	public Sprite green;
	private bool hacked;
	private float health;

	Ninja ninja;

	void Start () {
		hacked = false;
		health = 100f;
		ninja = FindObjectOfType<Ninja> ();
		transform.GetChild (0).gameObject.SetActive (false);
	}
	
	void Update () {
		if (!hacked) {
			if (health <= 0) {
				hacked = true;
				GetComponent<SpriteRenderer> ().sprite = green;
				transform.GetChild (0).GetComponent<BoxCollider2D> ().isTrigger = true;
				transform.GetChild (0).GetComponent<Unlocker> ().enabled = true;
				transform.GetChild (0).gameObject.SetActive (true);
				this.enabled = false;
			}
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player" && ninja.IsAttacking) {
			health -=1f;
		}
	}
}
