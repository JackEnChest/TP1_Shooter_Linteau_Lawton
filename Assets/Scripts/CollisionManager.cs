using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    // Start is called before the first frame update
    LifeManager lifeManager;
    LifeManager otherLifeManager;
    TokensManager tokensManager;
    
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
            else if (other.tag == "Alien")
            {
                otherLifeManager = (LifeManager)other.gameObject.GetComponent(typeof(LifeManager));
                otherLifeManager.decreaseLives();
            }
            gameObject.SetActive(false);
        }

        
        if (gameObject.tag == "Player")
        {
            if (other.tag == "Alien")
            {
                otherLifeManager = (LifeManager)other.gameObject.GetComponent(typeof(LifeManager));
                otherLifeManager.decreaseLives();
                if (gameObject.transform.position.y > 3) return;
                lifeManager.decreaseLives();
            }
            else if (other.tag == "Token")
            {
                tokensManager = (TokensManager)other.gameObject.GetComponent(typeof(TokensManager));
                tokensManager.tokenFilter();
                other.gameObject.SetActive(false);
            }
        }
        
    }
}
