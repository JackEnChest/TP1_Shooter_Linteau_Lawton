using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private float timeToDeath;
    private Rigidbody rigidBody;
    private float deathTimer;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        deathTimer = timeToDeath;
        rigidBody.velocity = transform.forward.normalized * speed;
    }

    private void Update()
    {
        deathTimer -= Time.deltaTime;
        if (deathTimer < 0) gameObject.SetActive(false);
    }

    void OnDisable()
    {
        rigidBody.velocity = Vector3.zero;
    }
}
