using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour

{
    // Start is called before the first frame update

    public Text scoreText;

    public Mood[] moods;
    public Education[] educations;
    void Start()
    {
        BaseCharacter playerdata = Game.currentGame.PlayerData;
        float educationValue = playerdata.GetEducation();
        string education="";
       foreach (Education item in educations)
        {
            if (educationValue >= item.educationMinValue && educationValue <= item.educationMaxValue)
            {
                education = item.Grade;
            }
        }  

        float happinessValue = Game.currentGame.PlayerData.getHappiness();
        string happiness="";
        foreach (Mood item in moods)
        {
            if (happinessValue >= item.happinessMinValue && happinessValue <= item.happinessMaxValue)
            {
                happiness = item.mood;
            }
        }
        float score=playerdata.getMoney()+playerdata.getHappiness()+playerdata.GetEducation()+playerdata.GetReputation();

        scoreText.text="€ "+
        playerdata.getMoney().ToString()+
        ",-\n"+
        playerdata.GetReputation().ToString()+
        "\n"+
        education+
        "\n"+
        happiness+
        "\n\n"+
        score;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
