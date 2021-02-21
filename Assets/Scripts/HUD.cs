using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    public Player player;
    public Text scoreText;
    public Text highScoreText;
    public GameObject restartButton;

    public void OnClickRestart()
    {
        //Restart the game and unpause it
        
        Time.timeScale = 1;
        player.Restart();
    }

    public void EndGame(bool _active)
    {
        // I wanted to have the high score update before the player restarts the game so that
        // they can bask in their glory before trying again. So, I've put it all in here.
        
        if (_active == true)
        {
            int highScore = PlayerPrefs.GetInt("High Score", 0);
            int currentScore = player.GetScore();

            if (currentScore > highScore)
            {
                PlayerPrefs.SetInt("High Score", currentScore);
                PlayerPrefs.Save();
                highScore = currentScore; // Update highScore for the text update below

            }

            // int highScore = PlayerPrefs.GetInt("High Score", 0); - This didn't work for some reason?

            highScoreText.text = string.Format("High Score: {0}", highScore.ToString("D8"));
        }

        restartButton.SetActive(_active);

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Time.timeScale = 1;
        //    player.Restart();
        //}

    }

    // Start is called before the first frame update
    void Start()
    {
        // Hide the reset button.. Maybe that line could just be put here instead of calling EndGame() ?
        EndGame(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Keep updating the current score.
        scoreText.text = player.GetScore().ToString("D8");
    }
}
