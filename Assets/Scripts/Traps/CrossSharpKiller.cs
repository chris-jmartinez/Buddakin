using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossSharpKiller : MonoBehaviour {

    private Zozzy playerScript;

    // Use this for initialization
    void Start()
    {

        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Zozzy>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerScript.HealthZozzy = 0;
        }
    }
}
