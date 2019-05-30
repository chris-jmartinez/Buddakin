using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundClouds : MonoBehaviour {

    [Range(0.5f, 10f)]
    public float m_cloudsSpeed = 0.5f;
    public GameObject m_camera;
    public float m_offsetForCloudsRight;
    public float m_cloudsBackgroundPixelSize = 20.005f;

    private Transform cameraTransform;
    private Transform transformClouds;
    private Vector3 currentXyPositionCamera;
    private Vector3 newPositionClouds;
    private Vector3 offsetCloudsRespectToCamera;

    // Use this for initialization
    void Start()
    {
        transformClouds = GetComponent<Transform>() as Transform;
        offsetCloudsRespectToCamera = new Vector3(m_offsetForCloudsRight, +3.85f, 0f);
        cameraTransform = m_camera.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        currentXyPositionCamera.Set(cameraTransform.position.x, cameraTransform.position.y, 0f);
        newPositionClouds = currentXyPositionCamera + offsetCloudsRespectToCamera;

        //I shall move the skyline when the camera moves, so there will be an if. Instead, the clouds will be moving anyway
        //I queue the right skyline after the left skyline. When the size of the skyline is passed, I put all back (repeat). (2048 pixel del background, / 100 pixel per unit, attuali nel progetto).
        transformClouds.position = newPositionClouds - transformClouds.right * Mathf.Repeat(m_cloudsSpeed * Time.time, m_cloudsBackgroundPixelSize); //Spiegaz: tipicamente mi muovo con relative time, cioè deltaTime (quello del framerate), se muovo un personaggio. Ma adesso, prendo l'absolute time, e appena ho passato 2048 pixel, torno indietro. Importante mettere m_position, che tiene in memoria la posizione iniziale dello sfondo (tr.position invece varia).
    }


    

    

}
