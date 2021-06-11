using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTitle : MonoBehaviour

{
    // Start is called before the first frame update

    public Text scoreText;
    void Start()
    {
        BaseCharacter playerdata = Game.currentGame.PlayerData;
        scoreText.text = playerdata.getName()+
        " is "+
        playerdata.GetAge().ToString()+
        " geworden en heeft de volgende score behaald:";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
