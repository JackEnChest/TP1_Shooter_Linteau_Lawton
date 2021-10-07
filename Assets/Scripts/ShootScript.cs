using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    private GameObject[] bullets;
    [SerializeField] private string inputName;
    [SerializeField] private GameObject bulletStartPoint;
    [SerializeField] private float recoil;
    private float recoilTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        bullets = GameObject.FindGameObjectsWithTag("Bullet");
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        recoilTimer += Time.deltaTime;
        if (Input.GetButton(inputName) && recoilTimer > recoil)
        {
            recoilTimer = 0;
            bool bulletFound = false;
            for (int i = 0; i < bullets.Length; i++)
            {
                if (!bullets[i].activeSelf && !bulletFound)
                {
                    bullets[i].transform.SetPositionAndRotation(bulletStartPoint.transform.position, bulletStartPoint.transform.rotation);
                    bullets[i].SetActive(true);
                    bulletFound = true;
                }
            }
        }
    }
}
