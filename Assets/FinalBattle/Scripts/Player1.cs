using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player1 : Character1 {

	protected bool jump;

	public Transform bulletPosition;
	public float reloadGunTime = 0.8f;

	[Header("GUI Header")]
	public Slider reloadBarSlider;
	public Text reloadtText;
	protected float reloadTimer = 0.0f;

	private bool readyToShootGunloaded = true;

	protected override void Start () {
		base.Start ();

		reloadBarSlider.value = 0.0f;
	}
	
	protected override void Update () {
		base.Update ();

		reloadBarSlider.value = reloadTimer;

		//Movement
		horizontal = Input.GetAxis("Horizontal");
		jump = Input.GetButtonDown("Jump");

		if ( horizontal < 0 && facing_right )
			Flip();
		else if ( horizontal > 0 && !facing_right )
			Flip();

	}

	protected override void FixedUpdate() {
		base.FixedUpdate ();

		Idle ();

		doWalk (horizontal);

		if ( jump && !isJumping ) {
			doJump ();
		}
	}

	protected override void OnCollisionEnter2D (Collision2D other) {
		base.OnCollisionEnter2D (other);
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

}