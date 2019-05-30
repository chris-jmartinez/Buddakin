using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarmaCoinNew : MonoBehaviour {

    public int m_karmaCoinPoints;
    public int m_karmaCoinHealth;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SoundManager.Instance.CollectCoin();
            GameManagerL1.Instance.Scored(m_karmaCoinPoints);
            Debug.Log("Score now: " + GameManagerL1.Instance.CurrentScorePlayer);

            switch (GameManagerL1.Instance.currentLevel)
            {
                case 1:
                    {
                        other.gameObject.GetComponent<Zozzy>().HealthZozzy += m_karmaCoinHealth;
                        if (other.gameObject.GetComponent<Zozzy>().HealthZozzy > 100)
                        {
                            other.gameObject.GetComponent<Zozzy>().HealthZozzy = 100;
                        }
                        gameObject.SetActive(false);
                        break;
                    }
            }

        }
    }
}
