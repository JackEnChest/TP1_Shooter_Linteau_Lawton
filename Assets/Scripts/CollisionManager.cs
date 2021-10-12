using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionManager : MonoBehaviour
{
    // Start is called before the first frame update
    LifeManager lifeManager;
    LifeManager otherLifeManager;
    
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
            if (other.tag == "Portal")
            {
                other.gameObject.SetActive(false);
            }

            if (other.tag == "Alien")
            {
                otherLifeManager = (LifeManager)other.gameObject.GetComponent(typeof(LifeManager));
                otherLifeManager.decreaseLives();
            }
            gameObject.SetActive(false);
        }

        else
        {
            if (gameObject.tag == "Player")
            {
                if (other.tag == "Alien")
                {
                    otherLifeManager = (LifeManager)other.gameObject.GetComponent(typeof(LifeManager));
                    otherLifeManager.decreaseLives();
                    if (gameObject.transform.position.y > 3) return;
                    lifeManager.decreaseLives();
                }
            }
        }
    }
}
