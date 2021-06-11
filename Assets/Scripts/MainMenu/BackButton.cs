using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public Button Back;
   
    public NavigationScript navigationScript;

    // Start is called before the first frame update
    void Start()
    {
    AudioController.Instance.PlayCharacterCreationMusic();
		Back.GetComponent<Button>().onClick.AddListener(clickedBack);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void clickedBack(){
        AudioController.Instance.PlayButtonPressedSound();
        
        //Creating New Game class, to delete old files
        Game.currentGame = new Game();
        SaveLoadGame.game = Game.currentGame;
        //Creating a new empty savefile to remove the old save
        SaveLoadGame.Save();

        navigationScript.SwitchScreen();
    }

}
