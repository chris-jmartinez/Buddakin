using UnityEngine;
using UnityEngine.UI; //Richiesta per text ed elementi della UI
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance = null; //Capital letter of the variable because it's static and public variable

    [Header("Prefabs for Pooling")]
	public GameObject m_shotBulletMib;

	void Awake() {
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy (gameObject);
		}
	}

	void Start () {
        ObjectPoolingManager.Instance.CreatePool(m_shotBulletMib, 1000, 1000);
	}
}
