using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressZone : MonoBehaviour {

	[SerializeField]
	private float damage = .1f;
	Ninja ninja;

	void Start () {
		ninja = FindObjectOfType<Ninja> ();
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player") {
			ninja.Damage (damage);
		}
	}

}
