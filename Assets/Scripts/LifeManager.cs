using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gameManager;

    [SerializeField] private int lives;
    void Start()
    {
        gameManager = (GameManager)GameObject.Find("GameManager").GetComponent(typeof(GameManager));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void decreaseLives()
    {
        lives--;

        if (gameObject.tag == "Alien")
        {
            gameManager.spawningOfTokens(gameObject.transform.position);
        }

        checkIfDead();
        if (gameObject.tag == "Player")
        {
            gameManager.updateLives(lives);
        }
        
        
    }

    private void checkIfDead()
    {
        if (lives == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
