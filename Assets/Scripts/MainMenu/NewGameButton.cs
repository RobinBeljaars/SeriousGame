using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewGameButton : MonoBehaviour
{
    public Button NewGame;
   
    public NavigationScript navigationScript;

    // Start is called before the first frame update
    void Start()
    {
    AudioController.Instance.PlayCharacterCreationMusic();
		NewGame.GetComponent<Button>().onClick.AddListener(clickedNewGame);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void clickedNewGame(){
        
        //Creating New Game class
        Game.currentGame = new Game();
       // SaveLoadGame.game = Game.currentGame;

        //Creating a new empty savefile to prevent potentiel errors further along
        SaveLoadGame.Save();
        UICache.SetLastMapSceneUI("Map");
        
        navigationScript.SwitchScreen();
    }

}
