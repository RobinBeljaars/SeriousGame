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
    public Text educationText;
    public Text moneyText;
    public Text energyText;
    public Text nameText;
    public Text reputationText;

    public Text feedBack;


    [Header("Parameters")]
    public Mood[] moods;
    public List<Education> educations;

    void Start() {
        Debug.Log("Starting map");
        if(Game.currentGame == null){
            Debug.Log("New Game needs to be created");
            Game.currentGame = new Game();
            SaveLoadGame.Load();
            Game.currentGame = SaveLoadGame.game;
        }
            UpdateAvatar();
            UpdateName();

            //Should mostly only show at first launch. Placeholder for now
            if(Game.currentGame.PlayerData.GetAge()==12&&Game.currentGame.PlayerData.getEnergy()==100){
                feedBack.text="Welkom! Klik op de iconen om een activiteit uit te voeren.\nElke activieit kost energie en als deze op is moet je slapen.\nSucces!";
            }
            
    }
    void Update()
    {
        UpdateAge();
        UpdateMood();
        UpdateEnergy();
        UpdateMoney();
        UpdateReputation();
        UpdateEductation();
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
        Debug.Log(Game.currentGame.PlayerData.getHappiness());
        foreach (Mood item in moods)
        {
            if (happinessValue >= item.happinessMinValue && happinessValue <= item.happinessMaxValue)
            {
                moodText.text = item.mood+" ("+happinessValue.ToString()+")";
                moodText.color = item.colour;
            }
        }
    }

    void UpdateMoney()
    {
        float moneyValue = Game.currentGame.PlayerData.getMoney();
        moneyText.text = "€ " + moneyValue.ToString();
        if (moneyValue >= 0)
        {
            moneyText.color = new Color(0,166f/255f,3f/255f,1);
        }
        else if (moneyValue <0)
        {
            moneyText.color = new Color(180f/255f,0,0,1);
        }

    }

    void UpdateEnergy()
    {
        energyText.text = Game.currentGame.PlayerData.getEnergy().ToString();
    }

    void UpdateEductation()
    {
        float educationValue = Game.currentGame.PlayerData.GetEducation();
       foreach (Education item in educations)
        {
            
            if (educationValue >= item.educationMinValue && educationValue <= item.educationMaxValue)
            {
                educationText.text = item.Grade+" ("+educationValue.ToString()+")";
                educationText.color = item.colour;
            }
        }
    }
    
    void UpdateReputation(){
        reputationText.text = Game.currentGame.PlayerData.GetReputation().ToString();
    }

}
