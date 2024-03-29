using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseGame : MonoBehaviour
{
    public bool gameEnded = false;
    public bool gameWon = false;
    public bool gamepaused = false;

    public int happinessWinScore = 7;
    public int RepScore = 15;
    public int energyCost = 12;
    
    public NavigationScript navigationScript;

    public SoccerTeam red;
    public SoccerTeam blue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            red.pause(gamepaused || gameEnded);
            blue.pause(gamepaused || gameEnded);
            if(gameEnded){
                finishgame();
            }
    }

    public void Pause(){
        gamepaused = !gamepaused;
    }

    void finishgame(){
        // rep base change happiness increase is omdat je blij bent dat je gewonnen hebt
            // decrease als je veloren hebt
        if(gameWon){
            Game.currentGame.PlayerData.AlterHappiness(happinessWinScore);
        } else{
        
            Game.currentGame.PlayerData.AlterHappiness(-1*happinessWinScore);
            
        }
            Game.currentGame.PlayerData.AlterReputation(RepScore);
            Game.currentGame.PlayerData.AlterEnergy(energyCost);
            navigationScript.SwitchScreen();
    }
}
