using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnContact : MonoBehaviour
{

    private List<Collider> killList;

    private void OnEnable()
    {
        killList = new List<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Alien")
        {
            killList.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Alien")
        {
            killList.Remove(other);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Token")
        {
            explode();
        }
    }

    private void explode()
    {
        killList.ForEach(collider => { collider.gameObject.GetComponent<LifeManager>().decreaseLives(); });
        gameObject.SetActive(false);
    }
}
