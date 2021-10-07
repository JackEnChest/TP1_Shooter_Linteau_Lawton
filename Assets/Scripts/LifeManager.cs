using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private int lives;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void decreaseLives()
    {
        lives--;
        checkIfDead();
    }

    private void checkIfDead()
    {
        if (lives == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
