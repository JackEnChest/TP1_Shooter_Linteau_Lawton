using UnityEngine;

public class TokensManager : MonoBehaviour
{
    private LifeManager playerLifeManager;
    private GameManager gameManager;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerLifeManager = GameObject.FindGameObjectWithTag("Player").GetComponent<LifeManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            gameObject.transform.Rotate(0, 50 * Time.deltaTime, 0);
        }
    }

    public void spawn(Vector3 position)
    {
        gameObject.transform.position = new Vector3(position.x, position.y + 3, position.z);
        audioSource.PlayOneShot(SoundManager.Instance.tokenAppearClip, 1f);
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
        audioSource.PlayOneShot(SoundManager.Instance.tokenPickupClip, 1f);
        gameObject.SetActive(false); // Sound doesn't have time to play
    }
}
