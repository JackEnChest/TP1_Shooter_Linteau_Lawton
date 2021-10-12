using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    private string str = ": ";
    private int lives = 5;
    private int missiles = 0;
    private int boostTime = 0;
    [SerializeField] Text currentText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
    }

    public void changeLivesText(int newliveValue)
    {
        lives = newliveValue;
    }

    public void changeMissilesText(int newMissileValue)
    {
        missiles = newMissileValue;
    }
}
