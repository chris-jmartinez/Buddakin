using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TntPlacingZone : MonoBehaviour {

    
    public GameObject m_neededTnt;
    public string m_messageName;


    private bool playerIsInTntZone, tntPlaced;
    private bool verticalDoorGoDown;
    public bool VerticalDoorGoDown
    {
        get { return verticalDoorGoDown; }
        set
        {
            verticalDoorGoDown = value;
        }
    }
    private Transform transfVerticalDoor;
    private bool doorActivated = false;

    // Use this for initialization
    void Start()
    {
        transfVerticalDoor = transform.GetChild(0).gameObject.transform;
        verticalDoorGoDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsInTntZone && (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !tntPlaced && checkSelectedTntInTheInventory())
        {
            //GetComponent<SpriteRenderer>().sprite = buttonPressed;
            InventoryL1.Instance.dropDroppableObject();
            tntPlaced = true;
            TipManager.sendMessage(m_messageName);
            if (!doorActivated) { 
                verticalDoorGoDown = true;
                doorActivated = true;
            }
            //In the script of the vertical door there will be the command to stop and put "goDown"=false

        }


        if (verticalDoorGoDown)
        {
            Debug.Log("Vertical door go down value: " + verticalDoorGoDown);
            transfVerticalDoor.localPosition = transfVerticalDoor.localPosition + new Vector3(0f, -0.2f, 0f); //Because the verticalDoor is a child of the button, I move the localPosition and not the globalPosition, that would move both button and door I think.

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerIsInTntZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerIsInTntZone = false;
        }
    }

    private bool checkSelectedTntInTheInventory()
    {
        
        if (InventoryL1.Instance.inventoryList[InventoryL1.Instance.CurrentIndexInventory].gameObject == m_neededTnt)
        {
            Debug.Log("TNT IS IN INVENTORY! OK, placing it");
            return true;
        }
        else
        {
            Debug.Log("TNT NOT SELECTED, inventory current instance name: " + InventoryL1.Instance.inventoryList[InventoryL1.Instance.CurrentIndexInventory].gameObject + "   WHILE NEEDED ITEM IS: " + m_neededTnt);
            return false;
        }
        

    }
}
