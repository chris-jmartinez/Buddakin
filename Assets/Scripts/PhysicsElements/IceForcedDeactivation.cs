using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceForcedDeactivation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Zozzy>().TouchingIce = false;
            other.gameObject.GetComponent<Zozzy>().JumpedFromIce = false;
        }
    }
}
