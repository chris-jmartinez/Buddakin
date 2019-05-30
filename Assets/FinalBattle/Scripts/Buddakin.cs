using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Spine.Unity;

public class Buddakin : Player1 {

	public TimCruiz enemy;
	public ParticleSystem particles;
	[SpineAnimation(dataField: "skeletonAnimation")]
	public string powerUpAnim;

	private float powerUpConfusionTime = 5.0f;
	[SerializeField]
	private Collider2D[] colliders;
	private int currentColliderIndex = 0;

	/**
	* Icon with animation for the health bar
	**/
	[SerializeField]
	private Image icon;
	private Animator iconAnim;

	private bool isPoweringUp;
	private bool isBellyAttacking;
	private bool isShooting;
	private bool isDemo = true;

	public Text scoreTextValue;

	protected override void Start () {
		base.Start ();

		scoreTextValue.text = GameManagerL1.Instance.CurrentScorePlayer.ToString();

		reloadBarSlider.maxValue = (400.0f * PowerUpConfusionTime);
		reloadtText.text = "Superpower";

		iconAnim = icon.GetComponent<Animator> () as Animator;
		changeCollider (0);
	}

	protected override void Update() {
		base.Update ();

		isShooting = Input.GetButtonDown("Fire1");
		isBellyAttacking = Input.GetButtonDown("Fire2");
		isPoweringUp = Input.GetButtonDown ("Fire3");

		iconAnim.SetInteger ("timer", (int)reloadTimer);
		reloadTimer++;
	}

	protected override void FixedUpdate() {
		base.FixedUpdate ();
		changeCollider (0);

		if ( isPoweringUp && reloadTimer >= (400.0f * PowerUpConfusionTime) ) {
			changeCollider (0);
			particles.Play ();
			//cam.GetComponent<CameraShake> ().ShakeCamera (5.0f, 10.0f);
			enemy.health -= 5.0f;
			enemy.IsBeingConfused = true;
			reloadTimer = 0;
			StartCoroutine (PlayAnimation(powerUpAnim, false, 0.75f));
		}

		if ( isShooting ) {
			SoundManager.Instance.FireBall ();
			StartCoroutine (PlayAnimation(power1Anim, false, 0.5f));
			shot ("fx_fire_ball_b");
		}

		if ( isBellyAttacking ) {
			changeCollider (1);
			//anim.SetTrigger("bellyAttack") ;
			StartCoroutine (PlayAnimation(power2Anim, true, 0.5f));
			//skeletonAnimation.state.SetAnimation(0, "buddakin-attack-tummy", false);
		}
	}

	protected override void OnCollisionEnter2D (Collision2D other) {
		base.OnCollisionEnter2D (other);

		if ( other.gameObject.tag == "Enemies" ) {

			if ( isBellyAttacking ) {
				enemy.health -= 0.35f;
			}
		
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if ( other.tag == "Bullet1" ) {
			StartCoroutine (PlayAnimation(beingHittedAnim, true, 0.25f));
			other.gameObject.SetActive(false);
			health -= 0.35f;
		}

		if ( other.tag == "Bullet2" ) {
			StartCoroutine (PlayAnimation(beingHittedAnim, true, 0.25f));
			other.gameObject.SetActive(false);
			health -= 0.55f;
		}
	}

	void changeCollider(int newColliderIndex) {
		colliders[currentColliderIndex].enabled = false;
		currentColliderIndex = newColliderIndex;
		colliders[currentColliderIndex].enabled = true;
	}

	public bool isUsingPowerUp() {
		return particles.isPlaying;
	}

	public override void Die() {
		_speed = speed;
		if ( isDemo ) {
			Stop ();
			speed = _speed;
			skeletonAnimation.loop = false;
			skeletonAnimation.AnimationName = dieAnim;
			GameManagerL1.Instance.RespawnPlayer ();
            scoreTextValue.text = GameManagerL1.Instance.CurrentScorePlayer.ToString();
        } else {
			base.Die ();
			GameObject rain = GameObject.Find ("RainPrefab2D");
			rain.SetActive (false);
			GameObject header = GameObject.Find ("PlayerHeader");
			header.SetActive (false);
		}
		enemy.Stop ();
	}

	/**
	* Getter & Setters
	*/
	public float PowerUpConfusionTime {
		get { 
			return powerUpConfusionTime;
		} set { 
			powerUpConfusionTime = value;
		}
	}
}