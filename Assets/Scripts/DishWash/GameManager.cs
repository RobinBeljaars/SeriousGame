using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text countText;
    public GameObject winPanel;
    private int count;
    public int scoreToWin;
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, -100.0F, 0);
        count = 0;

        SetCountText();
    }
    void SetCountText()
    {
        countText.text = "Count:" + count.ToString();
        if (count >= scoreToWin)
        {
            winPanel.SetActive(true);
        }
    }
    public void IncrementScore()
    {
        count++;
        SetCountText();
    }
}
