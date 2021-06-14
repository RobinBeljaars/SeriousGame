using UnityEngine;
using System.Collections;
 
[System.Serializable]
public class Game {
    // Om een new game te starten gebruik je Game.currentGame = new Game();
    public static Game currentGame;
    public BaseCharacter PlayerData;
    public int scenariosCount;
    public bool scenario1Played;
    public bool scenario2Played;
    public Game(){
        currentGame = this;
        PlayerData = new BaseCharacter();
        scenariosCount = 0;
        scenario1Played = false;
        scenario2Played = false;
    }
 }