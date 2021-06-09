using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLoaded : MonoBehaviour
{
    [Header("Dependends")]
    public Image imageAvatar;
    public Text ageText;
    public Text moodText;
    public Text EducationText;

    [Header("Parameters")]
    public Mood[] moods;
    public Education[] educations;
    void Update()
    {
        UpdateAvatar();
        UpdateAge();
        UpdateMood();
    }

    void UpdateAvatar()
    {
        imageAvatar.sprite = Game.currentGame.PlayerData.getAvatar();
    }
    void UpdateAge()
    {
        ageText.text = Game.currentGame.PlayerData.GetAge().ToString();
    }

    void UpdateMood()
    {
        float happinessValue = Game.currentGame.PlayerData.getHappiness();
        foreach (Mood item in moods)
        {
            if (happinessValue >= item.happinessMinValue && happinessValue <= item.happinessMaxValue)
            {
                moodText.text = item.mood;
                moodText.color = item.colour;
            }
        }
    }

    void UpdateEductation(){
        float education = 0;
        foreach (Education item in educations)
        {
            if (education >= item.educationMinValue && education <= item.educationMaxValue)
            {
                moodText.text = item.Grade;
                moodText.color = item.colour;
            }
        }
    }
}
