using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokensManager : MonoBehaviour
{
    LifeManager playerLifeManager;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        playerLifeManager = (LifeManager)GameObject.FindGameObjectWithTag("Player").GetComponent(typeof(LifeManager));
        gameManager = (GameManager)GameObject.Find("GameManager").GetComponent(typeof(GameManager));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tokenFilter()
    {
        if (gameObject.transform.parent.name == "HealTokens")
        {
            playerLifeManager.increaseLives();
        }

        else if (gameObject.transform.parent.name == "MissileTokens")
        {
            gameManager.addMissiles();
        }

        else
        {
            gameManager.addTimeToBoostShot();
        }
    }
}
