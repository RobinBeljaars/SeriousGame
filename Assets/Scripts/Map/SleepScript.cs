using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepScript : MonoBehaviour {

    public int maxAge;

    public int minEnergyBeforeYouCanSleep;

    public Text feedBack;

    public NavigationScript navigationScript;
    void OnMouseUp() {

        Game current = Game.currentGame;
        //We should only allow sleep when someone is acutally tired
        if(current.PlayerData.getEnergy()>minEnergyBeforeYouCanSleep){
            AudioController.Instance.PlayImpossibleChoice();
           feedBack.text=("Je bent nog niet moe...");
        }
        else
        {
            AudioController.Instance.PlayButtonPressedSound();
            if(current.PlayerData.GetAge()>=maxAge){
                //Player has gotten to old to keep playing, we should now end the game
                //Switch screen to Highscore
                current.PlayerData.IncrementAge(1);
                navigationScript.SwitchScreen();
            }
            else
            {
                current.PlayerData.IncrementAge(1);
                current.PlayerData.RestoreEnergy();
                SaveLoadGame.Save();
            }
        }

    }
}