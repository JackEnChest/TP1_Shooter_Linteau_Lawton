using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gameManager;

    [SerializeField] private int lives;
    private AudioSource gameObjectAudioSource = null;
    void Start()
    {
        gameManager = (GameManager)GameObject.Find("GameManager").GetComponent(typeof(GameManager));
        gameObjectAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void decreaseLives()
    {
        lives--;
        checkIfDead();
        if (gameObject.tag == "Alien")
        {
            gameManager.spawningOfTokens(gameObject.transform.position);
        }
        if (gameObject.tag == "Player")
        {
            gameManager.updateLives(lives);
        }
    }

    private void checkIfDead()
    {
        if (gameObject.tag == "Player")
        {

            if (lives == 0)
            {
                gameObjectAudioSource.PlayOneShot(SoundManager.Instance.playerDeathClip, 1f);
                gameObject.SetActive(false);
            }
            else
            {
                gameObjectAudioSource.PlayOneShot(SoundManager.Instance.playerHurtClip, 1f);
            }
        }
        else if (gameObject.tag == "Alien")
        {
            if (lives == 0)
            {
                gameObjectAudioSource.PlayOneShot(SoundManager.Instance.alienDeathClip, 1f);
                gameObject.SetActive(false);
            }
        }
    }
}
