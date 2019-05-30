using UnityEngine;
using System.Collections;

public class BulletShotLevel2 : MonoBehaviour {

    public float m_speed = 50f;
	public float damage = 20f;

    GameObject manInBlack;
    Ninja playerScript;
    Transform transfBullet;

    void Start(){
		gameObject.SetActive(true);
		transfBullet = GetComponent<Transform>() as Transform;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Ninja>();
    }

    void FixedUpdate(){
        transfBullet.position = transfBullet.position + transfBullet.right * m_speed * Time.fixedDeltaTime;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player"){
			playerScript.Damage(damage);
			gameObject.SetActive(false);
		}
    }
}
