using UnityEngine;
using System.Collections;

public class ManInBlackL2 : MonoBehaviour {

	public GameObject m_shotPrefabMib;
	public float m_speed = 20f;
    public float m_jumpForce = 15f;
    public float m_reloadGunTime = 0.8f;
    public float m_timeWalkingBeforeTurnPublic = 2f;
    public bool m_jumpMibActivated = true;
    [Range(0, 3)]
    public int m_abilityToJump = 1;
    public bool m_abilityToWalk = true;
	public Transform m_shotPosition;
	public string lampZone;
	public int points = 10;
	public string key = "x";

	[SerializeField]
	private bool facingRight = true;
	private bool isAlive;
	private bool isPlayerInMyVisionRange;
	private bool jumping = false;
    private bool readyToShootGunloaded = true;
	private bool manInBlackAttacking = false;
	private int healthMib;
	private float horizontalInputMov = 0f; 
	private float timeWalkingBeforeTurn;
    private Rigidbody2D rigidBodyMib;
    private Transform transformMib;
    private Animator animator;
	private GameObject player;
    private GameObject playerShot;
	private Ninja ninja;

    void Start () {
		healthMib = 100;
		isAlive = true;
		rigidBodyMib = GetComponent<Rigidbody2D>() as Rigidbody2D;
		transformMib = GetComponent<Transform>() as Transform;
        animator = GetComponent<Animator>() as Animator;
        player = GameObject.FindGameObjectWithTag("Player") as GameObject;
        timeWalkingBeforeTurn = m_timeWalkingBeforeTurnPublic;
		ninja = player.GetComponent<Ninja> ();
    }
	
	void Update () {
		if (isAlive) {
			if (!manInBlackAttacking && m_abilityToWalk) { //If the man in black is not attacking, walks around (some walking in right direction, some walking in left direction)
				if (timeWalkingBeforeTurn <= 0) {//It's time to turn and change direction (TimeWalkingBeforeTurn is now 0, will be resetted to 5 seconds)
					flipAnimationCharacter ();
					timeWalkingBeforeTurn = m_timeWalkingBeforeTurnPublic; //TimeWalkingBeforeTurn is now 0, it's resetted to 5 seconds of walking time.
				} else {//Else, the man in black walks in the direction he is facing, because the WalkingTimeBeforeTurn has not reached 0 yet:
					horizontalInputMov = facingRight ? 1 : -1; //Debug.Log("Value of facingRight (second else):" + facingRight);
				}
				timeWalkingBeforeTurn -= 0.03f; //Decrements time only when is not attacking ALTERNATIVE: timeWalkingBeforeTurn -= Time.deltaTime;
			}

			//If player it's enough close to the ManInBlack and the ManInBlack has the gun loaded and it's not jumping...then fires to the player
			if (ninja.OnLamp && IsPlayerInMyVisionRange() && ninja.IsAlive && !ninja.IsInvisible) {
				manInBlackAttacking = true;
				horizontalInputMov = 0f; //When the man in black detects the player, he doesn't walk anymore (stops), points to the player and shoots.
				//Sets the appropiate animation (ManInBlackPointing)
				animator.SetBool ("pointing", true);

				// Shoot creation
				if (readyToShootGunloaded && !jumping && ninja.IsAlive) {
					Debug.Log (gameObject.name + " shooting");
					GameObject newGameObject = ObjectPoolingManager.Instance.GetObject (m_shotPrefabMib.name);
					newGameObject.transform.position = m_shotPosition.position;
					newGameObject.transform.localScale = m_shotPosition.localScale;

					// If man in black is facing left, when fires the bullet, also the bullet must go in that direction(left) (the rotation was not automatically set, only position)
					newGameObject.transform.rotation = facingRight ? m_shotPosition.rotation : Quaternion.Euler (0, 0, 180);
					SoundManager.Instance.GunShot();
					readyToShootGunloaded = false;
					StartCoroutine (waitToShootAgain ());
				}
			} else { 
				manInBlackAttacking = false;
				animator.SetBool ("pointing", false);        
			}
		}
    }

    void FixedUpdate() {
        rigidBodyMib.velocity = new Vector2(horizontalInputMov * m_speed * Time.fixedDeltaTime, rigidBodyMib.velocity.y);

        animator.SetFloat("speed", Mathf.Abs(rigidBodyMib.velocity.x)); //it sets the animator's variable "speed" (caution with the capital letters) to the value specified. This allows the animator to recognize if it's time to change animation

        if (jumping && m_jumpMibActivated){
            rigidBodyMib.AddForce(new Vector2(0f, m_jumpForce), ForceMode2D.Impulse);
            jumping = false;
        }

    }

    IEnumerator waitToShootAgain() {
		yield return new WaitForSeconds(m_reloadGunTime);
		readyToShootGunloaded = true;
		yield return null;
    }


    //Flips the animation of the character (useful when goes left)
    void flipAnimationCharacter() {
		Vector3 localScaleTr = transformMib.localScale; //gets the parameter "scale" of "transform" (that contains position, rotation, scale)
        localScaleTr.x = localScaleTr.x * -1f; //inverts the "x" of the parameter scale (so inverting the displayed animation)
		transformMib.localScale = localScaleTr;
        facingRight = !(facingRight);
    }


	void OnCollisionEnter2D(Collision2D other) {//Allows the player to jump only one time at a time. Only when reachs the ground, he is allowed to jump another time
		if ((other.gameObject.tag == "TerrainAndFloor" || other.gameObject.tag == "Platform" || other.gameObject.tag == "Ice") && jumping == true) {
            jumping = false;
        }

		//If enemy collides with an object that is not the player then flips around.
		if ((other.gameObject.tag == "Box" || other.gameObject.tag == "Platform" || other.gameObject.tag == "Jump Items") &&  healthMib > 0) { //Add any other object with whom collision is blocking
			flipAnimationCharacter();
		}
    }

	bool IsPlayerInMyVisionRange () {
		return (lampZone.Equals (ninja.LampZone) && ((facingRight && (ninja.transform.position.x >= transform.position.x)) || (!facingRight && (ninja.transform.position.x <= transform.position.x))));
	}

	IEnumerator KillCharacter(){
		m_speed = 0;
		healthMib = 0;
		rigidBodyMib.drag = 5f;
		GameManagerL1.Instance.Scored (points);
		yield return new WaitForSeconds (1);
		animator.SetBool ("die", false);

		Collider2D[] colliders = gameObject.GetComponents<Collider2D>();

		foreach (Collider2D collider in colliders){
			collider.enabled = false;
		}
		colliders[2].enabled = true;

		//Enabling the key protected by the MiB if existing.
		if (transform.FindChild (key) != null) {
			GameObject goKey = transform.FindChild (key).gameObject;	
			goKey.GetComponent<BoxCollider2D> ().isTrigger = true;
			goKey.GetComponent<Unlocker> ().enabled = true;
		} else {
			yield return new WaitForSeconds (10f);
			gameObject.SetActive (false);
			this.enabled = false;
		}
		yield return null;
	}

	public void CleanMiB(){
		StartCoroutine (Dissapear());
	}

	IEnumerator Dissapear() {
		yield return new WaitForSeconds (20f);
		gameObject.SetActive (false);
		this.enabled = false;
		yield return null;
	}

	public void Die(){
		if (isAlive) {
			isAlive = false;
			animator.SetBool ("die", true);	
			StartCoroutine (KillCharacter ());
		}
	}

	public bool IsAlive{
		get { 
			return isAlive;
		}
		set { 
			isAlive = value;
		}
	}

	public bool Jumping{
		get { return jumping; }
		set { jumping = value;}
	}

	public int HealthMib{
		get { return healthMib; }
		set { healthMib = value;}
	}
}