using UnityEngine;
using UnityEngine.UI; //Richiesta per text ed elementi della UI
using System.Collections;

public class GameManagerL1 : MonoBehaviour {

    //GameManagerL1 is a static variable, so it is accessible within all the game and it's only one.
	public static GameManagerL1 Instance = null; //Capital letter of the variable because it's static and public variable

    public GameObject m_currentCheckpoint;
    private GameObject player;
    private Zozzy zozzyPlayerScript;
	private Buddakin buddakin;
	private Ninja ninja;
    private string messagePlayerDead = "playerDead";

    [Header("Prefabs for Pooling")]
	public GameObject m_shotRocketZozzy;  //"m_" before, because it's public customizable-by-designers field
    public GameObject m_explosion;
    public GameObject m_shotBulletMib;
	public GameObject m_level2_mib_shot;
	public GameObject m_player_shot1;
	public GameObject m_enemy_shot1;
	public GameObject m_enemy_shot2;

    [Header("Level parameters")]
    public int currentLevel;


	private int currentScorePlayer;
    public int CurrentScorePlayer
    {
        get { return currentScorePlayer; }
		set { currentScorePlayer = value; }
    }


    private int currentKilledEnemies = 0;
    public int CurrentKilledEnemies
    {
        get { return currentKilledEnemies; }
    }

    public int numEnemiesLevel;


    

    void Awake() {
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}

	// Use this for initialization
	void Start () {
		
    }

	public void setLevel(int level) {

        currentLevel = level;

		switch (currentLevel)
		{
		case 1:
			{
				ObjectPoolingManager.Instance.CreatePool(m_shotRocketZozzy, 3, 3);
				ObjectPoolingManager.Instance.CreatePool(m_explosion, 22, 22);
				ObjectPoolingManager.Instance.CreatePool(m_shotBulletMib, 30, 30);

				player = GameObject.FindGameObjectWithTag("Player");
				zozzyPlayerScript = player.GetComponent<Zozzy>();
				break;
			}
		case 2:
			{
				ObjectPoolingManager.Instance.CreatePool(m_level2_mib_shot, 1000, 1000);
				player = GameObject.FindGameObjectWithTag("Player");
				ninja = player.GetComponent<Ninja>();
				break;
			}
		case 3:
			{
				player = GameObject.FindGameObjectWithTag("Player");
				buddakin = player.GetComponent<Buddakin> ();
				ObjectPoolingManager.Instance.CreatePool (m_player_shot1, 1000, 1000);
				ObjectPoolingManager.Instance.CreatePool (m_enemy_shot1, 1000, 1000);
				ObjectPoolingManager.Instance.CreatePool (m_enemy_shot2, 1000, 1000);
				break;
			}
		}


	}

    //Consente di aumentare i punti dello score, quando un nemico è stato colpito (vedi shot script)

    public void Scored(int score) {
		currentScorePlayer += score;
	}

    public void enemyKilled()
    {
        currentKilledEnemies++;
    }



    public void RespawnPlayer()
    {
        Debug.Log("player RESPAWN");
        player.transform.position = m_currentCheckpoint.transform.position;
        Vector3 position = player.transform.position;
        position.z = 0f;
        player.transform.position = position;
        player.transform.rotation = m_currentCheckpoint.transform.rotation;

        //Player died: max -20 Score subtracted from the player's score (if the player has at least a score > 0) and a message to notify it.
        if (currentScorePlayer > 0)
        {
            if (currentLevel == 1 || currentLevel == 2)
            {
                TipManager.sendMessage(messagePlayerDead); //Buddakin says that the player has lost, at most, -20 score points.
            }
            
            if (currentScorePlayer >= 20)
            {
                Scored(-20);
            }
            else if (currentScorePlayer < 20 && currentScorePlayer > 0)
            {
                Scored(-currentScorePlayer);
            }
        }
        
        

        //This could be also implemented with inheritance
        switch (currentLevel)
        {
            case 1:
                {
                    zozzyPlayerScript.zozzyDead = false;
                    zozzyPlayerScript.HealthZozzy = 100;
                    player.GetComponent<Animator>().SetInteger("health", zozzyPlayerScript.HealthZozzy);
                    player.GetComponent<Animator>().SetBool("dead", false);                   
                    break;
                }
            case 2:
                {
					ninja.RestartHealth();
					player.GetComponent<Animator>().SetFloat("health", ninja.health);
                    break;
                }
            case 3:
                {
					buddakin.RestartHealth();
                    break;
                }

        }
       
    }
    
}
