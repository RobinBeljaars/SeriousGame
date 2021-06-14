using UnityEngine;
using System.Collections;
 
[System.Serializable]
public class Game {
    // Om een new game te starten gebruik je Game.currentGame = new Game();
    public static Game currentGame;
    private GameState gameState;
    private bool scenarioActive;
    private bool eventActive;
    public BaseCharacter PlayerData;

    public int scenariosCount;
    public bool scenario1Played;
    public bool scenario2Played;
    public Game(){
        currentGame = this;
        PlayerData = new BaseCharacter();
        gameState = GameState.NotStarted;
        scenarioActive=false;
        eventActive=false;

        scenariosCount = 0;
        scenario1Played = false;
        scenario2Played = false;
    }
        
    public  void StartGame(){
        gameState = GameState.Started;
        Debug.Log("Gamestate: "+gameState);
    }    
    public  void EndGame(){
        gameState = GameState.Completed;
        Debug.Log("Gamestate: "+gameState);
    }

    public  GameState GetGameStatus(){
        Debug.Log(gameState);
        return gameState;
    }

    public bool GetScenarioStatus(){
        return scenarioActive;
    }

    public void SetScenarioActive(){
        scenarioActive=true;
    }
    public void SetScenarioNotActive(){
        scenarioActive=false;
    }
    public bool GetEventStatus(){
        return eventActive;
    }

    public void SetEventoActive(){
        eventActive=true;
    }
    public void SetEventNotActive(){
        eventActive=false;
    }
 }