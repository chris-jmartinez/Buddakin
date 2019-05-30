using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class Ninja : Player {

	public float invisibilityTime = 3f;
	public float invisibilityTimeElapsedRequired = 180f;

	private bool onLamp;
	private bool isInvisible;
	private string lampZone;

	bool m_attackPressed;
	bool m_invisibilityPressed;
	float invisibleTime;

	public Text scoreTextValue;

	Color color;
	Rigidbody2D storedHingeRb;

	protected override void Start () {
		base.Start ();
		color = GetComponent<SpriteRenderer> ().color;
		isInvisible = false;
		reloadTimer = invisibilityTimeElapsedRequired + 1f;
		invisibleTime = 0f;

		reloadBarSlider.maxValue = invisibilityTimeElapsedRequired;
		reloadtText.text = "Invisibility";
		reloadTimer = invisibilityTimeElapsedRequired;
	}

	protected override void Update () {
		base.Update ();
		scoreTextValue.text = GameManagerL1.Instance.CurrentScorePlayer.ToString();

		m_attackPressed = Input.GetButtonDown("Fire1");
		m_invisibilityPressed = Input.GetButtonDown("Fire2");

		if (m_attackPressed && !isAttacking) {
			isAttacking = true;
			StartCoroutine (Attack());
		}

		if (isInvisible){
			invisibleTime += Time.deltaTime;

			if (invisibleTime >= invisibilityTime) {
				isInvisible = false;
				invisibleTime = 0f;
				color.a = 1f;
				GetComponent<SpriteRenderer> ().color = color;
			}
		} else {
			reloadTimer += Time.deltaTime;
		}

		if (m_invisibilityPressed && !isInvisible && reloadTimer > invisibilityTimeElapsedRequired) {
			isInvisible = true;
			reloadTimer = 0f;
			color.a = 0.3f;
			GetComponent<SpriteRenderer> ().color = color;
		}

		if (OnLift) {
			Stop ();
		} else {
			speed = _speed;
		}
	}

	void OnTriggerStay2D (Collider2D other){
		if (other.tag == "Man in Black") {
			if (isAttacking) {
				if (other.gameObject != null && other.gameObject.GetComponent<ManInBlackL2>() != null) other.gameObject.GetComponent<ManInBlackL2>().Die();
			}
		}
	}

	IEnumerator Attack(){
		SoundManager.Instance.CutTheirThroats ();
		anim.SetBool ("attack", true);
		anim.speed = .5f;
		yield return new WaitForSeconds (.5f);
		anim.SetBool ("attack", false);
		isAttacking = false;
		yield return null;
	}

	public bool OnLamp{
		get { 
			return onLamp;
		}
		set { 
			onLamp = value;
		}
	}

	public string LampZone{
		get { 
			return lampZone;
		}
		set { 
			lampZone = value;
		}
	}

	public bool IsInvisible{
		get { 
			return isInvisible;
		}
		set { 
			isInvisible = value;
		}
	}

	public void Active(bool active){
		gameObject.SetActive (active); 
	}
}