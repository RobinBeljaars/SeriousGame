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
    public Text moneyText;
    public Text energyText;
    public Text nameText;

    [Header("Parameters")]
    public Mood[] moods;
    public Education[] educations;

    private float prevMoney = 0.0f;
    void Start() {
        Debug.Log("Starting map");
        if(Game.currentGame == null){
            Debug.Log("New Game needs to be created");
            Game.currentGame = new Game();
        }
            UpdateAvatar();
            UpdateName();
            
    }
    void Update()
    {
        UpdateAge();
        UpdateMood();
        UpdateEnergy();
        UpdateMoney();
    }

    void UpdateName() {
        nameText.text = Game.currentGame.PlayerData.getName();
    }

    void UpdateAvatar()
    {
        if (Game.currentGame.PlayerData.getAvatar() != null)
        {
            imageAvatar.sprite = Game.currentGame.PlayerData.getAvatar();
        }
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

    void UpdateMoney()
    {
        float moneyValue = Game.currentGame.PlayerData.getMoney();
        moneyText.text = "€ " + moneyValue.ToString();
        if (moneyValue >= prevMoney)
        {
            moneyText.color = Color.green;
        }
        else if (moneyValue < prevMoney)
        {
            moneyText.color = Color.red;
        }
        prevMoney = moneyValue;
    }

    void UpdateEnergy()
    {
        energyText.text = Game.currentGame.PlayerData.getEnergy().ToString();
    }

    void UpdateEductation()
    {
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
