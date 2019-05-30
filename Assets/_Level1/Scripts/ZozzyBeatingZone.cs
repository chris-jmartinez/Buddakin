using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZozzyBeatingZone : MonoBehaviour {

    private Zozzy zozzyScript;

	// Use this for initialization
	void Start () {
        zozzyScript = transform.parent.gameObject.GetComponent<Zozzy>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "ManInBlack1" || other.gameObject.tag == "Pedrieu"))
        {
            //Debug.Log("Mib or Pedrieu TRIGGER ENTER IN BEATING ZONE");

            zozzyScript.EnemyInTrigger = true;
            zozzyScript.TargetEnemy = other.gameObject as GameObject;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if ((other.gameObject.tag == "ManInBlack1" || other.gameObject.tag == "Pedrieu"))
        {
            //Debug.Log("Mib or Pedrieu TRIGGER EXIT FROM BEATING ZONE");

            zozzyScript.EnemyInTrigger = false;
            zozzyScript.TargetEnemy = null;
        }
    }
}
