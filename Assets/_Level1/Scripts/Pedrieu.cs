using UnityEngine;
using System.Collections;

public class Pedrieu : MonoBehaviour {


    public float m_speed = 150f;
    public float m_distanceAttackX = 15f;
    public float m_distanceAttackY = 4f;

    [SerializeField]
    private int pedrieuDamage;
    public int PedrieuDamage
    {
        get { return pedrieuDamage; }
    }

    public int m_pedrieuScorePoints = 20;

    [SerializeField]
    private int healthPedrieu = 100;
    public int HealthPedrieu
    {
        get { return healthPedrieu; }
        set
        {
            healthPedrieu = value;
        }
    }

    public bool m_pedrieuTrapCage = false;

    private float horizontalInputMov = 0;
    public float HorizontalInputMov
    {
        get { return horizontalInputMov; }
        set
        {
            horizontalInputMov = value;
        }
    }

    //public AudioClip enemyAttack1;
    //public AudioClip enemyAttack2;

    private Animator animator;
    private Transform targetTransfPlayer;
    private Rigidbody2D rigidBodyPedrieu;
    private Transform transfPedrieu;
    private PedrieuDamageZone pedrieuDamageZoneScript;
    

    private float distanceBetweenPedrieuAndPlayerInX;
    private float distanceBetweenPedrieuAndPlayerInY;
    private bool facingRight = true;
    private bool pedrieuDead = false;


    private float reactionWaitTime = 0.3f;
    public float ReactionWaitTime
    {
        get { return reactionWaitTime; }
        set
        {
            reactionWaitTime = value;
        }
    }



    // Use this for initialization
    void Start () {        
        animator = GetComponent<Animator> ();
        targetTransfPlayer = GameObject.FindGameObjectWithTag("Player").transform as Transform;
        transfPedrieu = GetComponent<Transform>() as Transform;
        rigidBodyPedrieu = GetComponent<Rigidbody2D>() as Rigidbody2D;
        pedrieuDamageZoneScript = transform.GetChild(0).gameObject.GetComponent<PedrieuDamageZone>();
    }

    // Update is called once per frame
    void Update () {
        if (!pedrieuDead)
        {
            if (healthPedrieu <= 0)
            {
                pedrieuDead = true;
                pedrieuDies();
            }else
            {
                MoveEnemyWhenPlayerEntersHisZone();
            }
            
        }
    }


    void MoveEnemyWhenPlayerEntersHisZone()
    {

        distanceBetweenPedrieuAndPlayerInX = transfPedrieu.position.x - targetTransfPlayer.position.x;
        distanceBetweenPedrieuAndPlayerInY = transfPedrieu.position.y - targetTransfPlayer.position.y;

        if (Mathf.Abs(distanceBetweenPedrieuAndPlayerInX) < m_distanceAttackX && Mathf.Abs(distanceBetweenPedrieuAndPlayerInX) > 0.7 && Mathf.Abs(distanceBetweenPedrieuAndPlayerInY) < m_distanceAttackY  && !pedrieuDamageZoneScript.PedrieuIsAttacking ) {
            if (reactionWaitTime <= 0) { 
                horizontalInputMov = targetTransfPlayer.position.x > transfPedrieu.position.x ? 1 : -1;
                animator.SetFloat("speed", 1);

                if (horizontalInputMov == -1f && facingRight)
                {
                    flipAnimationCharacter(); //NB! flipAnimationCharacter changes the value of facingRight                
                }
                else if (horizontalInputMov == +1f && !facingRight)
                {
                    flipAnimationCharacter();
                }

            }else
            {
                reactionWaitTime -= Time.deltaTime;
                animator.SetFloat("speed", 0);
            }
            
        }
        else if (Mathf.Abs(distanceBetweenPedrieuAndPlayerInX) >= m_distanceAttackX  || Mathf.Abs(distanceBetweenPedrieuAndPlayerInY) >= m_distanceAttackY) {
            horizontalInputMov = 0;
            animator.SetFloat("speed", 0);
            reactionWaitTime = 0.3f;
        }

    }


    private void pedrieuDies()
    {
        Debug.Log("Pedrieu died");
        animator.SetBool("dead", pedrieuDead);
        horizontalInputMov = 0f;
        pedrieuDamageZoneScript.PedrieuIsAttacking = false;

        GameManagerL1.Instance.Scored(m_pedrieuScorePoints);

        rigidBodyPedrieu.drag = 8f;
        Collider2D[] collidersObject = gameObject.GetComponents<Collider2D>();
        foreach (Collider2D collider in collidersObject)
        {
            collider.enabled = false;
        }
        collidersObject[2].enabled = true;

        StartCoroutine(waitForAnimation());

        if (m_pedrieuTrapCage)
        {
            TrapCage trapCageScript = GameObject.FindGameObjectWithTag("TrapCage").GetComponent<TrapCage>() as TrapCage;
            trapCageScript.NumberEnemiesCage -= 1;
        }
    }

   

    IEnumerator waitForAnimation()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        yield return null; //Put this always or the game can stuck / block.
    }


    private void FixedUpdate()
    {
        rigidBodyPedrieu.velocity = new Vector2(horizontalInputMov * m_speed * Time.fixedDeltaTime, rigidBodyPedrieu.velocity.y);
        
    }


    //Flips the animation of the character (useful when goes left)
    void flipAnimationCharacter()
    {
        Vector3 localScaleTr = transfPedrieu.localScale; //gets the parameter "scale" of "transform" (that contains position, rotation, scale)
        localScaleTr.x = localScaleTr.x * -1f; //inverts the "x" of the parameter scale (so inverting the displayed animation)
        transfPedrieu.localScale = localScaleTr;
        facingRight = !(facingRight);
    }

    

    /*
	protected override void OnCantMove<T> (T component){
		Player hitPlayer = component as Player;
		animator.SetTrigger ("enemyAttack");
		//SoundManager.instance.RandomizeSfx (enemyAttack1, enemyAttack2);
		hitPlayer.LoseFood (playerDamage);
	}*/
}
