using UnityEngine;
using System.Collections;

public class LadderZone : MonoBehaviour {

	private Player player;

	bool m_up;
	bool m_down;
	bool enterLadder;

	BoxCollider2D bc;

	void Start () {
		player = FindObjectOfType<Player> ();
		bc = transform.parent.FindChild ("Ladder Floor").GetComponent<BoxCollider2D> ();
	}

	void Update() {
		
		m_up = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
        m_down = Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S); ;

		if ((m_up || m_down) && enterLadder) {
			bc.isTrigger = true;
		} 
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			enterLadder = true;
			player.OnLadder = true;
		}
	}

	void OnTriggerStay2D (Collider2D other) {
		if (other.tag == "Player") {
			enterLadder = true;
			player.OnLadder = true;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.tag == "Player") {
			player.OnLadder = false;
			enterLadder = false;
			bc.isTrigger = false;
		}
	}
}
