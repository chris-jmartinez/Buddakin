using UnityEngine;
using System.Collections;

public class DoorOpenReturn : MonoBehaviour {


    private Transform destinationTransform;
    private bool playerIsNearDoor;
    private GameObject player;


    // Use this for initialization
    void Start()
    {
        destinationTransform = transform.parent.gameObject.transform;
        playerIsNearDoor = false;
        player = GameObject.FindGameObjectWithTag("Player") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {

        if (playerIsNearDoor && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.L)))
        {
            player.transform.position = destinationTransform.position;
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
