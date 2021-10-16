using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private LifeManager lifeManager;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Player")
        {
            lifeManager = GetComponent<LifeManager>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Since the Script is only on Bullets and Players, we only need to mind those cases.
        if (gameObject.tag == "Bullet") 
        {
            if (other.tag == "Portal")
            {
                other.gameObject.GetComponent<LifeManager>().decreaseLives(1);
            }
            else if (other.tag == "Alien")
            {
                other.gameObject.GetComponent<LifeManager>().decreaseLives(1);
            }
            if(other.gameObject.tag != "Bullet" && other.gameObject.tag != "Missile" && other.gameObject.tag != "Token") gameObject.SetActive(false);
        }
        else if (gameObject.tag == "Player")
        {
            if (other.tag == "Alien")
            {
                other.gameObject.GetComponent<LifeManager>().decreaseLives(1);
                if (gameObject.transform.position.y < 3) lifeManager.decreaseLives(1);
            }
            else if (other.tag == "Token")
            {
                other.gameObject.GetComponent<TokensManager>().tokenFilter();
            }
        }
    }
}
