using UnityEngine;
using System.Collections;

public class BulletShotFinalBattle : MonoBehaviour {

    public float m_speed = 50f;

    Transform transfBullet;

    void Start() {
        transfBullet = GetComponent<Transform>() as Transform;
    }

    void FixedUpdate() {
        transfBullet.position = transfBullet.position + transfBullet.right * m_speed * Time.fixedDeltaTime;
	}

}
