using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private GameObject[] bullets;
    [SerializeField] private string inputName;
    [SerializeField] private GameObject bulletStartPoint;
    // Start is called before the first frame update
    void Start()
    {
        bullets = GameObject.FindGameObjectsWithTag("Bullets");
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton(inputName))
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                if (bullets[i].activeSelf)
                {
                    bullets[i].transform.SetPositionAndRotation(bulletStartPoint.transform.position, this.transform.rotation);
                    //Set velocity to set value
                    bullets[i].SetActive(true);
                }
            }
        }
    }
}
