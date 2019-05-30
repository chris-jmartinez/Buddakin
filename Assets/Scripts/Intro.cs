using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

	Animator anim;

	public void idle(string characterName) {
		anim = GameObject.Find(characterName).GetComponent<Animator>() as Animator;
		anim.SetFloat ("speed", 0f);
		switch (characterName) {
			case "Ninja":
				anim.Play ("Ninja Idle");
				break;
			case "Zozzy":
				anim.Play ("ZozzyIdleAnimation");
				break;
		}
	}

	public void walk(string characterName) {
		anim = GameObject.Find(characterName).GetComponent<Animator>() as Animator;
		anim.SetFloat ("speed", 10f);
		if ( characterName == "Ninja" ) {
			anim.Play ("Ninja Walk");
		}
		if ( characterName == "Zozzy" ) {
			anim.Play ("ZozzyRunAnimation");
		}
	}

}
