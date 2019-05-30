using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public float distance;
	public bool vertical;
	public float time;

	void Start () {
		iTween.MoveAdd(gameObject, iTween.Hash(vertical ? "y" : "x",  distance, "time", time, "looptype", iTween.LoopType.pingPong, "easetype", iTween.EaseType.linear, "delay", Random.Range(0, .4f)));
	}
}
