using UnityEngine;
using System.Collections;
 
[System.Serializable]
public class Game {
    public static Game currentGame;
    // public PlayerProperties playerProperties;
    public BaseCharacter baseCharacter;
    public Game(){
        // playerProperties = new PlayerProperties();
        baseCharacter = new BaseCharacter();
    }
 }