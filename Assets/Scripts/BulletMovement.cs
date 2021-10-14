using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private int speed;
    private Rigidbody rigidBody;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        rigidBody.velocity = transform.forward.normalized * speed;
    }

    void OnDisable()
    {
        rigidBody.velocity = Vector3.zero;
    }
}
