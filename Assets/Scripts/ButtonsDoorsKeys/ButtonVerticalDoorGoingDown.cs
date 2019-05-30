using UnityEngine;
using System.Collections;

public class ButtonVerticalDoorGoingDown : MonoBehaviour {

    private ButtonVerticalDoor buttonVerticalDoorScript;

	// Use this for initialization
	void Start () {
        buttonVerticalDoorScript = transform.parent.gameObject.GetComponent<ButtonVerticalDoor>(); //gets the script (component) of the parent (a button, see unity) of this verticalDoor.
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "TerrainAndFloor")
        {
            buttonVerticalDoorScript.GoDown = false;
        }
    }

}
