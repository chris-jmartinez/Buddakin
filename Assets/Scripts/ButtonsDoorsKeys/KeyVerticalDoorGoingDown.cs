using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyVerticalDoorGoingDown : MonoBehaviour {

    private KeyVerticalDoor keyVerticalDoorScript;

    // Use this for initialization
    void Start()
    {
        keyVerticalDoorScript = transform.parent.gameObject.GetComponent<KeyVerticalDoor>(); //gets the script (component) of the parent (a button, see unity) of this verticalDoor.
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "TerrainAndFloor")
        {
            keyVerticalDoorScript.GoDown = false;
        }
    }
}
