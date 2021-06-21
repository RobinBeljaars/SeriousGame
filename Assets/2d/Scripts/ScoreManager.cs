using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public int scoreToWin;
    public Text scoreDisplay;
    public GameObject winPanel;
    private void Start()
    {
        scoreDisplay.text = "Score: " + score.ToString();
    }
    void SetCountText()
    {
        scoreDisplay.text = "Score: " + score.ToString();
        if (score >= scoreToWin)
        {
            winPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void IncrementScore()
    {
        score++;
        SetCountText();
    }
}
