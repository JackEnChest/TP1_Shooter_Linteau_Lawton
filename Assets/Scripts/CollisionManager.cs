using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    // Start is called before the first frame update
    LifeManager lifeManager;
    void Start()
    {
        if (gameObject.tag != "Bullet")
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

        else
        {

        }
    }
}
