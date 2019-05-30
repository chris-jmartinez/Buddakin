using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Spine.Unity;

public class TimCruiz : Enemy1 {

	private bool isBeingConfused = false;

	[SpineAnimation(dataField: "skeletonAnimation")]
	public string shakeAnim;
	[SpineAnimation(dataField: "skeletonAnimation")]
	public string endShakeAnim;
	[SpineAnimation(dataField: "skeletonAnimation")]
	public string holdingGunAnim;
	[SpineAnimation(dataField: "skeletonAnimation")]
	public string holdingGunShooting2Anim;
	[SpineAnimation(dataField: "skeletonAnimation")]
	public string changeGunAnim;

	//Booleans to handle Spine Animations
	private bool isWalking = false;
	private bool isReadyToChangeGuns = false;
	private bool isPunching = false;

	protected override void Start() {
		base.Start ();
		//anim = GetComponent<Animator>() as Animator;
		_speed = this.speed;
		SetAnimation (100, holdingGunAnim, true);
	}

	protected override void Update() {
		base.Update ();
	}

	protected override void FixedUpdate() {
		base.FixedUpdate ();

		horizontal = (player.transform.position  - tr.position).normalized.x;

		if ( horizontal > 0.1 && facing_right )
			Flip();
		else if ( horizontal < 0.1 && !facing_right )
			Flip();	

		if ( (player as Buddakin).isUsingPowerUp() ) {
			Stop ();
		} else {
			Stop ();
			this.speed = _speed;
			move ();
		}

		//For animation purposes
		if ( isBeingConfused ) {
			StartCoroutine (ConfuseRoutine());
			//anim.SetBool ("shake", true);
		}
	}

	protected override void OnCollisionEnter2D (Collision2D other) {
		base.OnCollisionEnter2D (other);

		if ( other.gameObject.tag == "Player" ) {
			if ( (player as Buddakin).isUsingPowerUp() ) {
				return;
			}
			Stop ();

			if ( !isOnTop (other) ) {
				player.health -= 0.25f;
				SetAnimation (0, power1Anim, true);
				//StartCoroutine (PlayAnimation(power1Anim, true, 0.25f));
			} else {
				SetAnimation (0, idleAnim, true);
				//StartCoroutine (PlayAnimation(idleAnim, true, 0.25f));
			}

			//isPunching = true;
		}
	}

	void OnCollisionStay2D(Collision2D other) {
		if ( other.gameObject.tag == "Player" ) {
			if ( (player as Buddakin).isUsingPowerUp() ) {
				return;
			}
			//Stop();
			if ( !isOnTop (other) ) {
				player.health -= 0.25f;
			}
			isPunching = true;
		}
	}

	protected override void OnCollisionExit2D(Collision2D other) {
		base.OnCollisionExit2D (other);

		if ( other.gameObject.tag == "Player" ) {
			if ( (player as Buddakin).isUsingPowerUp() ) {
				return;
			}
			isPunching = false;
			//PlayAnimation (idleAnim, true);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if ( other.tag == "FireBall" ) {
			health -= 0.55f;
			other.gameObject.GetComponent<BulletShotFinalBattle> ().enabled = false;
			//StartCoroutine (AddAnimation(2, beingHittedAnim, false, 0.0f, 0.35f));
			//SetAnimation(1, beingHittedAnim, false);
			StartCoroutine (WaitForParticleSystem (other.gameObject));
		}
	}

	private void move() {
		if ( health > 0.0f ) {
			if ( isInsideTheFrustrum() ) {
				if ( !isPunching && Mathf.Abs(horizontal) > 0.9998f ) {
//				if ( Mathf.Abs (horizontal) > 0.99997f ) {
//					this.speed = _speed;
//					doWalk (horizontal < 0 ? -0.5f : 0.5f);
//				} else {
					isWalking = false;
//					//if (Mathf.Abs (horizontal) > 0.9998f) {
						if (readyToShootGunloaded) {
							shoot ();
						}
					//}
				//}
				}
			} else {
				Stop();
				this.speed = 2.0f * _speed;
				doWalk (horizontal < 0 ? -0.5f : 0.5f);
			}
		}
	}

	private void shoot() {
		this.speed = _speed;
		//anim.SetBool ("shoot", true);

		SetAnimation (0, idleAnim, true);

		if ( health > 25.0f ) {
			//SoundManager.Instance.GunFire ();
			AddAnimation (1, power2Anim, false, 0.0f);
			shot ("fx_fire_shot_a");
		} else {
			if ( !isReadyToChangeGuns ) {
				isReadyToChangeGuns = true;
			}

			if ( isReadyToChangeGuns ) {
				StartCoroutine (PlayAnimation(changeGunAnim, false, 0.5f));
			}

			//SoundManager.Instance.LaserGun ();
			AddAnimation (1, holdingGunShooting2Anim, false, 0.0f);
			shot ("fx_fire_shot_b");
		}
	}

	protected override void doWalk(float horizontal) {
		float vy = rb.velocity.y;
		rb.velocity = new Vector2 (horizontal * speed * Time.fixedDeltaTime, vy); 

		if ( Mathf.Abs(horizontal) > 0.0f && !isWalking ) {
			//anim.SetFloat ("speed", Mathf.Abs (rb.velocity.x));
			SetAnimation(0, walkAnim, true);
			isWalking = true;
		}
	}

	public override void Stop() {
		base.Stop ();

		//anim.SetBool ("shake", false);
		//anim.SetBool ("punch", false);
		//anim.SetBool ("shoot", false);
	}

	public override void Die() {
		base.Die ();
		GameObject rain = GameObject.Find ("RainPrefab2D");
		rain.SetActive (false);
		GameObject header = GameObject.Find("EnemyHeader");
		header.SetActive (false);
		player.Stop ();
	}

	IEnumerator ConfuseRoutine() {
		isBeingConfused = false;
		SetAnimation (0, shakeAnim, true);
		yield return new WaitForSeconds (0.8f * (player as Buddakin).PowerUpConfusionTime);
		skeletonAnimation.AnimationName = endShakeAnim;
	}

	IEnumerator WaitForParticleSystem(GameObject go) {
		yield return new WaitForSeconds(1f);
		go.SetActive (false);
		go.GetComponent<BulletShotFinalBattle> ().enabled = true;
	}

	/*
	* Getters & Setters
	*/

	public bool IsBeingConfused { 
		get { 
			return isBeingConfused; 
		} set { 
			isBeingConfused = value;
		} 
	}

}
