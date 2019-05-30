using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D other) //Allows the player to jump only one time at a time. Only when reachs the ground, he is allowed to jump another time
    {
        Debug.Log("Hit belt");
    }
}
