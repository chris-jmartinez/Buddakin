using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

	public AudioClip clip;

	void Start () {
		SoundManager.Instance.ChangeClip (clip);
	}
	
	void Update () {
		
	}

}
