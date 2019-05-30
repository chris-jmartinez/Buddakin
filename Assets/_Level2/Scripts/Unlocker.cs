using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlocker : MonoBehaviour {

	private bool ready;

	void Start(){
		ready = true;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player" && ready) {
			AddElementToInventory ();
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.tag == "Player" && ready) {
			this.enabled = false;
		}
	}

	private void AddElementToInventory() {
		InventoryManager.Instance.addItem (gameObject);
		if (transform.parent != null){
			if (transform.parent.gameObject.GetComponent<ManInBlackL2> () != null) {
				transform.parent.gameObject.GetComponent<ManInBlackL2> ().CleanMiB ();
			}
		}
		gameObject.SetActive (false);
	}
}
