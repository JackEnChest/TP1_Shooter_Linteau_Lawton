using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    enum ProjectileType
    {
        Normal,
        Missile,
        Scatter
    }

    private GameObject[] projectiles;
    [SerializeField] private string inputName;
    [SerializeField] private GameObject bulletStartPoint;
    [SerializeField] private float bulletRecoil;
    [SerializeField] private float missileRecoil;
    [SerializeField] private float scatterRecoil;
    private float trueRecoil;
    private int amountOfBullets = 12;
    private int amountOfMissiles = 6;
    private float recoilTimer = 0;
    private ProjectileType currentProjectileType = ProjectileType.Normal;
    private AudioSource gunAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        trueRecoil = bulletRecoil;
        gunAudioSource = GetComponent<AudioSource>();
        projectiles = GameObject.FindGameObjectsWithTag("Bullet");
        for (int i = 0; i < projectiles.Length; i++)
        {
            projectiles[i].SetActive(false);
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
        if (Input.GetButton(inputName) && recoilTimer > trueRecoil)
        {
            recoilTimer = 0;
            if (currentProjectileType == ProjectileType.Normal)
            {
                GameObject bullet = getInactiveProjectile(ProjectileType.Normal);
                bullet.transform.SetPositionAndRotation(bulletStartPoint.transform.position, bulletStartPoint.transform.rotation);
                bullet.SetActive(true);
                gunAudioSource.PlayOneShot(SoundManager.Instance.shootBulletClip, 1f);
                return;
            }
            if (currentProjectileType == ProjectileType.Missile)
            {
                GameObject bullet = getInactiveProjectile(ProjectileType.Missile);
                bullet.transform.SetPositionAndRotation(bulletStartPoint.transform.position, bulletStartPoint.transform.rotation);
                bullet.SetActive(true);
                gunAudioSource.PlayOneShot(SoundManager.Instance.shootMissileClip, 1f);
                return;
            }
            if (currentProjectileType == ProjectileType.Scatter)
            {
                GameObject bullet1 = getInactiveProjectile(ProjectileType.Scatter);
                bullet1.transform.SetPositionAndRotation(bulletStartPoint.transform.position, bulletStartPoint.transform.rotation);
                bullet1.SetActive(true);
                gunAudioSource.PlayOneShot(SoundManager.Instance.shootScatterClip, 1f);

                GameObject bullet2 = getInactiveProjectile(ProjectileType.Scatter);
                bullet2.transform.SetPositionAndRotation(bulletStartPoint.transform.position, bulletStartPoint.transform.rotation * Quaternion.Euler(30, 0, 0));
                bullet2.SetActive(true);
                gunAudioSource.PlayOneShot(SoundManager.Instance.shootScatterClip, 1f);

                GameObject bullet3 = getInactiveProjectile(ProjectileType.Scatter);
                bullet3.transform.SetPositionAndRotation(bulletStartPoint.transform.position, bulletStartPoint.transform.rotation * Quaternion.Euler(0, 0, 30));
                bullet3.SetActive(true);
                gunAudioSource.PlayOneShot(SoundManager.Instance.shootScatterClip, 1f);
                return;
            }
        }
    }

    private GameObject getInactiveProjectile(ProjectileType type)
    {
        GameObject projectile = null;
        switch (type)
        {
            case ProjectileType.Missile:
                for (int i = amountOfBullets - 1; i < amountOfBullets + amountOfMissiles; i++)
                {
                    if (!projectiles[i].activeSelf)
                    {
                        projectile = projectiles[i];
                    }
                }
                break;

            default:
                for (int i = 0; i < amountOfBullets; i++)
                {
                    if (!projectiles[i].activeSelf)
                    {
                        projectile = projectiles[i];
                    }
                }
                break;

        }
        return projectile;
    }
}
