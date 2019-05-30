using UnityEngine;
using System.Collections;

//This HolyLaser converts the player (dies) to Sciemfology is he stays into the light for at least 3 secs.
public class LaserHolyL2 : MonoBehaviour {

	private float timeToConversion;
	private bool isConvertingPlayer;
	private Ninja player;

	void Start () {
		timeToConversion = 3f;
		player = FindObjectOfType<Ninja> ();
	}

	void Update () {

		if (isConvertingPlayer == true)
        {
			timeToConversion -= Time.deltaTime;
			if (timeToConversion <= 0)
            {
				player.Die();
				isConvertingPlayer = false;
                SoundManager.Instance.LaserHolyExit();
                timeToConversion = 3f;
            }
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player" && !isConvertingPlayer){
			player.OnLamp = true;
			player.LampZone = gameObject.transform.parent.name;
			isConvertingPlayer = true;
			SoundManager.Instance.LaserHolyEnter();
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player"){
			player.OnLamp = false;
			player.LampZone = "";
			isConvertingPlayer = false;
			SoundManager.Instance.LaserHolyExit();
			timeToConversion = 3f;
		}
	}
}