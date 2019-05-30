using UnityEngine;
using System.Collections;

public class CrystalZone : MonoBehaviour {

	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;
	public Sprite sprite4;
	public Sprite sprite5;
	public Sprite sprite6;
	public Sprite sprite7;

	private float health;

	BoxCollider2D bc;
	SpriteRenderer sr;
	Ninja ninja;
	bool onZone;
    bool playerDead = false;

	void Start () {
		health = 140f;
		ninja = FindObjectOfType<Ninja> ();
		bc = GetComponent<BoxCollider2D>() as BoxCollider2D; 
		sr = transform.FindChild ("Crystal").GetComponent<SpriteRenderer> ();
		onZone = false;
	}
	
	void Update () {
		if (health >= 121 && health < 141 ){
			sr.sprite = sprite1;
		} else if (health >= 101 && health < 121 ){
			sr.sprite = sprite2;
		} else if (health >= 81 && health < 101 ){
			sr.sprite = sprite3;
		} else if (health >= 61 && health < 81 ){
			sr.sprite = sprite4;
		} else if (health >= 41 && health < 61 ){
			sr.sprite = sprite5;
		} else if (health >= 21 && health < 41 ){
			sr.sprite = sprite6;
		} else {
			sr.sprite = sprite7;
			if (onZone && !playerDead) StartCoroutine (KillCharacter ());
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Player") {
			health -=25f;
			onZone = true;
		}
	}

	void OnCollisionStay2D(Collision2D other){
		if (other.gameObject.tag == "Player") {
			health -=0.2f;
		}
	}

	void OnCollisionExit2D(Collision2D other){
		if (other.gameObject.tag == "Player") {
			health -=25f;
			onZone = false;
		}
	}

	IEnumerator KillCharacter(){
        playerDead = true;
		bc.isTrigger = true;
		yield return new WaitForSeconds (.8f);
		ninja.Die ();
		yield return new WaitForSeconds (.5f);
		restart ();
        playerDead = false;
		yield return null;
	}

	public void restart (){
		health = 140f;
		sr.sprite = sprite1;
		bc.isTrigger = false;
	}
}