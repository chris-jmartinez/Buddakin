using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

	void Start() {
	
	}
	
	IEnumerator OnTriggerEnter2D(Collider2D other) {
		float fadeTime = GameObject.Find ("Main Screen").GetComponent<Fading> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

}
