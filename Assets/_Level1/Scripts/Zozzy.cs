using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Zozzy : MonoBehaviour{

    [Header("Zozzy properties")]
    public GameObject m_shotRocketPrefab;

    public Transform m_shotPosition;

    public Slider healthBarSlider;
    public Text healthText;

    public Text m_scoreTextValue;

    public float m_reloadBazookaTime = 0.5f;
    private float reloadBazookaTimeIncrementing;
    private bool readyToShootBazookaLoaded = true;

    [SerializeField]
    private int healthZozzy = 100;
    public int HealthZozzy
    {
        get { return healthZozzy; }
        set
        {
            healthZozzy = value;
        }
    }
    public float m_recoil = -60f;
    public float m_speed = 500f; //Speed of the movement

   
    public bool zozzyDead = false;
    private float horizontalInputMov = 0f; //Indicates intensity of input Horizontal movement (right and left)  
    private bool facingRight = true; //keeps in memory the direction he's going

    private bool isBeating = false;

    public bool EnemyInTrigger
    {
        get { return enemyInTrigger; }
        set
        {
            enemyInTrigger = value;
        }
    }
    private bool enemyInTrigger = false;


    public GameObject TargetEnemy
    {
        get { return targetEnemy; }
        set
        {
            targetEnemy = value;
        }
    }
    private GameObject targetEnemy;

    private bool jumpPressed = false;
    private bool jumping = false;
    private float jumpForceStandard = 40f;
    

    //private float speedWhenHighDrag = 800f;

    //private float highDrag = 15f;

    private bool touchingIce;
    public bool TouchingIce
    {
        get { return touchingIce; }
        set
        {
            touchingIce = value;
        }
    }
    private bool touchingConveyorBelt;

    private bool jumpedFromIce;
    public bool JumpedFromIce
    {
        get { return jumpedFromIce; }
        set
        {
            jumpedFromIce = value;
        }
    }

    private bool jumpedFromBelt;


    public Slider m_reloadBar;
    public Text m_reloadBarText;


    private Rigidbody2D rigidBodyZozzy;
    private Transform transformZozzy;

    private Animator animator; //manages the animations
    private ConstrainedCamera constrainedCameraScript;

    /*This script has very specific (and good working) behaviours for Ice, for conveyor belt, and also
     * the flipCharacter has some relation with the camera (in fact the player should always stay a bit in the left
     * part of the screen)
     * */
   
    
    // Use this for initialization
    void Start () {
        rigidBodyZozzy = GetComponent<Rigidbody2D>() as Rigidbody2D; //"as" does the casting
        transformZozzy = GetComponent<Transform>() as Transform;
        animator = GetComponent<Animator>() as Animator;
        touchingIce = false;
        constrainedCameraScript = Camera.main.GetComponent<ConstrainedCamera>();
        //base.base.healthBarSlider.value = healthZozzy;
		InventoryL1.Instance.setUp();
    }


    // Update is called once per frame
    //Here I put reception of inputs (like keyboard, but if it's continuos like ps4 maybe I put in fixedUpdate)
    void Update()
    {
		m_scoreTextValue.text = GameManagerL1.Instance.CurrentScorePlayer.ToString();

        animator.SetInteger("health", healthZozzy);
        healthBarSlider.value = Mathf.Ceil(healthZozzy);
        healthText.text = Mathf.Clamp(Mathf.CeilToInt(healthZozzy), 0, 100).ToString();

        

        if (!readyToShootBazookaLoaded)
        {
            reloadBazookaTimeIncrementing += Time.deltaTime;
            m_reloadBar.value = Mathf.Clamp(reloadBazookaTimeIncrementing, 0, m_reloadBazookaTime);
        }

        if (!zozzyDead)
        {
            if (HealthZozzy <= 0)
            {
                zozzyDead = true;
                zozzyDies();
            }else
            {
                zozzyBehaviour();
            }

            
        }
        
        
        
    }




    void zozzyBehaviour()
    {
        

        horizontalInputMov = Input.GetAxis("Horizontal");
        jumpPressed = Input.GetButtonDown("Jump");

        if (jumpPressed && jumping == false)
        {           
            rigidBodyZozzy.AddForce(new Vector2(0f, jumpForceStandard), ForceMode2D.Impulse);
            jumping = true;

            if (touchingIce)
            {
                jumpedFromIce = true;
                Debug.Log("JumpedFromIce TRUE in UPDATE");
            }
        }


        if (horizontalInputMov < 0 && facingRight)
            flipAnimationCharacter();
        else if (horizontalInputMov > 0 && !facingRight)
            flipAnimationCharacter();


        if (Input.GetKeyDown(KeyCode.J) && !isBeating)
        {
            isBeating = true;
            animator.SetBool("isBeating", true);
            StartCoroutine(waitForBeatingAnimation());
            

            if (enemyInTrigger)
            {
                //Debug.Log("enemy in trigger AND beating. Subtracting Life.");
                if (targetEnemy.gameObject.tag == "ManInBlack1")
                {
                    SoundManager.Instance.zozzyPunch();
                    targetEnemy.gameObject.GetComponent<ManInBlackL1>().HealthMib -= 25;
                    Debug.Log("Mib HIT with BEATING. Health now: " + targetEnemy.gameObject.GetComponent<ManInBlackL1>().HealthMib);
                    if (targetEnemy.gameObject.GetComponent<ManInBlackL1>().HealthMib <= 0)
                    {
                        enemyInTrigger = false;
                        targetEnemy = null;
                    }
                }
                else if (targetEnemy.gameObject.tag == "Pedrieu")
                {
                    targetEnemy.gameObject.GetComponent<Pedrieu>().HealthPedrieu -= 25;
                    SoundManager.Instance.zozzyPunch();
                    SoundManager.Instance.pedrieuHit();
                    if (targetEnemy.gameObject.GetComponent<Pedrieu>().HealthPedrieu <= 0)
                    {
                        enemyInTrigger = false;
                        targetEnemy = null;
                    }
                }
            }

        }


        //Perceives if the player has shot
        if (Input.GetKeyDown(KeyCode.H) && readyToShootBazookaLoaded && !isBeating)
        {
            // shot creation
            GameObject newGameObject = ObjectPoolingManager.Instance.GetObject(m_shotRocketPrefab.name);

            newGameObject.transform.position = m_shotPosition.position;
            newGameObject.transform.localScale = m_shotPosition.localScale;

            if (!facingRight)  //if player faces left, the position of the shot it's okay but the rotation no (it would go right)
            {
                newGameObject.transform.rotation = Quaternion.Euler(0, 0, 180);

            }
            else
            {
                newGameObject.transform.rotation = m_shotPosition.rotation;

            }

            //Recoil
            if (touchingIce || jumpedFromIce)
            {
                if (Input.GetKeyDown(KeyCode.H) && facingRight)
                {
                    rigidBodyZozzy.AddForce(new Vector2(m_recoil, 0f), ForceMode2D.Impulse);
                }
                else if (Input.GetKeyDown(KeyCode.H) && !facingRight)
                {
                    rigidBodyZozzy.AddForce(new Vector2(-m_recoil, 0f), ForceMode2D.Impulse);
                }
            }

            readyToShootBazookaLoaded = false;
            StartCoroutine(waitToShootAgain());
            //SoundManager.Instance.PlayerShoots();

            
        }

        
    }


    IEnumerator waitToShootAgain()
    {
        reloadBazookaTimeIncrementing = 0f;
        m_reloadBar.value = 0f;
        m_reloadBarText.text = "Reload";
        yield return new WaitForSeconds(m_reloadBazookaTime);
        m_reloadBarText.text = "Ready";
        readyToShootBazookaLoaded = true;
    }


    void FixedUpdate(){
        if (!zozzyDead)
        {


            if (touchingIce || jumpedFromIce) //If ice, the player moves guided by a force
            {
                rigidBodyZozzy.AddForce(new Vector2(horizontalInputMov * m_speed * Time.fixedDeltaTime, rigidBodyZozzy.velocity.y)); //we assing to the rigid body a velocity vector, depending on the direction in input and the speed
            }
            else if (touchingConveyorBelt)
            {
                rigidBodyZozzy.AddForce(horizontalInputMov * m_speed * Vector2.right, ForceMode2D.Force);
            }
            else
            {
                //rigidBodyZozzy.AddForce(horizontalInputMov * m_speed * Vector2.right, ForceMode2D.Force);
                rigidBodyZozzy.velocity = new Vector2(horizontalInputMov * m_speed * Time.fixedDeltaTime, rigidBodyZozzy.velocity.y); //we assing to the rigid body a velocity vector, depending on the direction in input and the speed
                                                                                                                                      //rigidBodyZozzy.AddForce(horizontalInputMov * m_speed * Vector2.right, ForceMode2D.Force);

                /*TRY TO LIMIT Y VELOCITY 
                float speed = Vector3.Magnitude(rigidBodyZozzy.velocity);  // test current object speed
                float maximumYSpeed = 18f;
                if (speed > maximumYSpeed)

                {
                    float brakeSpeed = speed - maximumYSpeed;  // calculate the speed decrease

                    Vector3 normalisedVelocity = rigidBodyZozzy.velocity.normalized;
                    Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector3 value

                    rigidBodyZozzy.AddForce(-brakeVelocity);  // apply opposing brake force
                }*/


            }
            


            animator.SetFloat("speed", Mathf.Abs(rigidBodyZozzy.velocity.x)); //it sets the animator's variable "speed" (caution with the capital letters) to the value specified. This allows the animator to recognize if it's time to change animation
            //Debug.Log("VelocityZozzy: " + rigidBodyZozzy.velocity.x);


        }

    }


    //Flips the animation of the character (useful when goes left)
    void flipAnimationCharacter()
    {
        Vector3 localScaleTr = transformZozzy.localScale; //gets the parameter "scale" of "transform" (that contains position, rotation, scale)
        localScaleTr.x = localScaleTr.x * -1f; //inverts the "x" of the parameter scale (so inverting the displayed animation)
        transformZozzy.localScale = localScaleTr;
        facingRight = !(facingRight);
        constrainedCameraScript.offset.x *= -1;
    }


    //OnCollisionEnter2D is used when the object is subject to physics (rigid body 2d not kinematic)
    void OnCollisionEnter2D(Collision2D other) //Allows the player to jump only one time at a time. Only when reachs the ground, he is allowed to jump another time
    {
        
        if (jumping && ( other.gameObject.tag == "TerrainAndFloor" || other.gameObject.tag == "Platform" || other.gameObject.tag == "Pedrieu" || other.gameObject.tag == "ManInBlack1"))
        {
            
            jumping = false; //QUESTO DOVREBBE ESSERE FATTO SOLO QUANDO TOCCA IL TERRENO O PIATTAFORME, NON ANCHE I MURI
            touchingIce = false;
            touchingConveyorBelt = false;           
            jumpedFromIce = false;


            

        }
        

        if (other.gameObject.tag == "Platform") //Necessary to put this separately because in the previous if, if the player is falling into the platform, the player wouldn't attach to the platform (isJumping would be false)
        {
            jumping = false; //QUESTO DOVREBBE ESSERE FATTO SOLO QUANDO TOCCA IL TERRENO O PIATTAFORME, NON ANCHE I MURI
            touchingIce = false;
            touchingConveyorBelt = false;
            jumpedFromIce = false;

            transform.parent = other.transform;

        }
        


        if (other.gameObject.tag == "Ice")
        {
            jumping = false;
            touchingIce = true;
        }

        
        if (other.gameObject.tag == "ConveyorBelt")
        {
            jumping = false;
            touchingConveyorBelt = true;
            

        }


    }


    


    

    /*
     * if ((other.gameObject.tag == "ManInBlack1" || other.gameObject.tag == "Pedrieu") && isBeating && !detectedFirstCollisionPerAnim)
        {
            detectedFirstCollisionPerAnim = true;

            if (other.gameObject.tag == "ManInBlack1")
            {
                other.gameObject.GetComponent<ManInBlackL1>().HealthMib -= 25;
                Debug.Log("Mib HIT. Health now: " + other.gameObject.GetComponent<ManInBlackL1>().HealthMib);
            }
            else if (other.gameObject.tag == "Pedrieu")
            {
                other.gameObject.GetComponent<Pedrieu>().HealthPedrieu -= 25;
            }

        }
        */


    void OnCollisionExit2D(Collision2D other) //Allows the player to jump only one time at a time. Only when reachs the ground, he is allowed to jump another time
    {
        
        if ((other.gameObject.tag == "Ice"))
        {
            //rigidBodyZozzy.AddForce(new Vector2(rigidBodyZozzy.velocity.x, 0f), ForceMode2D.Impulse); //to "mantain a velocity in x"
            touchingIce = false;
			//Debug.Log ("Touching Ice FALSE On Collision Exit");

            if (jumpPressed)
            {
                jumpedFromIce = true;
                //Debug.Log("JumpedFromIceTRUE On Collision Exit");
            }
        }

        if (other.gameObject.tag == "ConveyorBelt")
        {
            //rigidBodyZozzy.AddForce(new Vector2(rigidBodyZozzy.velocity.x, 0f), ForceMode2D.Impulse); //to "mantain a velocity in x"
            touchingConveyorBelt = false;
        }

        if (other.gameObject.tag == "Platform")
        {
            transform.parent = null;

        }


    }


 

    void zozzyDies()
    {
        Debug.Log("Zozzy died");
        horizontalInputMov = 0;
        animator.SetInteger("health", healthZozzy);
        animator.SetBool("dead", zozzyDead);
        StartCoroutine(waitForAnimation());
        
    }

    IEnumerator waitForAnimation()
    {
        yield return new WaitForSeconds(2f);
        
        //gameObject.SetActive(false);
        GameManagerL1.Instance.RespawnPlayer();
        
        //restartScene();
        yield return null; //Put this always or the game can stuck / block.
    }


    IEnumerator waitForBeatingAnimation()
    {
        yield return new WaitForSeconds(1f);

        isBeating = false;
        animator.SetBool("isBeating", false);
        

        yield return null; //Put this always or the game can stuck / block.
    }


    public void restartScene()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Application.Quit();
    }
}
