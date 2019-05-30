using UnityEngine;
using System.Collections;

public class ButtonVerticalDoor : MonoBehaviour {

    private bool playerIsNearButton;
    private bool doorReachedBottom;
    public bool DoorReachedBottom //getter and setter of the private variable doorReachedBottom
    {
        get { return doorReachedBottom; }
        set
        {
            doorReachedBottom = value;
        }
    }

    private bool goDown;
    public bool GoDown
    {
        get { return goDown; }
        set
        {
            goDown = value;
        }
    }

    private Transform transfVerticalDoor;
    private bool buttonPressed;
    private Animator animator;

    // Use this for initialization
    void Start () {
        transfVerticalDoor = transform.GetChild(0).gameObject.transform; //For practicity, reusability and to avoid to create new tags, the vertical door we will make go down is a child of the button that opens it (see in unity).
        goDown = false;
        playerIsNearButton = false;
        buttonPressed = false;
        animator = GetComponent<Animator>() as Animator;
    }
	

	// Update is called once per frame
	void Update () {
        if (playerIsNearButton && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.L)) && !buttonPressed)
        {
            //GetComponent<SpriteRenderer>().sprite = buttonPressed;
            goDown = true;
            buttonPressed = true;
            //In the script of the vertical door there will be the command to stop and put "goDown"=false
            animator.SetBool("buttonPressed", true);
        }

        if (goDown)
        {
            transfVerticalDoor.localPosition = transfVerticalDoor.localPosition + new Vector3(0f, -0.4f, 0f); //Because the verticalDoor is a child of the button, I move the localPosition and not the globalPosition, that would move both button and door I think.
            
        }

    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerIsNearButton = true;
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerIsNearButton = false;
        }
    }

}
