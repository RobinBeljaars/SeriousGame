using UnityEngine;
using System.Collections;
 
[System.Serializable]
public class Game {
    // Om een new game te starten gebruik je Game.currentGame = new Game();
    public static Game currentGame;
    private GameState gameState;
    public BaseCharacter PlayerData;
    public Game(){
        currentGame = this;
        PlayerData = new BaseCharacter();
        gameState = GameState.NotStarted;
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
 }