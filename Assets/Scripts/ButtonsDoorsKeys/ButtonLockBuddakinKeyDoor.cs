using UnityEngine;
using System.Collections;

public class ButtonLockBuddakinKeyDoor : MonoBehaviour {

    private ButtonLockBuddakin buttonLockBuddakinScript;

	// Use this for initialization
	void Start () {
        buttonLockBuddakinScript = transform.parent.gameObject.GetComponent<ButtonLockBuddakin>(); //gets the script (component) of the parent (a button, see unity) of this verticalDoor.
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "TerrainAndFloor")
        {
            buttonLockBuddakinScript.GoDown = false;
        }
    }

}
