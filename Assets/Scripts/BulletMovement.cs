using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private int speed;
    private Rigidbody rigidBody;

    // Start is called before the first frame update
    void Awake()
    {
        rigidBody = this.gameObject.GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        rigidBody.velocity = transform.forward * speed;
    }
}
