using UnityEngine;

public class TokensManager : MonoBehaviour
{
    [SerializeField] private int playerLivesPerToken;
    [SerializeField] private int missilesPerToken;
    [SerializeField] private float shootBoostDurationPerToken;
    private LifeManager playerLifeManager;
    private GameManager gameManager;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        playerLifeManager = GameObject.FindGameObjectWithTag("Player").GetComponent<LifeManager>();
        gameManager = FindObjectOfType<GameManager>();
        audioSource = transform.parent.GetComponent<AudioSource>();
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
        if (transform.parent.parent.name == "HealTokens")
        {
            playerLifeManager.increaseLives(playerLivesPerToken);
        }

        else if (transform.parent.parent.name == "MissileTokens")
        {
            gameManager.addMissiles(missilesPerToken);
        }

        else
        {
            gameManager.addTimeToBoostShot(shootBoostDurationPerToken);
        }
        audioSource.PlayOneShot(SoundManager.Instance.tokenPickupClip, 1f);
        gameObject.SetActive(false); // Sound doesn't have time to play
    }
}
