using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject[] portals;
    private GameObject[] aliens;
    private float spawnTime = 0f;
    private int currentPortal = 0;
    // Start is called before the first frame update
    void Start()
    {
        portals = GameObject.FindGameObjectsWithTag("Portal");
        aliens = GameObject.FindGameObjectsWithTag("Alien");
    }

    // Update is called once per frame
    void Update()
    {
        if ((spawnTime += Time.deltaTime) >= 1)
        {
            for (int i = 0; i < aliens.Length; i++)
            {
                for (int j = 0; j < portals.Length; j++)
                {
                    if (portals[currentPortal].activeSelf == false)
                    {
                        currentPortal++;
                        resetCurrentPortal();
                    }
                }
                
                if (aliens[i].activeSelf == false && portals[currentPortal].activeSelf == true)
                {
                    aliens[i].SetActive(true);
                    aliens[i].transform.localPosition = portals[currentPortal].transform.localPosition;
                }
            }
            currentPortal++;
            resetCurrentPortal();
        }
    }

    private void resetCurrentPortal()
    {
        if (currentPortal == 8)
        {
            currentPortal = 0;
        }
    }
}
