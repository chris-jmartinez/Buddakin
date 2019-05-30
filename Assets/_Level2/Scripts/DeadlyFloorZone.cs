using UnityEngine;
using System.Collections;

public class DeadlyFloorZone : MonoBehaviour {

	[SerializeField]
	private float damage = .1f;
	Ninja ninja;
	//BoxCollider2D bc;

	void Start () {
		ninja = FindObjectOfType<Ninja> ();
		//bc = GetComponent<BoxCollider2D>() as BoxCollider2D; 
	}
	
	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player") {
			ninja.Damage (damage);
			//StartCoroutine (KillCharacter());
		}
	}

	/*
	IEnumerator KillCharacter(){
		ninja.Die ();
		yield return new WaitForSeconds (1);
		bc.isTrigger = true;
		yield return null;
	}*/
}
