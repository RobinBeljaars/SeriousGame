using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericAction : MonoBehaviour {

    public int minAge;
    public float moneyChange;
    public float energyChange;

    public float happinessChange;

    public float reputationChange;
    public float educationChange;
    public string feedbackText;

    public Text feedBack;

    void OnMouseUp() {
        performAction();
    }


public void performAction(){
    {
        
         BaseCharacter playerData = Game.currentGame.PlayerData;
        //We should only allow the action as long as there's enough energy
        if(Game.currentGame.GetScenarioStatus()||Game.currentGame.GetEventStatus()){
            return;
        }

        if(playerData.getEnergy()+energyChange>=0){
            if(playerData.GetAge()>=minAge){
                string feedbackString = feedbackText+"\n";
                AudioController.Instance.PlayButtonPressedSound();
                
                if(moneyChange!=0){
                    playerData.AlterMoney(moneyChange);
                    feedbackString=feedbackString+"Geld: €"+moneyChange+"\n";
                }

                if(energyChange!=0){
                    playerData.AlterEnergy(energyChange);
                    feedbackString=feedbackString+"Energie: "+energyChange+"\n";
                }

                if(happinessChange!=0){
                    playerData.AlterHappiness(happinessChange);
                    feedbackString=feedbackString+"Stemming: "+happinessChange+"\n";
                 }
                
                if(reputationChange!=0){
                    playerData.AlterReputation(reputationChange);
                    feedbackString=feedbackString+"Reputatie: "+reputationChange+"\n";
                }
                
                if(educationChange!=0){
                    playerData.AlterEducation(educationChange);
                    feedbackString=feedbackString+"Scholing: "+educationChange+"\n";
                }

                feedBack.text = feedbackString;
            }
            else{
                feedBack.text="Je bent niet oud genoeg om dit te mogen...\nJe kunt deze actie uit voeren vanaf "+minAge+" jaar..";
                AudioController.Instance.PlayImpossibleChoice();
            }
            
        }

        else{
            feedBack.text="Je bent nu te moe om deze actie uit te voeren...";
            AudioController.Instance.PlayImpossibleChoice();
        }
           SaveLoadGame.Save();       
    }
}
}