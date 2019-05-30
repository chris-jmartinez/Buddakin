using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Character : MonoBehaviour {

	public float health = 100f;
	public Slider healthBarSlider;
	public Text healthText;

	public float speed = 300.0f;
	public float jumpForce = 40.0f;
	public int maxJumps = 1;

	public GameObject finalScreen;

	protected Camera cam;
	protected Animator anim;
	protected Transform tr;
	protected Rigidbody2D rb;
	protected Collider2D[] cs;

	[SerializeField]
	protected bool facing_right = true;
	protected bool isAlive;
	protected bool isAttacking;
	protected bool isJumping = false;
	protected int count_jumps;
	protected float _speed;

	float _health;

	protected virtual void Start () {
		cam = Camera.main;
		tr = GetComponent<Transform>() as Transform;
		anim = GetComponent<Animator>() as Animator;
		rb = GetComponent<Rigidbody2D>() as Rigidbody2D;
		cs = GetComponents<Collider2D> () as Collider2D[];
		isAlive = true;
		count_jumps = 0;	
		_speed = speed;
		_health = health;
	}
	
	protected virtual void Update () {
		//Health Bars
		healthBarSlider.value = Mathf.Ceil (health);
		healthText.text = Mathf.Clamp(Mathf.CeilToInt(health), 0, 100).ToString();

		if ( health <= 0 ) {
			Die ();
		}
	}

	protected virtual void FixedUpdate () {
	}

	protected virtual void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Terrain" || other.gameObject.tag == "Ice" || other.gameObject.tag == "Platform" || other.gameObject.tag == "Box" || other.gameObject.tag == "Jump Items"){ 
			count_jumps = 0;
			isJumping = false;
		}
	}

	protected void Flip() {
		Vector3 ls = tr.localScale;
		ls.x = -1f * ls.x;
		tr.localScale = ls;
		facing_right = !facing_right;
	}

	protected void DoJump() {
		if (isAlive) {
			rb.AddForce (new Vector2 (0, jumpForce), ForceMode2D.Impulse);
			isJumping = true;
			count_jumps++;
		}
	}

	protected void DoWalk(float horizontal) {
		if (isAlive) {
			float vy = rb.velocity.y;
			rb.velocity = new Vector2 (horizontal * speed * Time.fixedDeltaTime, vy); 
			anim.speed = .9f;
			anim.SetFloat ("speed", Mathf.Abs (rb.velocity.x));
		}
	}

	public virtual void Die() {
		if (isAlive) {
			GameManagerL1.Instance.RespawnPlayer ();
			//Stop ();
			//health = 0f;
			//isAlive = false;
			//rb.isKinematic = true;
			//foreach (Collider2D c in cs) {
			//	c.isTrigger = true;
			//}
			//finalScreen.SetActive (true);
			//this.enabled = false;
			//SceneManager.LoadScene(0);
		}
	}

	public virtual void Stop() {
		Vector3 velocity = Vector3.zero;
		rb.velocity = velocity;
		this.speed = 0.0f;
	}

	protected bool isInsideTheFrustrum() {
		Vector3 screenPoint = cam.WorldToViewportPoint(transform.position);
		return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
	}

	protected bool isOnTop(Collision2D other) {
		Vector3 dir = (other.gameObject.transform.position - gameObject.transform.position).normalized;

		return dir.y >= 0.8f;

		/*Vector3 collisionNormal = collision.contacts[0].normal;

		return Mathf.Abs (collisionNormal.x) < Mathf.Abs (collisionNormal.y);*/
	}

	public void Damage(float value){
		if (isAlive) {
			health -= value;
		}
	}

	public void Heal(float value) {
		if (isAlive) {
			health += value;
		}
	}

	public void RestartHealth() {
		if (isAlive) {
			health = _health;
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

	public bool IsAttacking{
		get { 
			return isAttacking;
		}
		set { 
			isAttacking = value;
		}
	}

	public bool IsFacingRight{
		get { 
			return facing_right;
		}
		set { 
			facing_right = value;
		}
	}
}