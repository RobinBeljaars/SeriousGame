using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text countText;
    public GameObject winPanel;
    private int count;

    [Header("UI to toggle on/off on element click")]
    public float scoreMultiplier;

    private float scoreToWin;
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, -100.0F, 0);
        count = 0;
        if(Game.currentGame==null){
            scoreToWin=scoreMultiplier*12f;
        }else{
        scoreToWin = Game.currentGame.PlayerData.GetAge()*scoreMultiplier;
        }
        scoreToWin = (int)scoreToWin; //Lazy way of rounding to the nearst 'whole' number
        SetCountText();
    }
    void SetCountText()
    {
        countText.text = "Schoongemaakt:\n" + count.ToString()+" / "+scoreToWin;
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
