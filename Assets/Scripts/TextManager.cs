using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    private string strLives = ": ";
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
        currentText.text = strLives + lives.ToString();
    }

    public void changeLivesText(int newliveValue)
    {
        lives = newliveValue;
    }
}
