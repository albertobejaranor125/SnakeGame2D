using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOver;
    public static GameManagerScript instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    private int scoreSnake;
    private int highScoreSnake;
    // Start is called before the first frame update
    private void Awake()
    {
        #region Create Instance and Load Max Score possible
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        LoadScoreSnakeJSON();
        scoreSnake = 0;
        scoreText.text = "Score: " + scoreSnake.ToString();
        #endregion
    }
    private void LoadScoreSnakeJSON()
    {
        highScoreSnake = SaveManagerScript.LoadHighScoreSnakeJSON();
        highScoreText.text = "High Score: " + highScoreSnake.ToString();
    }
    public void IncreaseScore()
    {
        scoreSnake++;
        scoreText.text = "Score: " + scoreSnake.ToString();
    }
    public void GameOver()
    {
        gameOver.SetActive(true);
        if (CheckHighScore())
        {
            highScoreText.text = "High Score: " + scoreSnake.ToString();
        }
    }
    private bool CheckHighScore()
    {
        #region Checks Score is more higher than High Score
        if (scoreSnake > highScoreSnake)
        {
            highScoreSnake = scoreSnake;
            SaveHighScoreSnakeJSON();
            return true;
        }
        return false;
        #endregion
    }
    private void SaveHighScoreSnakeJSON()
    {
        SaveManagerScript.SaveHighScoreSnakeJSON(highScoreSnake);
    }
    public void ButtonExit()
    {
        SceneManager.LoadScene(0);
    }
}
