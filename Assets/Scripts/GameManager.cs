using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject[] portals;
    private GameObject[] aliens;
    private GameObject[] tokens;

    private float spawnTime = 0f;
    private int currentPortal = 0;
    private int livesOfPlayer = 5;
    private int missiles = 0;
    private float boostTimer = 0;

    private TextManager textLivesManager;
    private TextManager textMissilesManager;
    private TextManager textBoostManager;

    // Start is called before the first frame update
    void Start()
    {
        portals = GameObject.FindGameObjectsWithTag("Portal");
        aliens = GameObject.FindGameObjectsWithTag("Alien");
        tokens = GameObject.FindGameObjectsWithTag("Token");

        textLivesManager = (TextManager)GameObject.Find("TextLives").GetComponent(typeof(TextManager));
        textMissilesManager = (TextManager)GameObject.Find("TextMissiles").GetComponent(typeof(TextManager));
        textBoostManager = (TextManager)GameObject.Find("TextBoostTime").GetComponent(typeof(TextManager));

        for (int i = 0; i < tokens.Length; i++)
        {
            tokens[i].SetActive(false);
        }

        for(int i = 0; i < aliens.Length; i++)
        {
            aliens[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawningOfAliens();

        //Countdown for the boost shot
        if (boostTimer > 0)
        {
            boostTimer -= Time.deltaTime;
            int seconds = Mathf.FloorToInt(boostTimer);
            textBoostManager.changeBoostTimeText(seconds);
        }

        else
        {
            textBoostManager.changeBoostTimeText(0);
            boostTimer = 0;
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
                    tokens[i].transform.position = new Vector3(position.x, position.y + 3, position.z);
                    return;
                }
            }
        }
    }

    public void addMissiles()
    {
        missiles += 5;
        sendInfosToHUD();
    }

    public void addTimeToBoostShot()
    {
        boostTimer += 10;
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
                    LifeManager lifeManager = (LifeManager)aliens[i].GetComponent(typeof(LifeManager));
                    lifeManager.increaseLives();
                    break;
                }
            }
            currentPortal++;
            resetCurrentPortal();
        }
    }

    private void sendInfosToHUD()
    {
        textLivesManager.changeLivesText(livesOfPlayer);
        textMissilesManager.changeMissilesText(missiles);
    }
}
