using UnityEngine;
using System.Collections;

public class LampZone : MonoBehaviour {

	private Ninja ninja; //TODO Change to be generic into a higher class such Character

	void Start () {
		ninja = FindObjectOfType<Ninja> ();
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			ninja.OnLamp = true;
			ninja.LampZone = gameObject.transform.parent.name;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.tag == "Player") {
			ninja.OnLamp = false;
			ninja.LampZone = "";
		}
	}
}
