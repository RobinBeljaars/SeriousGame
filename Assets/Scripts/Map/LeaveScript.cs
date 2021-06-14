using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaveScript : MonoBehaviour {

    public int minAgeToGoOut;

    public int MinEnergyRequired;

    public Text feedBack;

    public NavigationScript navigationScript;
    void OnMouseUp() {

        Game current = Game.currentGame;
        //We should only allow sleep when someone is acutally tired
        if(current.PlayerData.GetAge()<minAgeToGoOut){
            AudioController.Instance.PlayImpossibleChoice();
           feedBack.text=("Helaas, je bent nog niet oud genoeg om uit te gaan...");
        }
        else if (current.PlayerData.getEnergy()<MinEnergyRequired)
        {
            AudioController.Instance.PlayImpossibleChoice();
            feedBack.text=("Helaas, je bent nu te moe om te stappen. \nProbeer het morgen nog eens");
        }   
        else{
            //Player meets the requirments to leave
            AudioController.Instance.PlayButtonPressedSound();
                
             //TODO   navigationScript.SwitchScreen();
            }
    }
}