using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepScript : MonoBehaviour {
    void OnMouseUp() {
        AudioController.Instance.PlayButtonPressedSound();
        Game current = Game.currentGame;
        current.PlayerData.IncrementAge(1);
        SaveLoadGame.Save();
    }
}