using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoobDoor : MonoBehaviour {

	private bool used;
	private bool onZone;

	void Start () {
		used = false;
		onZone = false;
	}

	void Update(){
		if ((Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W)) && onZone && !used) {
			used = true;
			if (GameManagerL1.Instance.CurrentScorePlayer >= 50) {
				print ("yesss " + gameObject.name);
				GameManagerL1.Instance.Scored (-50);
			}
		}
	}
	
	private void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "Player") {
			onZone = true;
		}	
	}

	private void OnTriggerExit2D (Collider2D other){
		if (other.tag == "Player") {
			onZone = false;
		}	
	}
}
