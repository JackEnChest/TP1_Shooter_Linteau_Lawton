using UnityEngine;

public class ExplodeOnContact : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int explosionRadius;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Bullet" && other.gameObject.tag != "Token" && other.gameObject.tag != "Player")
        {
            explode();
        }
    }

    private void explode()
    {
        foreach(Collider hit in Physics.OverlapSphere(transform.position, explosionRadius))
        {
            if(hit.gameObject.tag == "Alien" || hit.gameObject.tag == "Portal")
            {
                hit.gameObject.GetComponent<LifeManager>().decreaseLives(damage);
            }
        }
        gameObject.SetActive(false);
    }
}
