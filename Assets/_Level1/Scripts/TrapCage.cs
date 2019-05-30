using UnityEngine;
using System.Collections;

public class TrapCage : MonoBehaviour {

    private Transform transfTrapCage;
    private GameObject targetTrapCageTrigger;
    private GameObject player;

    private bool playerSurvivedTheCage;
    private bool goDown;
    private bool goUp;
    private bool detectingPlayerPosition;
    private bool pedrieusLiberated;

    //[SerializeField]  to make it visible from the unity editor
    private int numberEnemiesCage = 8; // This value should be changed if we put more enemies in the cage (solution, count the childs of TrapCageEnemies with tag "ManInBlack" and "Pedrieu").
    public int NumberEnemiesCage
    {
        get { return numberEnemiesCage; }
        set
        {
            numberEnemiesCage = value;
        }
    }



    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        targetTrapCageTrigger = transform.GetChild(0).gameObject;  
        transfTrapCage = GetComponent<Transform>();

        detectingPlayerPosition = true;
        playerSurvivedTheCage = false;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (player.transform.position.x >= targetTrapCageTrigger.transform.position.x  && detectingPlayerPosition)
        {
            goDown = true;
            detectingPlayerPosition = false;
        }

        if (numberEnemiesCage == 2 && !pedrieusLiberated) //When the remaining enemies are the last 3 pedrieu, liberate their cage
        {
            StartCoroutine(pedrieusWaitSomeSeconds());
            
        }else if (numberEnemiesCage == 0 && !playerSurvivedTheCage){
            playerSurvivedTheCage = true;
            goUp = true;       
        }

        if (goDown) { 
            transfTrapCage.position = transfTrapCage.position + new Vector3(0f, -0.05f, 0f); //The cage stars going down
        }

        if (goUp)
        {
            transfTrapCage.position = transfTrapCage.position + new Vector3(0f, +0.05f, 0f);
        }
    }

    

    IEnumerator pedrieusWaitSomeSeconds()
    {
        pedrieusLiberated = true;
        yield return new WaitForSeconds(2f);
        GameObject pedrieusCage = GameObject.FindGameObjectWithTag("PedrieusCage") as GameObject;
        pedrieusCage.SetActive(false);
        yield return null; //Put this always or the game can stuck / block.
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "TerrainAndFloor") //Detected collider, the cage has to stop going down (or up)
        {
            if (goDown == true) 
            {
                goDown = false;
                GameObject mibsCage = GameObject.FindGameObjectWithTag("ManInBlacksCage") as GameObject;
                mibsCage.SetActive(false);
            }
            else if (goUp)
            {
                goUp = false;
            }
            
        }
    }
}
