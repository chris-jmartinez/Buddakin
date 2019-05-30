using UnityEngine;
using System.Collections;

public class KeyVerticalDoor : MonoBehaviour {

    //public string m_neededKey; //Write in the editor which is the needed key for this door (it's a string)
    public GameObject m_neededKey; //Place here, with the unity editor, the key needed to open the door
    public Sprite m_buttonPressedSprite;
    public string m_messageName;

    private bool playerIsNearButton;
    

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
        if (playerIsNearButton && ( Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) ) && !buttonPressed && checkselectedKeyInTheInventory()) //!!!Add Condition where it has the red key in its inventary and he used it
        {
            
            goDown = true; //In the script of the vertical door there will be the command to stop and put "goDown"=false
            buttonPressed = true;
            
            //gameObject.GetComponent<SpriteRenderer>().sprite = m_buttonPressedSprite as Sprite; //NOT WORKING FOR NOW, BUT WOULD BE BETTER
            Debug.Log("Sprite to change: " + m_buttonPressedSprite);
            animator.SetBool("insertedKey", true);
            InventoryL1.Instance.removeUsedObject();
        }else if (playerIsNearButton && (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !buttonPressed && searchKeyInInventory() == false)
        {
            //Buddakin says "You need the red key"
        }

        if (goDown)
        {
            transfVerticalDoor.localPosition = transfVerticalDoor.localPosition + new Vector3(0f, -0.4f, 0f); //Because the verticalDoor is a child of the button, I move the localPosition and not the globalPosition, that would move both button and door I think.
            
        }

    }


    private bool searchKeyInInventory()
    {
        foreach (GameObject item in InventoryL1.Instance.inventoryList)
        {
            //PREVIOUS VERS: if (item.gameObject.GetComponent<PickableObject>().m_objectName == m_neededKey)
            if (item.gameObject == m_neededKey)
            {
                //Debug.Log("NEEDED KEY IS IN INVENTORY, but maybe is not selected");
                TipManager.sendMessage(m_messageName);
                return true;
            }
        }

        return false;
    }

    private bool checkselectedKeyInTheInventory()
    {


        //PREVIOUS VERSION WAS: if (Inventory.Instance.inventoryList[Inventory.Instance.CurrentIndexInventory].gameObject.GetComponent<PickableObject>().m_objectName == m_neededKey)
        if (InventoryL1.Instance.inventoryList[InventoryL1.Instance.CurrentIndexInventory].gameObject == m_neededKey)
        {
            //Debug.Log("NEEDED KEY IS IN INVENTORY! OK, OPENING DOOR.  Current selected item is: " + InventoryL1.Instance.inventoryList[InventoryL1.Instance.CurrentIndexInventory].gameObject + " ; THE NEEDED KEY IS: " + m_neededKey);
            return true;
        }else
        {
            //Debug.Log("KEY NOT SELECTED, inventory current selected item: " + InventoryL1.Instance.inventoryList[InventoryL1.Instance.CurrentIndexInventory].gameObject + "   WHILE NEEDED KEY IS: " + m_neededKey);
            return false;
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
