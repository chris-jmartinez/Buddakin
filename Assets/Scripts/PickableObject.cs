using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour {


    public Sprite m_spriteIconObject; //The sprite, added from the editor.
    //public string m_objectName; //Example: "RedKey", "TNT", "Note1". Added from the editor.
    public bool m_droppableObject; //If the object is the TNT, this must be true. Set from the editor.

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && gameObject.tag != "Tnt")
        {
            InventoryL1.Instance.pickupObject(gameObject);
            //SoundManager.Instance.pickup();
            //Debug.Log("Item added to INVENTORY");
            gameObject.SetActive(false); //Maybe it will be set active false also into the inventory?
        }
    }
}
