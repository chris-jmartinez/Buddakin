using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour {

	public GameObject lockedObject;
	public GameObject[] unlocks;
	private bool locked;
	public Sprite spriteGreen;

	bool onZone;
	bool m_useKeyPressed;
	CircleCollider2D cc;

	void Start () {
		locked = true;	
	}
	
	void Update () {
		m_useKeyPressed = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);

		if (locked && m_useKeyPressed && onZone && checkItemOnInventory()) {
			locked = false;

			if(spriteGreen != null)
				GetComponent<SpriteRenderer> ().sprite = spriteGreen;

			//Enable locked object associated to this lock
			if (lockedObject.GetComponent<MovingPlatform> () != null) {
				lockedObject.GetComponent<MovingPlatform> ().enabled = true;
			} else if (lockedObject.name == "Door Last Floor") {
				lockedObject.GetComponent<BoxCollider2D> ().isTrigger = true;
			}
			this.enabled = false;
		}
	}

	bool checkItemOnInventory(){
		bool hasAllUnlocks = true;

		foreach (GameObject go in unlocks) {
			hasAllUnlocks = hasAllUnlocks && InventoryManager.Instance.findItem (go.name);
		}

		return hasAllUnlocks;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			onZone = true;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.tag == "Player") {
			onZone = false;
		}
	}
}
