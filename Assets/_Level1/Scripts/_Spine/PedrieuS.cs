using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;


public class PedrieuS : MonoBehaviour {

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
    private PedrieuDamageZoneS pedrieuDamageZoneScript;


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

    private SkeletonAnimation skeletonAnimation;
    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;

    #region Inspector
    // [SpineAnimation] attribute allows an Inspector dropdown of Spine animation names coming form SkeletonAnimation.
    [SpineAnimation]
    public string idleCalmAnimationName;

    [SpineAnimation]
    public string idleHypeAnimationName;

    [SpineAnimation]
    public string noticedAnimationName;

    [SpineAnimation]
    public string furyRunAnimationName;

    [SpineAnimation]
    public string attackAnimationName;
    #endregion



    // Use this for initialization
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.state;
        skeleton = skeletonAnimation.skeleton;

        targetTransfPlayer = GameObject.FindGameObjectWithTag("Player").transform as Transform;
        transfPedrieu = GetComponent<Transform>() as Transform;
        rigidBodyPedrieu = GetComponent<Rigidbody2D>() as Rigidbody2D;
        pedrieuDamageZoneScript = transform.GetChild(0).gameObject.GetComponent<PedrieuDamageZoneS>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pedrieuDead)
        {
            if (healthPedrieu <= 0)
            {
                pedrieuDead = true;
                pedrieuDies();
            }
            else
            {
                MoveEnemyWhenPlayerEntersHisZone();
            }

        }
    }


    void MoveEnemyWhenPlayerEntersHisZone()
    {

        distanceBetweenPedrieuAndPlayerInX = transfPedrieu.position.x - targetTransfPlayer.position.x;
        distanceBetweenPedrieuAndPlayerInY = transfPedrieu.position.y - targetTransfPlayer.position.y;

        if (Mathf.Abs(distanceBetweenPedrieuAndPlayerInX) < m_distanceAttackX && Mathf.Abs(distanceBetweenPedrieuAndPlayerInX) > 0.7 && Mathf.Abs(distanceBetweenPedrieuAndPlayerInY) < m_distanceAttackY && !pedrieuDamageZoneScript.PedrieuIsAttacking)
        {
            if (reactionWaitTime <= 0)
            {
                horizontalInputMov = targetTransfPlayer.position.x > transfPedrieu.position.x ? 1 : -1;
                //animator.SetFloat("speed", 1);
                //spineAnimationState.SetAnimation(0, furyRunAnimationName, true);
                skeletonAnimation.AnimationName = furyRunAnimationName;
                //skeletonAnimation.state.AddAnimation(0, furyRunAnimationName, true, 0f);

                if (horizontalInputMov == -1f && facingRight)
                {
                    flipAnimationCharacter(); //NB! flipAnimationCharacter changes the value of facingRight                
                }
                else if (horizontalInputMov == +1f && !facingRight)
                {
                    flipAnimationCharacter();
                }

            }
            else
            {
                reactionWaitTime -= Time.deltaTime;
                //animator.SetFloat("speed", 0);
                //spineAnimationState.SetAnimation(0, idleHypeAnimationName, true);
                skeletonAnimation.AnimationName = noticedAnimationName;
                //skeletonAnimation.state.AddAnimation(0, idleHypeAnimationName, true, 0f);
            }

        }
        else if (Mathf.Abs(distanceBetweenPedrieuAndPlayerInX) >= m_distanceAttackX || Mathf.Abs(distanceBetweenPedrieuAndPlayerInY) >= m_distanceAttackY)
        {
            horizontalInputMov = 0;
            //animator.SetFloat("speed", 0);
            //spineAnimationState.SetAnimation(0, idleCalmAnimationName, true);
            skeletonAnimation.AnimationName = idleCalmAnimationName;
            //skeletonAnimation.state.AddAnimation(0, idleCalmAnimationName, true, 0f);
            reactionWaitTime = 0.3f;
        }

    }


    private void pedrieuDies()
    {
        Debug.Log("Pedrieu died");
        //animator.SetBool("dead", pedrieuDead);
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

        /*if (skeleton.FlipX)
        {
            skeleton.FlipX = false;
        }else
        {
            skeleton.FlipX = true;
        }*/
        
        facingRight = !(facingRight);
    }


}
