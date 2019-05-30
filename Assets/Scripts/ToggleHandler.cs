using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleHandler : MonoBehaviour {

	public bool isMusic;
	public bool isSoundEffect;

	void Start () {
		
	}
	
	void Update () {
		
	}

	void OnGUI() {
		Toggle toggle = gameObject.GetComponent<Toggle> ();

		if ( isMusic ) {
			toggle.isOn = SoundManager.Instance.IsMusicOn;
		}

		if ( isSoundEffect ) {
			toggle.isOn = SoundManager.Instance.IsSoundEffectsOn;
		}
	}

}