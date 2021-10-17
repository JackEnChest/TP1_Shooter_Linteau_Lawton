using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private int initialLives;
    [SerializeField] private float invincibilityTime;
    private float invincibilityTimer = 0;
    private int lives;
    private GameManager gameManager;
    private AudioSource gameObjectAudioSource = null;

    // Start is called before the first frame update
    void Start()
    {
        lives = initialLives;
        gameManager = FindObjectOfType<GameManager>();
        gameObjectAudioSource = transform.parent.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (invincibilityTimer > 0) invincibilityTimer -= Time.deltaTime;
    }

    private void OnEnable()
    {
        lives = initialLives;
    }

    public void decreaseLives(int amount)
    {
        if (invincibilityTimer < 0)
        {
            lives -= amount;
            checkIfDead();
            if (gameObject.tag == "Alien")
            {
                gameManager.spawningOfTokens(gameObject.transform.position);
            }
            else if (gameObject.tag == "Player")
            {
                gameManager.updateLives(lives);
            }
        }
    }

    public void increaseLives(int amount)
    {
        lives += amount;
        if (gameObject.tag == "Player")
        {
            gameManager.updateLives(lives);
        }
    }

    private void checkIfDead()
    {
        if (gameObject.tag == "Player")
        {

            if (lives <= 0)
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
            if (lives <= 0)
            {
                gameObjectAudioSource.PlayOneShot(SoundManager.Instance.alienDeathClip, 1f);
                gameObject.SetActive(false);
            }
        }
        else if (gameObject.tag == "Portal")
        {
            if (lives <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void makeInvincible()
    {
        invincibilityTimer = invincibilityTime;
    }

    public int getLives()
    {
        return lives;
    }
}
