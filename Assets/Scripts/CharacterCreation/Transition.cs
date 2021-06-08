using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public string sceneWhenStartingTheGame; 
    public CharacterCarousel characterCarousel;

    

    // Start is called before the first frame update
    void Start()
    {
        AudioController.Instance.PlayCharacterCreationMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(){


        //We let CharacterCarousel decide if we can start a game or not.
        if(characterCarousel.SetCharacter()){
            AudioController.Instance.PlayButtonPressedSound();
            Debug.Log("Attempting to start: "+sceneWhenStartingTheGame);
            SceneManager.LoadScene(sceneWhenStartingTheGame);
        }
        Debug.Log("Not enough to start!");
    }
}
