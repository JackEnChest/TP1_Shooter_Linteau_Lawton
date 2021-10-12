using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokensManager : MonoBehaviour
{

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
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
            gameManager.updateLives(gameManager.getLivesOfPlayer() + 1);
        }

        else if (gameObject.transform.parent.name == "MissileTokens")
        {

        }

        else
        {

        }
    }
}
