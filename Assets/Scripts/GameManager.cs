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
    private TextManager textLivesManager;

    // Start is called before the first frame update
    void Start()
    {
        portals = GameObject.FindGameObjectsWithTag("Portal");
        aliens = GameObject.FindGameObjectsWithTag("Alien");
        tokens = GameObject.FindGameObjectsWithTag("Token");

        textLivesManager = (TextManager)GameObject.Find("TextLives").GetComponent(typeof(TextManager));

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

    public void updateLives(int newLivesValue)
    {
        livesOfPlayer = newLivesValue;
        sendLivesToHUD();
    }

    public int getLivesOfPlayer()
    {
        return livesOfPlayer;
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
                }
            }
        }
    }

    private void sendLivesToHUD()
    {
        textLivesManager.changeLivesText(livesOfPlayer);
    }
}
