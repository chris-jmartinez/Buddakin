using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : Character {

	protected float horizontal;
	protected float vertical;
	protected bool jump;

	public Transform bulletPosition;
	public float reloadGunTime = 0.8f;

	public Slider reloadBarSlider;
	public Text reloadtText;
	protected float reloadTimer = 0.0f;

	private bool readyToShootGunloaded = true;

	private long score = 0L;
	private bool onLadder;
	private bool onLift;
	[SerializeField]
	private float climbSpeed;
	private float gravityStore;
	private float _animSpeed;

	protected override void Start () {
		base.Start ();

		reloadBarSlider.value = 0.0f;
		gravityStore = rb.gravityScale;
	}
	
	protected override void Update () {
		base.Update ();

		reloadBarSlider.value = reloadTimer;

		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");
		jump = Input.GetButtonDown("Jump");

		if ( horizontal < 0 && facing_right || horizontal > 0 && !facing_right)
			Flip();

		if (onLadder) {
			DoClimb ();
		} else {
			rb.gravityScale = gravityStore;
		}

		if ( jump && count_jumps < maxJumps ) {
			DoJump ();
		}
	}

	protected override void FixedUpdate() {
		base.FixedUpdate ();

		DoWalk (horizontal);

		//if ( jump && count_jumps < maxJumps ) {
		//	DoJump ();
		//}
	}

	protected override void OnCollisionEnter2D (Collision2D other) {
		base.OnCollisionEnter2D (other);
		if (other.gameObject.tag == "Platform") {
			transform.parent = other.transform;
		}
	}

	protected virtual void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.tag == "Platform") {
			transform.parent = null;
		}
	}

	protected virtual void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "Ladder") {
			anim.SetBool ("climb", true);
		} else if (other.tag == "Ladder Area") {
			cs [1].enabled = false;
		}
	}

	protected virtual void OnTriggerExit2D (Collider2D other){
		if (other.tag == "Ladder") {
			rb.gravityScale = gravityStore;
			anim.SetBool ("climb", false);
		} else if (other.tag == "Ladder Area") {
			cs [1].enabled = true;
		}
	}

	public virtual void DoClimb() {
		anim.speed = vertical != 0 ? Mathf.Abs(vertical) : Mathf.Abs(vertical);
		rb.gravityScale = 0f;
		rb.velocity = new Vector2 (rb.velocity.x, climbSpeed * Input.GetAxisRaw ("Vertical"));
	}	

	public void CollectKarmaCoin (long value){
		if (isAlive) {
			score += value;
		}
	}

	protected void shot(string name) {

		if ( readyToShootGunloaded ) {
			GameObject newGameObject = ObjectPoolingManager.Instance.GetObject(name);
			newGameObject.transform.position = bulletPosition.position;
			newGameObject.transform.localScale = bulletPosition.localScale;

			// If man in black is facing left, when fires the bullet, also the bullet must go in that direction(left) (the rotation was not automatically set, only position)
			newGameObject.transform.rotation = facing_right ? bulletPosition.rotation : Quaternion.Euler(0, 0, 180);

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

	IEnumerator waitToShootAgain() {
		yield return new WaitForSeconds(reloadGunTime);
		readyToShootGunloaded = true;
	}

	public bool OnLift{
		get { 
			return onLift;
		}
		set { 
			onLift = value;
		}
	}

	public bool OnLadder{
		get { 
			return onLadder;
		}
		set { 
			onLadder = value;
		}
	}

	public long KarmaPoints{
		get { 
			return score;
		}
	}
}
