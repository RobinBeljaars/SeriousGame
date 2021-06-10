using UnityEngine;
using System.Collections;
 
public static class UICache {
    // Om een new game te starten gebruik je Game.currentGame = new Game();
    public  static string LastMapSceneUI;



    public static void SetLastMapSceneUI(string uiTag){
        LastMapSceneUI = uiTag;
    }

    public static string GetLastMapSceneUI(){
        return LastMapSceneUI;
    }
 }