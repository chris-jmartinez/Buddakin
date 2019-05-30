using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    
    private bool playerHitOneTime = false;
    public bool m_delayedExplosion = false;
    
	// Use this for initialization
	void Start () {
        
	}
	

	// Update is called once per frame
	void Update () {
	
	}





    //void OnCollisionEnter2D(Collision2D other)
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("ON TRIGGER EXPLOSION, OTHER IS: " + other.name);
        
        StartCoroutine(waitForAnimation());
            
       if (other.gameObject.tag == "Player" && !playerHitOneTime)
       {
           playerHitOneTime = true;
           other.gameObject.GetComponent<Zozzy>().HealthZozzy -= 5;
           //Debug.Log("Actual Health Zozzy: " + other.gameObject.GetComponent<Zozzy>().HealthZozzy);
       }
        
    }



    IEnumerator waitForAnimation()
    {
        
        yield return new WaitForSeconds(1f);
        Debug.Log("Setting false explosion");
        gameObject.SetActive(false);
        playerHitOneTime = false;
        
        yield return null; //Put this always or the game can stuck / block.
    }



}
