using UnityEngine;
using System.Collections;

//This HolyLaser converts the player (dies) to Sciemfology is he stays into the light for at least 3 secs.
public class LaserHoly : MonoBehaviour {

    private float timeToConversion;
    private bool isConvertingPlayer;
    private Zozzy player;
	
    // Use this for initialization
	void Start () {
        
        timeToConversion = 3f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Zozzy>();
    }
	
	// Update is called once per frame
	void Update () {

        if (isConvertingPlayer == true)
        {
            timeToConversion -= Time.deltaTime;
            if (timeToConversion <= 0)
            {
                player.HealthZozzy = 0;
                isConvertingPlayer = false;
                SoundManager.Instance.LaserHolyExit();
                timeToConversion = 3f;
            }
        }
        
    }


    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player" && !isConvertingPlayer)
        {
            isConvertingPlayer = true;
            SoundManager.Instance.LaserHolyEnter();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isConvertingPlayer = false;
            SoundManager.Instance.LaserHolyExit();
            timeToConversion = 3f;
        }
    }
}
