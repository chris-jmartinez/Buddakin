using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryL1 : MonoBehaviour {

    /* Put this script in Inventory Canvas prefab. Then, add with the unity editor the inventoryIconEmpty and the Tnt 
     * object instantiated, if it is the 1st level.
     * Then in the objects the player has to collect (keys, for example) add the script "PickableObject", and in that script,
     * via the unity editor, add the IconObject that has to appear in the inventory and say if it is a droppable object (only for tnt).
     * */

    public static InventoryL1 Instance = null;
    public List<GameObject> inventoryList;
    public Sprite m_iconInventoryEmpty;
    public GameObject m_tnt;

    private GameObject player;

    private int currentIndexInventory;
    public int CurrentIndexInventory
    {
        get { return currentIndexInventory; }
        set
        {
            currentIndexInventory = value;
        }
    }
    
    


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }



    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");       
        inventoryList.Clear(); //CLEARED LIST AT START
        currentIndexInventory = -1;
        
        if (GameManagerL1.Instance.currentLevel == 1)
        {
            pickupObject(m_tnt);
        }

        if (inventoryList.Count == 0)
        {
            setEmptyIcon();
            currentIndexInventory = -1;
        }
    }
	
	// Update is called once per frame
	void Update () {

       
        if (Input.GetKeyDown(KeyCode.K) && inventoryList.Count > 1)
        {
            scrollInventory();
        }

        
	}

	public void setUp() {
		player = GameObject.FindGameObjectWithTag("Player");       
		inventoryList.Clear(); //CLEARED LIST AT START
		currentIndexInventory = -1;
		pickupObject(m_tnt);
	}
    
	public void pickupObject(GameObject pickedUpObject)
    {
        inventoryList.Add(pickedUpObject);
        if (inventoryList.Count == 1)
        {
            currentIndexInventory = 0;
        } else if (inventoryList.Count > 1)
        {
            currentIndexInventory++;
        }
        gameObject.GetComponent<Image>().sprite = pickedUpObject.GetComponent<PickableObject>().m_spriteIconObject as Sprite;
        //Debug.Log("PICKED UP OBJECT: " + pickedUpObject.name + " AND SPRITE icon is set,,," + "Inventory count " + inventoryList.Count + "current Index Inventory: " + currentIndexInventory);
    }


    public void removeUsedObject()
    {
        inventoryList.Remove(inventoryList[currentIndexInventory]);

        updateInventoryAfterRemove();
 
    }


    public void updateInventoryAfterRemove()
    {
        if (inventoryList.Count <= 0)
        {
            setEmptyIcon();
            currentIndexInventory = -1;
        }
        else if (inventoryList.Count == 1)
        {
            currentIndexInventory = 0;
            gameObject.GetComponent<Image>().sprite = inventoryList[currentIndexInventory].gameObject.GetComponent<PickableObject>().m_spriteIconObject as Sprite;
        }
        else if (inventoryList.Count > 1)
        {
            if (currentIndexInventory > inventoryList.Count - 1)
            {
                scrollInventory();
            }else
            {
                gameObject.GetComponent<Image>().sprite = inventoryList[currentIndexInventory].gameObject.GetComponent<PickableObject>().m_spriteIconObject as Sprite;
            }
        }
    }

    public void setEmptyIcon()
    {
        gameObject.GetComponent<Image>().sprite = m_iconInventoryEmpty;
    }

    public void dropDroppableObject()
    {
        if (inventoryList[currentIndexInventory].gameObject.GetComponent<PickableObject>().m_droppableObject == true)
        {
            Vector3 traslationRespectPlayer = new Vector3(3f, 0f, 0f);
            GameObject droppedItem = inventoryList[currentIndexInventory];
            droppedItem.transform.position = player.transform.position + traslationRespectPlayer;
            droppedItem.transform.rotation = player.transform.rotation;
            droppedItem.SetActive(true);

            inventoryList.Remove(inventoryList[currentIndexInventory]);
            updateInventoryAfterRemove();
        }
    }


    public void scrollInventory()
    {
        if (inventoryList.Count > 1) { 
            if ((currentIndexInventory + 1) <= (inventoryList.Count - 1))
            {
                currentIndexInventory += 1;
                gameObject.GetComponent<Image>().sprite = inventoryList[currentIndexInventory].GetComponent<PickableObject>().m_spriteIconObject as Sprite;
            }
            else
            {
                currentIndexInventory = 0;
                gameObject.GetComponent<Image>().sprite = inventoryList[currentIndexInventory].GetComponent<PickableObject>().m_spriteIconObject as Sprite;
            }
        } else if (inventoryList.Count <= 0)
        {
            setEmptyIcon();
        }
    }




}
