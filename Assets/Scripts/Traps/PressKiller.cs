using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressKiller : MonoBehaviour {

    public float m_movementQuantity;
    //private Transform transformPressKiller;

    // Use this for initialization
    void Start()
    {
        //transformPressKiller = transform;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += new Vector3(0f, m_movementQuantity, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "TerrainAndFloor" || other.gameObject.tag == "ConveyorBelt" || other.gameObject.tag == "Player" || other.gameObject.tag == "DirectionChanger")
        { 
            //Debug.Log("Ceiling or player or belt touched");
            m_movementQuantity *= -1; //Inverting the movement, ceiling or conveyor belt or player or something touched
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<Zozzy>().HealthZozzy = 0;
            }
        }
    }

    
}
