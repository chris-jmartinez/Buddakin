using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformVertical : MonoBehaviour {

    public int yAmount;
    public int secondsToReachMax;
    // Use this for initialization
    void Start()
    {
        iTween.MoveAdd(gameObject, iTween.Hash("y", yAmount, "time", secondsToReachMax, "looptype", iTween.LoopType.pingPong, "easetype", iTween.EaseType.linear, "delay", Random.Range(0, .4f)));
    }

    // Update is called once per frame
    void Update()
    {

    }


}
