using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionManager : MonoBehaviour
{
    // Start is called before the first frame update
    LifeManager lifeManager;
    
    void Start()
    {
        if (gameObject.tag == "Alien" || gameObject.tag == "Player")
        {
            lifeManager = (LifeManager)gameObject.GetComponent(typeof(LifeManager));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Bullet") 
        {
            gameObject.SetActive(false);
        }

        else if (gameObject.tag == "Portal" && other.tag == "Bullet")
        {
            gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }

        else
        {
            if ((other.tag == "Player" || other.tag == "Bullet") && gameObject.tag != "Player")
            {
                if (other.tag == "Bullet")
                {
                    other.gameObject.SetActive(false);
                }
                lifeManager.decreaseLives();
            }

            if (other.tag == "Alien")
            {
                if (gameObject.tag == "Player" && gameObject.transform.position.y > 3) return;
                lifeManager.decreaseLives();
            }
        }
    }
}
