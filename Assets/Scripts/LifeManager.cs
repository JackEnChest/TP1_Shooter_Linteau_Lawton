using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private int lives;
    private GameManager gameManager;
    private AudioSource gameObjectAudioSource = null;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameObjectAudioSource = GetComponent<AudioSource>();
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

    public void increaseLives()
    {
        lives++;
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
