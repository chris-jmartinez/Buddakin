using UnityEngine;
using System.Collections;

public class BulletShot : MonoBehaviour {

    public float m_speed = 50f;
    public int m_bulletDamage = 10;

    private GameObject manInBlack;
    private Zozzy playerScript;
    private Transform transfBullet;

    // Use this for initialization
    void Start()
    {
        transfBullet = GetComponent<Transform>() as Transform;       
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Zozzy>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transfBullet.position = transfBullet.position + transfBullet.right * m_speed * Time.fixedDeltaTime;

        //Debug.Log("Man in black transform position" + manInBlack.transform.position.x);
        /* NOT WORKING. THIS REFERS TO A RANDOM MAN IN BLACK, I HAVE TO REFER TO THE FATHER MAN IN BLACK
        //Deactivates bullets that go beyond the lenght of the screen
        if (Mathf.Abs(transfBullet.position.x - manInBlack.transform.position.x) > 10f)
            gameObject.SetActive(false);
        if (Mathf.Abs(transfBullet.position.y - manInBlack.transform.position.y) > 10f)
            gameObject.SetActive(false);
        */
    }


    void OnTriggerEnter2D(Collider2D other)
    {

        //Debug.Log("HIT " + other.gameObject.name);
        /*
        GameObject go = ObjectPoolingManager.Instance.GetObject(m_grunt_explodes.name);
        go.transform.position = other.gameObject.transform.position;
        go.transform.rotation = other.gameObject.transform.rotation;
        SoundManager.Instance.GruntExplodes();
        other.gameObject.SetActive(false);
        GameManager.Instance.Scored(100); //Invoca metodo di game Manager e aggiorna i punti (grunt colpito). Quel metodo eventualmente passa al livello successivo
        */
        if (other.gameObject.tag != "ManInBlack1")
        {
            if (other.gameObject.tag == "MibJumpCollider" || other.gameObject.tag == "DoorOrElevator" || other.gameObject.tag == "LaserHoly" || other.gameObject.tag == "ZozzyBeatingZone" || other.gameObject.tag == "Checkpoint" || other.gameObject.tag == "Coins" || other.gameObject.tag == "Tip")
            {
                ;//Do nothing
            }
            else if (other.gameObject.tag == "Player")
            {
                playerScript.HealthZozzy -= m_bulletDamage;
                //Debug.Log("Player Hit by Mib, life now" + playerScript.HealthZozzy);
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        
        
        
    }
}
