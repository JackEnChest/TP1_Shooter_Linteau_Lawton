using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    [SerializeField] private Text currentText;

    private string str = ": ";
    private int lives;
    private int missiles;
    private int boostTime;

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

    public void setLivesText(int newliveValue)
    {
        lives = newliveValue;
    }

    public void setMissilesText(int newMissileValue)
    {
        missiles = newMissileValue;
    }

    public void setBoostTimeText(int newBoostTime)
    {
        boostTime = newBoostTime;
    }
}
