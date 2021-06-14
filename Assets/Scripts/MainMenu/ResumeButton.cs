using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResumeButton : MonoBehaviour
{

    public Button Resume;

    public NavigationScript navigationScript;
    public Text error;


    // Start is called before the first frame update
    void Start()
    {
        Resume.GetComponent<Button>().onClick.AddListener(clickedResume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void clickedResume(){
        SaveLoadGame.Load();
        Game.currentGame = SaveLoadGame.game;
        if(Game.currentGame.GetGameStatus()!=GameState.Started){
            //Only actually started that have not been ended can be loaded
            AudioController.Instance.PlayImpossibleChoice();
            error.text="Geen opgeslagen spel gevonden";
        }
        else{
            
            navigationScript.SwitchScreen();
        }
    }
  
}
