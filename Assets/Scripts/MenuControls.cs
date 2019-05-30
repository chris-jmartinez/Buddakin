using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour {

	[Header("Intro Options")]
	public bool isIntro;

	[Header("Pause Menu Options")]
	public bool isPauseMenu;
	public Transform pauseMenu;
	public ScrollSnapRect scrollSnapRect;

	void Start() {		

	}

	void Update () {
		if ( Input.GetKeyDown(KeyCode.Escape) ) {
			if ( isPauseMenu ) {
				pause ();	
			}
			if ( isIntro ) {
				escape ();
			}
		}
	}

	public void pause() {
		if ( !pauseMenu.gameObject.activeInHierarchy ) {
			pauseMenu.gameObject.SetActive (true);
			Time.timeScale = 0;
			AudioListener.pause = true;
		} else {
			scrollSnapRect.restart ();
			pauseMenu.gameObject.SetActive (false);
			Time.timeScale = 1;
			AudioListener.pause = false;
		}
	}

	public void manageMusic(bool isMusicOn) {
		SoundManager.Instance.IsMusicOn = isMusicOn;

		if ( isMusicOn ) {
			SoundManager.Instance.RestartMusic ();
		} else {
			SoundManager.Instance.PauseMusic ();
		}
	}

	public void manageSoundEffects(bool isSoundEffectsOn) {
		SoundManager.Instance.IsSoundEffectsOn = isSoundEffectsOn;
	}

	public void restart() {
		GameManagerL1.Instance.CurrentScorePlayer = 0;
		StartCoroutine (changeScene (0));
	}

	public void previousLevel() {
		int scene = GetScene(SceneManager.GetActiveScene().buildIndex - 1);
		StartCoroutine (changeScene (scene));
	}

	public void nextLevel() {
		int scene = GetScene(SceneManager.GetActiveScene().buildIndex + 1);
		StartCoroutine (changeScene (scene));
    }

	public void escape() {
		StartCoroutine (changeScene (SceneManager.GetActiveScene().buildIndex + 1));
	}

	public void quit() {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

	private int GetScene(int scene) {
		if ( scene == 2 ) {
			return 5;
		}

		if ( scene == 6 ) {
			return 3;
		}

		return scene;
	}

	private void resetValues() {
		Time.timeScale = 1;
		AudioListener.pause = false;
	}

	private IEnumerator changeScene(int scene) {
		resetValues();
		float fadeTime = GameObject.Find ("Main Screen").GetComponent<Fading> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene(scene);
	}

}