using UnityEngine;

public class ShootScript : MonoBehaviour
{

    enum ProjectileType
    {
        Normal,
        Missile
    }

    [SerializeField] private string inputName;
    [SerializeField] private string missileInputName;
    [SerializeField] private Transform bulletStartPoint;
    [SerializeField] private float bulletRecoil;

    private Transform[] normalBullets;
    private Transform[] missiles;
    private AudioSource gunAudioSource;
    private GameManager gameManager;

    private float recoilTimer = 0;
    private float scatterTimer = 0;
    private int missileAmmo = 0;

    // Start is called before the first frame update
    void Start()
    {
        normalBullets = GameObject.Find("NormalBullets").GetComponentsInChildren<Transform>(true);
        missiles = GameObject.Find("Missiles").GetComponentsInChildren<Transform>(true);
        gunAudioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (recoilTimer < bulletRecoil) recoilTimer += Time.deltaTime;
        if (recoilTimer > bulletRecoil) manageShooting();
    }

    public void setMissilesAmmo(int ammo)
    {
        missileAmmo = ammo;
    }

    public void setScatterTimer(float duration)
    {
        scatterTimer = duration;
    }

    private void manageShooting()
    {
        if (Input.GetButton(missileInputName) && missileAmmo > 0)
        {
            recoilTimer = 0;
            gameManager.useMissile();
            missileAmmo--;
            fireMissile();
        }
        else if (Input.GetButton(inputName))
        {
            recoilTimer = 0;
            fireBullet();
        }
    }

    private void fireMissile()
    {
        Transform bullet = getInactiveProjectile(ProjectileType.Missile);
        bullet.SetPositionAndRotation(bulletStartPoint.position, bulletStartPoint.rotation);
        bullet.gameObject.SetActive(true);
        gunAudioSource.PlayOneShot(SoundManager.Instance.shootMissileClip, 1f);
    }

    private void fireBullet()
    {
        if (scatterTimer > 0) //Shoot a scatter shot if the timer is on
        {
            Transform bullet1 = getInactiveProjectile(ProjectileType.Normal);
            bullet1.SetPositionAndRotation(bulletStartPoint.position, bulletStartPoint.rotation);
            bullet1.gameObject.SetActive(true);

            Transform bullet2 = getInactiveProjectile(ProjectileType.Normal);
            bullet2.SetPositionAndRotation(bulletStartPoint.position, bulletStartPoint.rotation * Quaternion.Euler(0, 30, 0));
            bullet2.gameObject.SetActive(true);

            Transform bullet3 = getInactiveProjectile(ProjectileType.Normal);
            bullet3.SetPositionAndRotation(bulletStartPoint.position, bulletStartPoint.rotation * Quaternion.Euler(0,-30,0));
            bullet3.gameObject.SetActive(true);

            gunAudioSource.PlayOneShot(SoundManager.Instance.shootScatterClip, 1f);
        }
        else //Shoot a normal bullet
        {
            Transform bullet = getInactiveProjectile(ProjectileType.Normal);
            bullet.SetPositionAndRotation(bulletStartPoint.position, bulletStartPoint.rotation);
            bullet.gameObject.SetActive(true);
            gunAudioSource.PlayOneShot(SoundManager.Instance.shootBulletClip, 1f);
        }
    }

    private Transform getInactiveProjectile(ProjectileType type)
    {
        Transform projectile = null;
        switch (type)
        {
            case ProjectileType.Missile:
                for (int i = 1; i < missiles.Length; i++)
                {
                    if (!missiles[i].gameObject.activeSelf)
                    {
                        projectile = missiles[i];
                    }
                }
                break;

            default:
                for (int i = 1; i < normalBullets.Length; i++)
                {
                    if (!normalBullets[i].gameObject.activeSelf)
                    {
                        projectile = normalBullets[i];
                    }
                }
                break;

        }
        return projectile;
    }
}
