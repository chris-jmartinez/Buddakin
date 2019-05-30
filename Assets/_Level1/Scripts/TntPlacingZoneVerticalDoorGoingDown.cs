using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TntPlacingZoneVerticalDoorGoingDown : MonoBehaviour {

    private TntPlacingZone tntPlacingZoneScript;

    // Use this for initialization
    void Start()
    {
        tntPlacingZoneScript = transform.parent.gameObject.GetComponent<TntPlacingZone>(); //gets the script (component) of the parent (a button, see unity) of this verticalDoor.
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "TerrainAndFloor")
        {
            tntPlacingZoneScript.VerticalDoorGoDown = false;
            
        }
    }
}
