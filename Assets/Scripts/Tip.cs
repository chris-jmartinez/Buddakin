using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tip : MonoBehaviour {

	public GameObject buddakinSays;
	public string messageName;

	void Start() {
		TipManager.anim = buddakinSays.GetComponentInChildren <Animator> ();
	}

	void OnTriggerExit2D(Collider2D other) {
		if ( other.tag == "Player" ) {
			TipManager.sendMessage (messageName);
		}
	}

}
