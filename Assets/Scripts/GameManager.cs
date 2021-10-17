using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject[] portals;
    private GameObject[] aliens;
    private GameObject[] tokens;
    private LifeManager playerLifeManager;
    private ShootScript playerShootScript;

    private float spawnTime = 0f;
    private int currentPortal = 0;
    private int livesOfPlayer;
    private int missiles = 0;
    private float boostTimer = 0;
    private bool gameIsOver = false;
    private float musicTimer = 0;
    private bool fullStop = false;

    private TextManager textLivesManager;
    private TextManager textMissilesManager;
    private TextManager textBoostManager;
    private GameObject textGameOver;
    private GameObject textVictory;

    private AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        portals = GameObject.FindGameObjectsWithTag("Portal");

        aliens = GameObject.FindGameObjectsWithTag("Alien");
        for (int i = 0; i < aliens.Length; i++)
        {
            aliens[i].SetActive(false);
        }

        tokens = GameObject.FindGameObjectsWithTag("Token");
        for (int i = 0; i < tokens.Length; i++)
        {
            tokens[i].SetActive(false);
        }

        playerLifeManager = GameObject.FindGameObjectWithTag("Player").GetComponent<LifeManager>();
        playerShootScript = GameObject.FindGameObjectWithTag("Player").GetComponent<ShootScript>();

        textLivesManager = GameObject.Find("TextLives").GetComponent<TextManager>();
        textMissilesManager = GameObject.Find("TextMissiles").GetComponent<TextManager>();
        textBoostManager = GameObject.Find("TextBoostTime").GetComponent<TextManager>();

        textGameOver = GameObject.Find("TextGameOver");
        textVictory = GameObject.Find("TextVictory");
        textGameOver.SetActive(false);
        textVictory.SetActive(false);

        musicSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();


        livesOfPlayer = playerLifeManager.getLives();
        sendInfosToHUD();
    }

    // Update is called once per frame
    void Update()
    {
        spawningOfAliens();
        boostManager();
        if (!fullStop)
        {
            checkIfGameIsOver();
            checkIfPlayerWon();
        }
    }

    public void updateLives(int newLivesValue)
    {
        livesOfPlayer = newLivesValue;
        sendInfosToHUD();
    }

    public void spawningOfTokens(Vector3 position)
    {
        for (int i = 0; i < tokens.Length; i++)
        {
            if (!tokens[i].activeSelf)
            {
                int result = Random.Range(1, 100);
                if (result == 1)
                {
                    tokens[i].SetActive(true);
                    tokens[i].GetComponent<TokensManager>().spawn(position);
                    break;
                }
            }
        }
    }

    public void useMissile()
    {
        missiles--;
        sendInfosToHUD();
    }

    public void addMissiles(int amount)
    {
        missiles += amount;
        playerShootScript.setMissilesAmmo(missiles);
        sendInfosToHUD();
    }

    public void addTimeToBoostShot(float duration)
    {
        boostTimer += duration;
    }

    private void resetCurrentPortal()
    {
        if (currentPortal == portals.Length)
        {
            currentPortal = 0;
        }
    }

    private void spawningOfAliens()
    {
        if ((spawnTime += Time.deltaTime) >= 1)
        {
            spawnTime = 0;
            for (int i = 0; i < aliens.Length; i++)
            {
                for (int j = 0; j < portals.Length; j++)
                {
                    if (!portals[currentPortal].activeSelf)
                    {
                        currentPortal++;
                        resetCurrentPortal();
                    }
                }

                if (!aliens[i].activeSelf && portals[currentPortal].activeSelf)
                {
                    aliens[i].SetActive(true);
                    aliens[i].transform.position = portals[currentPortal].transform.position;
                    break;
                }
            }
            currentPortal++;
            resetCurrentPortal();
        }
    }

    private void sendInfosToHUD()
    {
        textLivesManager.setLivesText(livesOfPlayer);
        textMissilesManager.setMissilesText(missiles);
    }

    private void checkIfGameIsOver()
    {
        if (livesOfPlayer == 0)
        {
            fullStop = true;
            textGameOver.SetActive(true);
        }
    }

    private void checkIfPlayerWon()
    {
        int aliveEnemies = 0;
        for (int i = 0; i < aliens.Length; i++)
        {
            if (aliens[i].activeSelf) aliveEnemies++;
        }

        for (int i = 0; i < portals.Length; i++)
        {
            if (portals[i].activeSelf) aliveEnemies++;
        }
        if (aliveEnemies == 0 && !gameIsOver)
        {
            gameIsOver = true;
            musicTimer = 3;
            musicSource.loop = false;
            StartCoroutine(FadeAudioSource.StartFade(musicSource, 3, 0));
            textVictory.SetActive(true);
        }
        if (musicTimer > 0 && gameIsOver) musicTimer -= Time.deltaTime;
        if(musicTimer < 0 && gameIsOver)
        {
            StopCoroutine("StartFade");
            musicSource.clip = SoundManager.Instance.victoryClip;
            musicSource.Play();
            StartCoroutine(FadeAudioSource.StartFade(musicSource, 3, 0.25f));
            fullStop = true;
        }
    }

    private void boostManager()
    {
        //Countdown for the boost shot
        if (boostTimer > 0)
        {
            boostTimer -= Time.deltaTime;
            playerShootScript.setScatterTimer(boostTimer);
            int seconds = Mathf.FloorToInt(boostTimer);
            textBoostManager.setBoostTimeText(seconds);
        }
        else
        {
            textBoostManager.setBoostTimeText(0);
            boostTimer = 0;
        }
    }
}
