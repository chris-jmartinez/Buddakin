using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private bool hitCheckpoint = false;
    private Animator animator;
	// Use this for initialization
	void Start () {
        animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !hitCheckpoint)
        {
            hitCheckpoint = true;
            GameManagerL1.Instance.m_currentCheckpoint = gameObject;
            animator.SetBool("activated", true);
        }
    }
}
