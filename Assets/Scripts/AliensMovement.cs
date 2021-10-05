using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AliensMovement : MonoBehaviour
{
    private GameObject spaceMarine;
    [SerializeField] float speed;
    private NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        spaceMarine = GameObject.Find("SpaceMarine");
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent != null)
        {
            navMeshAgent.destination = spaceMarine.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (navMeshAgent == null)
            {
                transform.position = Vector3.MoveTowards(transform.position, spaceMarine.transform.position, speed * Time.deltaTime);
            }
        }
    }
}
