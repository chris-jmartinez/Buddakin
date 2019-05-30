using UnityEngine;
using System.Collections;

public class ManInBlackL1JumpCollider : MonoBehaviour {

    ManInBlackL1 manInBlackL1Script;

	// Use this for initialization
	void Start () {
        manInBlackL1Script = transform.parent.gameObject.GetComponent <ManInBlackL1>() as ManInBlackL1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter2D(Collider2D other) //Allows the player to jump only one time at a time. Only when reachs the ground, he is allowed to jump another time
    {
        if (other.gameObject.tag == "RocketShot")
        {
            if (manInBlackL1Script.Jumping == false && manInBlackL1Script.m_jumpMibActivated)
            {

                switch (manInBlackL1Script.m_abilityToJump)
                {
                    case 0: //In this case the man in black doesn't jump at all
                        break;
                    case 1: //The ability to jump will be very poor (1/7 of chances to jump, using random to decide)
                        if (Random.Range(0, 8) == 0)
                        {
                            manInBlackL1Script.Jumping = true;
                        }
                        break;
                    case 2: //The ability to jump will be medium (50% success)
                        if (Random.Range(0, 2) == 1)
                        {
                            manInBlackL1Script.Jumping = true;
                        }
                        break;
                    case 3: //The ability to jump will be perfect (he jumps all the rockets/player shots)
                        manInBlackL1Script.Jumping = true;
                        break;
                }

                

            }
        }

    }
}
