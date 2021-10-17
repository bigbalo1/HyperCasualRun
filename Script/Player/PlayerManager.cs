﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGameStarted;
    public GameObject startingText;

    public static int numberOfCoin;
    public Text coinText;
    public Text highCoinText;
    
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = false;
        numberOfCoin = 0;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

        coinText.text = "Coins: " + numberOfCoin;
        highCoinText.text = coinText.text;
       
       

        if (SwiptManager.tap)
        {
            isGameStarted = true;
            Destroy(startingText);
        }
    }

    public int GetHighCoin()
    {
        return PlayerPrefs.GetInt("highscore");
    }
   
}
