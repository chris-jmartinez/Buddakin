using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PedrieuDamageZoneS : MonoBehaviour {

    GameObject playerGameObject;
    PedrieuS pedrieuScript;
    Animator pedrieuAnimator;

    public float m_waitTimeBeforeAnotherBitePublic = 0.5f;
    private float waitTimeBeforeAnotherBiteFlowing;

    private bool pedrieuIsAttacking;
    public bool PedrieuIsAttacking
    {
        get { return pedrieuIsAttacking; }
        set
        {
            pedrieuIsAttacking = value;
        }
    }

    private SkeletonAnimation pedrieuSkeletonAnimation;

    // Use this for initialization
    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player") as GameObject;
        waitTimeBeforeAnotherBiteFlowing = m_waitTimeBeforeAnotherBitePublic;
        pedrieuScript = transform.parent.gameObject.GetComponent<PedrieuS>();
        //pedrieuAnimator = transform.parent.gameObject.GetComponent<Animator>();
        pedrieuSkeletonAnimation = transform.parent.gameObject.GetComponent<SkeletonAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pedrieuIsAttacking)
        {
            pedrieuScript.HorizontalInputMov = 0;
            waitTimeBeforeAnotherBiteFlowing -= Time.deltaTime;
            if (waitTimeBeforeAnotherBiteFlowing <= 0)
            {
                playerGameObject.GetComponent<Zozzy>().HealthZozzy -= transform.parent.gameObject.GetComponent<Pedrieu>().PedrieuDamage;
                waitTimeBeforeAnotherBiteFlowing = m_waitTimeBeforeAnotherBitePublic;
            }
        }

    }



    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Contact with: " + other.gameObject.name);

        if (other.gameObject.tag == "Player" && !pedrieuIsAttacking)
        {
            Debug.Log("PLAYER IN PEDRIEU DAMAGE ZONE, triggered");
            pedrieuIsAttacking = true;
            //pedrieuAnimator.SetBool("attack", true);
            pedrieuSkeletonAnimation.state.SetAnimation(1, pedrieuScript.attackAnimationName, true);
            //pedrieuSkeletonAnimation.state.set
            pedrieuScript.ReactionWaitTime = 0.3f;
            //Attacks the first time instantly (when enters the collider trigger), then attacks every second
            playerGameObject.GetComponent<Zozzy>().HealthZozzy -= pedrieuScript.PedrieuDamage;
            //Debug.Log("Player hit, health now is: " + playerGameObject.GetComponent<Zozzy>().HealthZozzy);
        }
        /*
        else if (other.gameObject.tag == "RocketShot")
        {
            transform.parent.gameObject.GetComponent<Pedrieu>().HealthPedrieu -= other.gameObject.GetComponent<RocketShot>().DamageRocket;
            Debug.Log("Pedrieu hit, health now is: " + transform.parent.gameObject.GetComponent<Pedrieu>().HealthPedrieu);
            
            GameManager.Instance.Scored(100); //Scored points
            
        }*/
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && pedrieuIsAttacking)
        {
            Debug.Log("PLAYER EXIT FROM DAMAGE ZONE, triggered");
            pedrieuIsAttacking = false;
            //pedrieuAnimator.SetBool("attack", false);
            //pedrieuSkeletonAnimation.state.AddAnimation(0, pedrieuScript.noticedAnimationName, true, 0f);
            pedrieuSkeletonAnimation.state.SetEmptyAnimation(1, 1f);
            //pedrieuSkeletonAnimation.state.ClearTrack(1);
            waitTimeBeforeAnotherBiteFlowing = m_waitTimeBeforeAnotherBitePublic;
        }

    }
}
