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
    private AudioSource gunAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        gunAudioSource = this.GetComponent<AudioSource>();
        bullets = GameObject.FindGameObjectsWithTag("Bullet");
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveBullet();
    }

    private void moveBullet()
    {
        recoilTimer += Time.deltaTime;
        if (Input.GetButton(inputName) && recoilTimer > recoil)
        {
            recoilTimer = 0;
            for (int i = 0; i < bullets.Length; i++)
            {
                if (!bullets[i].activeSelf)
                {
                    bullets[i].transform.SetPositionAndRotation(bulletStartPoint.transform.position, bulletStartPoint.transform.rotation);
                    bullets[i].SetActive(true);
                    gunAudioSource.PlayOneShot(SoundManager.Instance.shootBulletClip, 1f);
                    return;
                }
            }
        }
    }
}
