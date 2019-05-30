using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {


    public GameObject m_Player;
    private Vector3 offset; //this will be the difference between the position of the camera and the position of the player (in vectors, so this will be a vector pointing from the camera to the player)

    // Use this for initialization
    void Start () {
        
        offset = transform.position - m_Player.transform.position; //Vector difference between camera and player positions
        
	}
	
	// LateUpdate called after Update, each frame. Here we are sure the player already moved (we update the movement in update or fixed update)
	void LateUpdate () {
   
        transform.position = m_Player.transform.position + offset; //from the previous position of the player, we move towards the player with the camera

	}
}
