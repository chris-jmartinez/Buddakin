using UnityEngine;
using System.Collections;

public class Enemy : Character {

	public Player player;
	public Transform bulletPosition;
	public float reloadGunTime = 0.8f;

	private bool readyToShootGunloaded = true;

	protected override void Start () {
		base.Start();
	}

	protected override void Update () {
		base.Update ();
	}

	protected override void FixedUpdate () {
		base.FixedUpdate ();
	}

	protected void shot(string name) {
		if ( player.health <= 0.0f ) {
			return;
		}

		if ( readyToShootGunloaded ) {
			GameObject newGameObject = ObjectPoolingManager.Instance.GetObject(name);
			newGameObject.transform.position = bulletPosition.position;
			newGameObject.transform.localScale = bulletPosition.localScale;

			// If man in black is facing left, when fires the bullet, also the bullet must go in that direction(left) (the rotation was not automatically set, only position)
			newGameObject.transform.rotation = !facing_right ? bulletPosition.rotation : Quaternion.Euler(0, 0, 180);

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
