using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Spine.Unity;

public class Character1 : MonoBehaviour {

	[Header("GUI Header")]
	public float health = 100f;
	public Slider healthBarSlider;
	public Text healthText;

	public float speed = 300.0f;
	public float jumpForce = 40.0f;
	public bool isJumping = false;

	public GameObject finalScreen;

	protected Camera cam;
	protected Animator anim;
	protected Transform tr;
	protected Rigidbody2D rb;
	protected Collider2D[] cs;

	protected float horizontal;
	protected bool facing_right = true;
	protected bool isPlayingAnimation = false;
	protected float _speed;

	private float _health = 0.0f;

	protected SkeletonAnimation skeletonAnimation;

	[Header("Animation")]
	[SpineAnimation(dataField: "skeletonAnimation")]
	public string idleAnim;
	[SpineAnimation(dataField: "skeletonAnimation")]
	public string walkAnim;
	[SpineAnimation(dataField: "skeletonAnimation")]
	public string dieAnim;
	[SpineAnimation(dataField: "skeletonAnimation")]
	public string beingHittedAnim;
	[SpineAnimation(dataField: "skeletonAnimation")]
	public string jumpAnim;
	[SpineAnimation(dataField: "skeletonAnimation")]
	public string power1Anim;
	[SpineAnimation(dataField: "skeletonAnimation")]
	public string power2Anim;

	protected virtual void Start () {
		cam = Camera.main;
		tr = GetComponent<Transform>() as Transform;
		//anim = GetComponent<Animator> () as Animator;
		rb = GetComponent<Rigidbody2D>() as Rigidbody2D;
		cs = GetComponents<Collider2D> () as Collider2D[];
		skeletonAnimation = GetComponent<SkeletonAnimation>();
		skeletonAnimation.timeScale = 1.3f;
		_health = health;
	}
	
	protected virtual void Update () {
		//Health Bars
		healthBarSlider.value = Mathf.Ceil (health);
		healthText.text = Mathf.Clamp(Mathf.CeilToInt(health), 0, 100).ToString();
		//anim.SetFloat("health", health);

		if ( health <= 0 ) {
			Die ();
		}
	}

	protected virtual void FixedUpdate () {
	}

	protected virtual void OnCollisionEnter2D (Collision2D other) {
		//Avoid Double Jumping
		if ( other.gameObject.tag == "Terrain" || other.gameObject.tag == "TerrainAndFloor" ) {
			isJumping = false;
		}

	}

	protected virtual void OnCollisionExit2D(Collision2D other) {
		isPlayingAnimation = false;	
	}

	protected void Flip() {
		Vector3 ls = tr.localScale;
		ls.x = -1f * ls.x;
		tr.localScale = ls;
		facing_right = !facing_right;
	}

	protected void doJump() {
		rb.AddForce (new Vector2 (0, jumpForce), ForceMode2D.Impulse);
		isJumping = true;
		//anim.SetTrigger ("jump");
		skeletonAnimation.loop = false;
		skeletonAnimation.AnimationName = jumpAnim;
	}

	protected virtual void doWalk(float horizontal) {
		float vy = rb.velocity.y;
		rb.velocity = new Vector2 (horizontal * speed * Time.fixedDeltaTime, vy); 
		if ( Mathf.Abs(horizontal) > 0f && !( isJumping || isPlayingAnimation ) ) {
			//anim.SetFloat ("speed", Mathf.Abs (rb.velocity.x));
			skeletonAnimation.loop = true;
			skeletonAnimation.AnimationName = walkAnim;
		}
	}

	public virtual void Die() {
		Stop ();
		skeletonAnimation.loop = false;
		skeletonAnimation.AnimationName = dieAnim;
		//skeletonAnimation.state.SetAnimation(0, "die", false);
		rb.isKinematic = true;
		foreach ( Collider2D c in cs ) {
			c.isTrigger = true;
		}
		finalScreen.SetActive (true);
		this.enabled = false;
	}

	public virtual void Stop() {
		Vector3 velocity = Vector3.zero;
		rb.velocity = velocity;
		this.speed = 0.0f;
	}

	protected virtual void Idle() {
		if ( !isInMotion() ) {
			skeletonAnimation.loop = true;
			skeletonAnimation.AnimationName = idleAnim;
		}
	}

	public void RestartHealth() {
		health = _health;
	}

	protected virtual bool isInMotion() {
		return isJumping || isPlayingAnimation || Mathf.Abs (horizontal) > 0f;
	}

	protected virtual IEnumerator PlayAnimation(string animationName, bool loop, float seconds) {
		isPlayingAnimation = true;
		skeletonAnimation.loop = loop;
		skeletonAnimation.AnimationName = animationName;
		yield return new WaitForSeconds (seconds);
		isPlayingAnimation = false;
	}

	protected virtual void PlayAnimation(string animationName, bool loop) {
		isPlayingAnimation = true;
		skeletonAnimation.loop = loop;
		skeletonAnimation.AnimationName = animationName;
	}

	protected virtual void SetAnimation(int trackIndex, string animationName, bool loop) {
		isPlayingAnimation = true;
		skeletonAnimation.state.SetAnimation (trackIndex, animationName, loop);
	}

	protected virtual void AddAnimation(int trackIndex, string animationName, bool loop, float delay) {
		isPlayingAnimation = true;
		skeletonAnimation.state.AddAnimation (trackIndex, animationName, loop, delay);
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

}