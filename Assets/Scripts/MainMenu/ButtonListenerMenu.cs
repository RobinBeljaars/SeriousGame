using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonListenerMenu : MonoBehaviour
{
    public Button NewGame;
    public Button ResumeGame;
    public Button ShutDown;


    // Start is called before the first frame update
    void Start()
    {
		NewGame.GetComponent<Button>().onClick.AddListener(clickedNewGame);
        ResumeGame.GetComponent<Button>().onClick.AddListener(clickedResume);
        ShutDown.GetComponent<Button>().onClick.AddListener(clickedExit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void clickedNewGame(){
        AudioController.Instance.PlayButtonPressedSound();
        
        
    }
    void clickedResume(){
        AudioController.Instance.PlayButtonPressedSound();
        
    }
    void clickedExit(){
        AudioController.Instance.PlayButtonPressedSound();
      Application.Quit();
    }
}
