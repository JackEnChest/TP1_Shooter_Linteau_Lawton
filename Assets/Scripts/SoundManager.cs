using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;

    [SerializeField] private AudioClip shootBullet;
    [SerializeField] private AudioClip shootMissile;
    [SerializeField] private AudioClip ShootScatter;
    [SerializeField] private AudioClip playerDeath;
    [SerializeField] private AudioClip playerHurt;
    [SerializeField] private AudioClip victory;
    [SerializeField] private AudioClip tokenPickup;
    [SerializeField] private AudioClip alienDeath;

    public static SoundManager Instance { get { return instance; } }

    public AudioClip shootBulletClip { get { return shootBullet; } }
    public AudioClip shootMissileClip { get { return shootMissile; } }
    public AudioClip shootScatterClip { get { return ShootScatter; } }
    public AudioClip playerDeathClip { get { return playerDeath; } }
    public AudioClip playerHurtClip { get { return playerHurt; } }
    public AudioClip victoryClip { get { return victory; } }
    public AudioClip tokenPickupClip { get { return tokenPickup; } }
    public AudioClip alienDeathClip { get { return alienDeath; } }
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
}
