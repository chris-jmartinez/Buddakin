using UnityEngine;
using System.Collections;

public class DoorDestroyable : MonoBehaviour {

    private Animator m_animator;
    //private CameraShake cameraShakeScript;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator> () as Animator;
        //cameraShakeScript = Camera.main.GetComponent<CameraShake>();
    }


    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D other) //Overridden functions starts with capital letter, programmer new functions starts with no capital letter (I use this to make the code clean and readable)
    {
        Debug.Log("DOOR DESTROYABLE touched");
        if (other.gameObject.tag == "RocketShot"  ||  (other.gameObject.tag == "Player" && other.gameObject.GetComponent<Rigidbody2D>().velocity.x > 17.4f) ){
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            m_animator.SetBool("destroying", true);
            //cameraShakeScript.ShakeCamera(1f, 1f);
            StartCoroutine(waitForAnimation());
        }
    }





    IEnumerator waitForAnimation()
    {
        
        yield return new WaitForSeconds(0.6f);
        gameObject.SetActive(false);
       
        yield return null; //Put this always or the game can stuck / block.

    }
}
