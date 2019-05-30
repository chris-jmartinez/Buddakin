using UnityEngine;
using System.Collections;

public class RocketShot : MonoBehaviour {

    public GameObject m_explosionPrefab;
    public float m_speed = 20f;
    

    public GameObject m_player;
    private Transform transfRocketShot;

   

    [SerializeField]
    private int damageRocket = 10;
    public int DamageRocket
    {
        get { return damageRocket; }
        set
        {
            damageRocket = value;
        }
    }


    // Use this for initialization
    void Start()
    {
        transfRocketShot = GetComponent<Transform>() as Transform;
        m_player = GameObject.FindGameObjectWithTag("Player");   
    }



    // Update is called once per frame
    void FixedUpdate()
    {
		if ( m_player == null ) {
			m_player = GameObject.FindGameObjectWithTag("Player");
		}

        Debug.Log("Player position:" + m_player.transform.position);
        transfRocketShot.position = transfRocketShot.position + transfRocketShot.right * m_speed * Time.fixedDeltaTime;

        /*Sometimes it happen that unity finds all the objects with tag player, also in another scenes. What a pain.*/
        /*GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            Debug.Log("Player: " + player.name);
        }*/
        
        

        if (Mathf.Abs(transfRocketShot.position.x - m_player.transform.position.x) > 17)
        {
            //Debug.Log("transf Rocket shot position: " + transfRocketShot.position.x + "/// Player transf pos: " + m_player.transform.position.x + "Player name:" + m_player);
            gameObject.SetActive(false);
        }
            
        
         
        
    }


    //void OnCollisionEnter2D(Collision2D other)
    void OnTriggerEnter2D(Collider2D other)
    {

        
        if ( !(other.gameObject.tag == "Player" || other.gameObject.tag == "ZozzyBeatingZone" || other.gameObject.tag == "PedrieuDamageZone" || other.gameObject.tag == "MibJumpCollider" || other.gameObject.tag == "DoorOrElevator" || other.gameObject.tag == "LaserHoly" || other.gameObject.tag == "Checkpoint" || other.gameObject.tag == "Coins" || other.gameObject.tag == "Tip")) { //This because the rocket was touching the player box collider and disappeared. In this manner disappears only when touches enemy or walls (all things not being the player)

            Vector3 transfPositionRocket = transform.position;
            Quaternion transfRotationRocket = transform.rotation;
            gameObject.SetActive(false);
            SoundManager.Instance.bazookaBoom();

            GameObject newExplosion = ObjectPoolingManager.Instance.GetObject(m_explosionPrefab.name);
            newExplosion.transform.position = transfPositionRocket;
            newExplosion.transform.rotation = transfRotationRocket;
            newExplosion.transform.GetChild(0).GetComponent<Animator>().SetBool("explode", true);
            
            //StartCoroutine(waitForAnimation(newExplosion));
            

            if (other.gameObject.tag == "ManInBlack1")
            {
                ManInBlackL1 manInBlackScriptOfOther;
                manInBlackScriptOfOther = other.GetComponent<ManInBlackL1>();
                manInBlackScriptOfOther.HealthMib -= damageRocket;
                
                Debug.Log("Man in black hit, life now: " + manInBlackScriptOfOther.HealthMib);
                /*
                GameManager.Instance.Scored(100); //Scored points
                */
            }


            if (other.gameObject.tag == "Pedrieu")
            {
                Pedrieu pedrieuScriptOfOther;
                pedrieuScriptOfOther = other.GetComponent<Pedrieu>();
                pedrieuScriptOfOther.HealthPedrieu -= damageRocket;
                Debug.Log("Pedrieu hit, life now: " + pedrieuScriptOfOther.HealthPedrieu);
                SoundManager.Instance.pedrieuHit();
            }





        }

        
    }

    
    IEnumerator waitForAnimation(GameObject explosInstance)
    {
        
        yield return new WaitForSeconds(1f);
        explosInstance.SetActive(false);       
        yield return null; //Put this always or the game can stuck / block.
    }

    /* CAPIRE COME POTER ANDARE AL DI LA DEL DO WHILE
    IEnumerator waitForAnimation2(Animation animation, GameObject explosionInstance)
    {

        do
        {
            Debug.Log("In do while");
            yield return null;
        } while (animation.isPlaying);

        explosionInstance.SetActive(false);
        Debug.Log("explosion instance set active false");
        yield return null;
    }
    */

}
