using UnityEngine;
using UnityEngine.AI;

public class AliensMovement : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // LateUpdate is called once per frame
    void LateUpdate()
    {
        if (gameObject.activeSelf)
        {
            if (navMeshAgent) navMeshAgent.destination = player.position;
        }
    }
}
