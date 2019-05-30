using UnityEngine;
using System.Collections;

public class ButtonLockBuddakin : MonoBehaviour {

    private bool playerIsNearButton;
    private bool keyDoorReachedBottom;
    public bool DoorReachedBottom //getter and setter of the private variable doorReachedBottom
    {
        get { return keyDoorReachedBottom; }
        set
        {
            keyDoorReachedBottom = value;
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

    private Transform transfKeyDoor;
    private Animator animatorLock1Buddakin;
    private bool buttonPressed;
    private Animator animatorButtonToPress;

    // Use this for initialization
    void Start () {
        transfKeyDoor = transform.GetChild(0).gameObject.transform; //For practicity, reusability and to avoid to create new tags, the vertical door we will make go down is a child of the button that opens it (see in unity).
        animatorLock1Buddakin = transform.GetChild(1).gameObject.GetComponent<Animator>();
        goDown = false;
        playerIsNearButton = false;
        //buttonPressed = Resources.Load<Sprite>("buttonActive"); //THEY SAY IT'S NOT THE PERFECT WAY TO LOAD A SPRITE, IN FACT IT DOESN'T WORK (MAYBE NEED TO CREATE A FOLDER "RESOURCES" BUT I THINK IT'S BAD PRACTICE.
        animatorButtonToPress = GetComponent<Animator>() as Animator;
    }
	

	// Update is called once per frame
	void Update () {
        if (playerIsNearButton && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.L)) && !buttonPressed)
        {
            buttonPressed = true;
            goDown = true;
            //In the script of the vertical door there will be the command to stop and put "goDown"=false
            animatorButtonToPress.SetBool("buttonPressed", true);
            animatorLock1Buddakin.SetBool("unlocked", true);
        }

        if (goDown)
        {
            transfKeyDoor.localPosition = transfKeyDoor.localPosition + new Vector3(0f, -0.1f, 0f); //Because the verticalDoor is a child of the button, I move the localPosition and not the globalPosition, that would move both button and door I think.
            
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
