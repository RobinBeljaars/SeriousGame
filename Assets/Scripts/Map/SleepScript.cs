using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepScript : MonoBehaviour {

    public int maxAge;

    public int minEnergyBeforeYouCanSleep;

    public Text feedBack;

    public NavigationScript navigationScript;

    public float interestPercentrage;
    void OnMouseUp() {

        Game current = Game.currentGame;

        if(current.GetScenarioStatus()){
            return;
        }
        //We should only allow sleep when someone is acutally tired
        if(current.PlayerData.getEnergy()>minEnergyBeforeYouCanSleep){
            AudioController.Instance.PlayImpossibleChoice();
           feedBack.text=("Je bent nog niet moe...\nJe kunt slapen zodra je energieniveau onder de "+minEnergyBeforeYouCanSleep+" is");
        }
        else
        {
            //If we end the day with negative money we 
            if(Game.currentGame.PlayerData.getMoney()<0){

                float calculatedInterest = (Game.currentGame.PlayerData.getMoney()*-1)*(interestPercentrage/100);
                Game.currentGame.PlayerData.AlterMoney(calculatedInterest*-1);
                feedBack.text=("Je stond in het rood... \nJe ouders hebben je geld geleend maar verwachten wel 10% rente van je...\nZorg dat je morgen weer in het groen staat!");
            }


            if(current.PlayerData.GetAge()>=maxAge){
                //Player has gotten to old to keep playing, we should now end the game
                //Switch screen to Highscore
                Game.currentGame.EndGame();
                current.PlayerData.IncrementAge(1);
                navigationScript.SwitchScreen();
            }
            else
            {
                AudioController.Instance.PlayButtonPressedSound();
                current.PlayerData.IncrementAge(1);
                current.PlayerData.RestoreEnergy();
                SaveLoadGame.Save();
            }
        }

    }
}