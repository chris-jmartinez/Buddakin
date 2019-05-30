using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TntTrigger : MonoBehaviour {

    public GameObject m_player;
    public GameObject m_explosionPrefab;
    private bool isDetonated = false;
    private float triggerTntPositionX;
    private GameObject[] explosionsGameObjects;

	// Use this for initialization
	void Start () {
        triggerTntPositionX = gameObject.transform.position.x;

        //explosionsGameObjects = gameObject.transform.getC
	}
	
	// Update is called once per frame
	void Update () {
		
        if (!isDetonated &&  (m_player.transform.position.x >= triggerTntPositionX ) )
        {
            isDetonated = true;

            Time.timeScale = 0.2f;

            Debug.Log("IS DETONATED TRUE");
            foreach (Transform childExplosionTransform in transform)
            {
                StartCoroutine(waitForInstantiatingExplosion(childExplosionTransform));
              
            }

            StartCoroutine(waitForEpicSlowMotionEnd());
        }
	}


    IEnumerator waitForEpicSlowMotionEnd()
    {
        yield return new WaitForSeconds(1f);

        Debug.Log("SLOW MOTION ENDED");

        Time.timeScale = 1f;


        yield return null;
    }

    IEnumerator waitForInstantiatingExplosion(Transform childExplosionTransf)
    {
        yield return new WaitForSeconds(Random.Range(0f, 0.5f));

        GameObject newExplosionObject = ObjectPoolingManager.Instance.GetObject(m_explosionPrefab.name);
        newExplosionObject.transform.position = childExplosionTransf.position;
        //newExplosionObject.transform.localScale = childExplosionTransform.localScale;
        newExplosionObject.transform.rotation = childExplosionTransf.rotation;
        newExplosionObject.transform.GetChild(0).GetComponent<Animator>().SetBool("explode", true);

        yield return null;
    }

}
