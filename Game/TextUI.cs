// Steven Hankinson 21129647

using JetBrains.Annotations;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    public TextMeshPro score;
    public TextMeshPro time;
    public TextMeshPro bombs;

    MeshRenderer scoreRender;
    MeshRenderer timeRender;
    MeshRenderer bombRender;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreRender = score.GetComponent<MeshRenderer>();
        timeRender = time.GetComponent<MeshRenderer>();
        bombRender = bombs.GetComponent<MeshRenderer>();

        scoreRender.sortingOrder = GameData.layerUI;
        timeRender.sortingOrder = GameData.layerUI;
        bombRender.sortingOrder = GameData.layerUI;

        // Stops the code from progressing past this point if the game is paused
        if (GameData.isPaused)
        {
            return;
        }

        // Converts the time left from in numbers to minutes and seconds
        int minutes = 1;
        int seconds = (int)(GameData.gameTimeMax - GameData.gameTime);

        string mins = "";
        string secs = "";

        if ((GameData.gameTimeMax - GameData.gameTime) >= 180)
        {
            minutes = 3;
            seconds = (int)(GameData.gameTimeMax - GameData.gameTime) - 180;
        }

        if ((GameData.gameTimeMax - GameData.gameTime) >= 120 && (GameData.gameTimeMax - GameData.gameTime) < 180)
        {
            minutes = 2;
            seconds = (int)(GameData.gameTimeMax - GameData.gameTime) - 120;
        }

        if ((GameData.gameTimeMax - GameData.gameTime) >= 60 && (GameData.gameTimeMax - GameData.gameTime) < 120)
        {
            minutes = 1;
            seconds = (int)(GameData.gameTimeMax - GameData.gameTime) - 60;
        }

        if ((GameData.gameTimeMax - GameData.gameTime) < 60)
        {
            minutes = 0;
            seconds = (int)(GameData.gameTimeMax - GameData.gameTime);
        }

        string scoreText = "00000";

        secs = seconds.ToString();

        if (minutes < 10)
        {
            mins = "0" + minutes;
        }

        if (minutes == 0)
        {
            mins = "00";
        }

        if (seconds < 10)
        {
            secs = "0" + seconds;
        }

        if (seconds == 0)
        {
            secs = "00";
        }

        // Checks the score to work out if it needs addition 0s inn front of it to keep it in line
        if (PlayerData.score >= 10000)
        {
            scoreText = PlayerData.score.ToString();
        }

        if (PlayerData.score >= 1000)
        {
            scoreText = "0" + PlayerData.score.ToString();
        }

        if (PlayerData.score >= 100)
        {
            scoreText = "00" + PlayerData.score.ToString();
        }
        
        // Displays the score
        score.text = scoreText;

        // Checks the bombs amount to work out if it needs addition 0s inn front of it to keep it in line
        string bombsText = "Bombs: ";

        if(PlayerData.smokeBombAmount < 10)
        {
            bombsText += "0";
        }

        bombsText += PlayerData.smokeBombAmount.ToString();
        
        // Displays the bomb amount and the time left
        bombs.text = bombsText;

        if (GameData.gameTime <= GameData.gameTimeMax)
        {
            time.text = "Time: " + mins + ":" + secs;
        }
    }
}
