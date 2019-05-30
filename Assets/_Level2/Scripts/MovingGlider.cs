using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingGlider : MonoBehaviour {

	public Sprite sprite;
	private Ninja ninja;

	bool activate;
	Sprite ninjaGlider;

	void Start () {
		activate = false;
		ninja = FindObjectOfType<Ninja> ();
		ninjaGlider = GetComponent<SpriteRenderer> ().sprite;
		GetComponent<SpriteRenderer> ().sprite = sprite;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player" && !activate){
			activate = true;
			ninja.Active (false);
			GetComponent<SpriteRenderer>().sprite = ninjaGlider;
			StartCoroutine (MoveGlider());
		}
	}

	IEnumerator MoveGlider() {
		GetComponent<Animator> ().enabled = true;
		yield return new WaitForSeconds (3.5f);
		float fadeTime = GameObject.Find ("Main Screen").GetComponent<Fading> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
