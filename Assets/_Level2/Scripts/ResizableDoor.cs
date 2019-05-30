using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizableDoor : MonoBehaviour {

	void Start () {
		StartCoroutine (PlayAnimation());
	}

	IEnumerator PlayAnimation(){
		GetComponent<Animator> ().SetBool("play", true);
		yield return new WaitForSeconds (1.8f);
		GetComponent<Animator> ().SetBool("play", false);
		GetComponent<CircleCollider2D> ().isTrigger = true;
		gameObject.SetActive (false);
		this.enabled = false;
		yield return null;
	}
}
