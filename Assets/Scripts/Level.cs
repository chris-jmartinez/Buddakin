using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

	public int level;

	void Start () {
		GameManagerL1.Instance.setLevel (level);
	}
	
	void Update () {
		
	}

}
