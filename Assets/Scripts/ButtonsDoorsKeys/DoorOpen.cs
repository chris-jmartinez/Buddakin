using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour {

    private Transform destinationTransform;
    private bool playerIsNearDoor;
    private GameObject player;


	// Use this for initialization
	void Start () {
        destinationTransform = transform.GetChild(0).gameObject.transform;  //For practicity and to not create too much tags, I put the destination gameObject (where player arrives opening the door) as a child of the door gameObject.      
        playerIsNearDoor = false;
        player = GameObject.FindGameObjectWithTag("Player") as GameObject;       
    }
	
	// Update is called once per frame
	void Update () {
	    
        if (playerIsNearDoor && ( Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.L)) )
        {
            
            player.transform.position = new Vector3(destinationTransform.position.x, destinationTransform.position.y, destinationTransform.position.z);
            playerIsNearDoor = false;
        }
	}


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerIsNearDoor = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerIsNearDoor = false;
        }
    }
}
