using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliensMovement : MonoBehaviour
{
    private GameObject spaceMarine;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        spaceMarine = GameObject.Find("SpaceMarine");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {

        }
    }
}
