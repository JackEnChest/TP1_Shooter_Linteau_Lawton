using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    [SerializeField] private Text currentText;

    private string str = ": ";
    private int lives = 5;
    private int missiles = 0;
    private int boostTime = 0;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "TextLives")
        {
            currentText.text = str + lives.ToString();
        }
        else if (gameObject.name == "TextMissiles")
        {
            currentText.text = str + missiles.ToString();
        }
        else
        {
            currentText.text = str + boostTime.ToString();
        }
    }

    public void changeLivesText(int newliveValue)
    {
        lives = newliveValue;
    }

    public void changeMissilesText(int newMissileValue)
    {
        missiles = newMissileValue;
    }

    public void changeBoostTimeText(int newBoostTime)
    {
        boostTime = newBoostTime;
    }
}
