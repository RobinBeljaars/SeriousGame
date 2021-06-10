using UnityEngine;
using System.Collections;
 
[System.Serializable]
public class Game {
    // Om een new game te starten gebruik je Game.currentGame = new Game();
    public static Game currentGame;
    public BaseCharacter PlayerData;
    public Game(){
        currentGame = this;
        PlayerData = new BaseCharacter();
    }
 }