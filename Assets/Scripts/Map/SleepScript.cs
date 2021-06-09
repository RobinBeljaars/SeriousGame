using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepScript : MonoBehaviour {
    void OnMouseUp() {
        Game current = Game.currentGame;
        current.PlayerData.IncrementAge(1);
        SaveLoadGame.Save();
    }
}