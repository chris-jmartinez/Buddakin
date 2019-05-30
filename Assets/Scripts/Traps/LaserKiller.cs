using UnityEngine;
using System.Collections;

public class LaserKiller : MonoBehaviour {

    private Zozzy playerScript;

    // Use this for initialization
    void Start () {

        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Zozzy>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerScript.HealthZozzy = 0;
        }
    }
}
