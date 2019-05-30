using UnityEngine;
using System.Collections;

public class ManInBlackL1 : MonoBehaviour {

    [SerializeField]
    private int healthMib = 100;
    public int HealthMib
    {
        get { return healthMib; }
        set
        {
            healthMib = value;
        }
    }
    
    public int m_mibScorePoints = 10;

    //CAUTION! PUBLIC VARIABLES! ACTUALIZING THEM IN THE CODE DOESN'T ACTUALIZE THEM IN THE UNITY EDITOR!
    public float m_speed = 20f;
    public float m_jumpForce = 15f;
    public float m_reloadGunTime = 0.8f;
    public float m_timeWalkingBeforeTurnPublic = 2f;
    public bool m_jumpMibActivated = true; //from interface we can decide if the Man in black can jump or not (adds difficulty)
    public bool m_mibDoesSamePath = true;
    public float m_distanceMibAttacksX = 9f;
    public float m_distanceMibAttacksY = 4f;
    [Range(0, 3)]
    public int m_abilityToJump = 1;
    public bool m_abilityToWalk = true;
    public GameObject m_shotPrefabMib;
    public Transform m_shotPosition;
    public bool m_mibTrapCage = false;

    


    private float timeWalkingBeforeTurn;
    
    private bool jumping = false;
    public bool Jumping
    {
        get { return jumping; }
        set
        {
            jumping = value;
        }
    }
    private bool readyToShootGunloaded = true;
    private float horizontalInputMov = 0f; //Varies between -1 and 1, and it's detected in input when you use horizontal inputs like arrows
    
    public bool m_facingRight = true;
    private bool facingRightStored;
    private bool mibDead = false;
    private int counterTimesEnteredAttackSectionAtEveryUpdate = 0;
    private bool manInBlackAttacking = false;
    private Rigidbody2D rigidBodyMib;
    private Transform transformMib;
    private Animator animator;

    private GameObject player;
    private GameObject playerShot;
    private float distanceBetweenMibAndPlayerInX;
    private float distanceBetweenMibAndPlayerInY;
    private float distanceBetweenMibAndPlayerShot;

    // Use this for initialization
    void Start () {
        rigidBodyMib = GetComponent<Rigidbody2D>() as Rigidbody2D; //"as" does the casting
        transformMib = GetComponent<Transform>() as Transform;
        animator = GetComponent<Animator>() as Animator;
        player = GameObject.FindGameObjectWithTag("Player") as GameObject;
        timeWalkingBeforeTurn = m_timeWalkingBeforeTurnPublic;
        //playerShot = GameObject.FindGameObjectWithTag("RocketShot") as GameObject; //HOW TO DETECT "RocketShot" prefab? Error when I try "RocketShot" because it's a prefab and instanciated only when player fires. "RocketShotPosition" it's only a bad workaround and doesn't work well.
    }
	
	// Update is called once per frame
	void Update () {

        if (!mibDead)
        {
            if (healthMib <= 0)
            {
                mibDead = true;
                mibDies();
            }else
            {
                mibBehaviour();
            }
        }

    }




    void mibBehaviour()
    {

        //m_horizontal = Input.GetAxis("Horizontal");

        distanceBetweenMibAndPlayerInX = transformMib.position.x - player.transform.position.x;
        distanceBetweenMibAndPlayerInY = transformMib.position.y - player.transform.position.y;
        //distanceBetweenMibAndPlayerShot = transformMib.position.x - playerShot.transform.position.x;

        if (!manInBlackAttacking && m_abilityToWalk)
        { //If the man in black is not attacking, walks around (some walking in right direction, some walking in left direction)
            if (timeWalkingBeforeTurn <= 0) //It's time to turn and change direction (TimeWalkingBeforeTurn is now 0, will be resetted to 5 seconds)
            {
                if (m_facingRight == true)
                {
                    flipAnimationCharacter();
                    //Debug.Log("flipped animation (when was right)"); 
                }
                else if (m_facingRight == false)
                {
                    flipAnimationCharacter();
                    //Debug.Log("flipped animation (when was left)"); 
                }
                timeWalkingBeforeTurn = m_timeWalkingBeforeTurnPublic; //TimeWalkingBeforeTurn is now 0, it's resetted to 5 seconds of walking time.
            }
            else //Else, the man in black walks in the direction he is facing, because the WalkingTimeBeforeTurn has not reached 0 yet:
            {
                if (m_facingRight == true)
                {
                    horizontalInputMov = 1;
                    //Debug.Log("horizontalInputMov = +1");   
                    //Debug.Log("Value of facingRight:" + facingRight);
                }
                else if (!m_facingRight)
                {
                    horizontalInputMov = -1;
                    //Debug.Log("horizontalInputMov = -1");  
                    //Debug.Log("Value of facingRight (second else):" + facingRight);
                }
            }


            timeWalkingBeforeTurn -= 0.03f; //Decrements time only when is not attacking ALTERNATIVE: timeWalkingBeforeTurn -= Time.deltaTime;

        }

        //Debug.Log("timerWalking value:" + timeWalkingBeforeTurn + "Time delta time value: " + Time.deltaTime);


        //ATTACK MODE
        //If player it's enough close to the ManInBlack and the ManInBlack has the gun loaded and it's not jumping...then fires to the player
        if (Mathf.Abs(distanceBetweenMibAndPlayerInX) < m_distanceMibAttacksX && Mathf.Abs(distanceBetweenMibAndPlayerInX) > 0.3f && Mathf.Abs(distanceBetweenMibAndPlayerInY) < m_distanceMibAttacksY)
        {
            manInBlackAttacking = true;
            horizontalInputMov = 0f; //When the man in black detects the player, he doesn't walk anymore (stops), points to the player and shoots.

            counterTimesEnteredAttackSectionAtEveryUpdate++;

            if (counterTimesEnteredAttackSectionAtEveryUpdate == 1)
                facingRightStored = m_facingRight; //Facing right is stored (only the first time the player enters the "ManInBlackAttackingZone" and after attacking the direction he was walking (same path) will be ripristinated after attacking, if needed (mibDoesSamePath it's a variable to state if we want he does the same path).

            //The man in black turns in the direction of the player when the player enters the "field where the Man in black can shoot the player"
            if (distanceBetweenMibAndPlayerInX > 0 && m_facingRight)
            {
                flipAnimationCharacter(); //NB! flipAnimationCharacter changes the value of facingRight                
            }

            else if (distanceBetweenMibAndPlayerInX < 0 && !m_facingRight)
            {
                flipAnimationCharacter();
            }

            //Sets the appropiate animation (ManInBlackPointing)
            animator.SetBool("pointing", true);
            animator.SetInteger("health", healthMib);

            // Shoot creation
            if (readyToShootGunloaded && jumping == false)
            {
                GameObject newGameObject = ObjectPoolingManager.Instance.GetObject(m_shotPrefabMib.name);
                newGameObject.transform.position = m_shotPosition.position;
                newGameObject.transform.localScale = m_shotPosition.localScale;

                // If man in black is facing left, when fires the bullet, also the bullet must go in that direction(left) (the rotation was not automatically set, only position)
                if (!m_facingRight)
                {
                    newGameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
                }
                else
                {
                    newGameObject.transform.rotation = m_shotPosition.rotation;

                }

                //Here I could istantiate the sound of the gun
                //				Instantiate (m_shot_prefab,
                //				m_shootleft.position,
                //				m_shootleft.rotation) 
                //				as GameObject;
                //SoundManager.Instance.PlayerShoots();

                readyToShootGunloaded = false;
                StartCoroutine(waitToShootAgain());
            }
        }
        else if ((Mathf.Abs(distanceBetweenMibAndPlayerInX) >= m_distanceMibAttacksX || Mathf.Abs(distanceBetweenMibAndPlayerInY) >= m_distanceMibAttacksY) && counterTimesEnteredAttackSectionAtEveryUpdate >= 1)
        {
            manInBlackAttacking = false;
            animator.SetBool("pointing", false);
            animator.SetInteger("health", healthMib);

            counterTimesEnteredAttackSectionAtEveryUpdate = 0;

            /*If the ManInBlack has to do the same path and the facingRight, before entering in attacking mode, 
             * was different from the facingRight that we have now (when he exit the attacking mode) then we 
             * flipTheAnimationCharacter (and so the facing), so he continues doing the same path he was doing before 
             * attacking.
             * */
            if (m_mibDoesSamePath && facingRightStored != m_facingRight)
            {
                flipAnimationCharacter();
            }

        }

    }





    void FixedUpdate()
    {
        if (!mibDead)
        {
            rigidBodyMib.velocity = new Vector2(horizontalInputMov * m_speed * Time.fixedDeltaTime, rigidBodyMib.velocity.y);

            animator.SetFloat("speed", Mathf.Abs(rigidBodyMib.velocity.x)); //it sets the animator's variable "speed" (caution with the capital letters) to the value specified. This allows the animator to recognize if it's time to change animation

            if (jumping && m_jumpMibActivated)
            {
                rigidBodyMib.AddForce(new Vector2(0f, m_jumpForce), ForceMode2D.Impulse);
                jumping = false;
            }
        }
    
    }

    IEnumerator waitToShootAgain()
    {

        yield return new WaitForSeconds(m_reloadGunTime);

        readyToShootGunloaded = true;
    }


    //Flips the animation of the character (useful when goes left)
    void flipAnimationCharacter()
    {
        Vector3 localScaleTr = transformMib.localScale; //gets the parameter "scale" of "transform" (that contains position, rotation, scale)
        localScaleTr.x = localScaleTr.x * -1f; //inverts the "x" of the parameter scale (so inverting the displayed animation)
        transformMib.localScale = localScaleTr;
        m_facingRight = !(m_facingRight);
    }


    void OnCollisionEnter2D(Collision2D other) //Allows the player to jump only one time at a time. Only when reachs the ground, he is allowed to jump another time
    {
        if ((other.gameObject.tag == "TerrainAndFloor" || other.gameObject.tag == "Platform" || other.gameObject.tag == "Ice") && jumping == true)
        {
            jumping = false;
        }

        

    }


    void OnTriggerEnter2D(Collider2D other) //The jumping (randomly) of the man in black, is implemented in the script ManInBlackL1JumpCollider, a child of ManInBlack that has the reference to this script and a collider created for that aim (let him jump).
    {
        /* HERE I COULD PUT DECREASE OF HEALTH IF OTHER IS ROCKETSHOT (but I have implemented it in the rocketshot onTrigger)
        
        */

    }




    void mibDies()
    {
        Debug.Log("Mib died");
        //SoundManager.Instance.mibDies();
        animator.SetInteger("health", healthMib);
        animator.SetBool("dead", mibDead);

        GameManagerL1.Instance.Scored(m_mibScorePoints);

        rigidBodyMib.drag = 5f;
        Collider2D[] collidersObject = gameObject.GetComponents<Collider2D>();
        foreach (Collider2D collider in collidersObject)
        {
            collider.enabled = false;
        }
        collidersObject[2].enabled = true;


        StartCoroutine(waitForAnimation());

        if (m_mibTrapCage)
        {
            TrapCage trapCageScript = GameObject.FindGameObjectWithTag("TrapCage").GetComponent<TrapCage>() as TrapCage;
            trapCageScript.NumberEnemiesCage -= 1;
        }
    }


    IEnumerator waitForAnimation()
    {
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);
        yield return null; //Put this always or the game can stuck / block.
    }



}
