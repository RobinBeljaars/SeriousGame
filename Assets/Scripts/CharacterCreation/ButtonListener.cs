using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonListener : MonoBehaviour
{
    public Button LeftButton;
    public Button RightButton;

    public Button StartButton;

    public NavigationScript navigationScript;

    public CharacterCarousel characterCarousel;
    // Start is called before the first frame update
    void Start()
    {
		LeftButton.GetComponent<Button>().onClick.AddListener(ClickedLeft);
        RightButton.GetComponent<Button>().onClick.AddListener(ClickedRight);
        StartButton.GetComponent<Button>().onClick.AddListener(ClickedStart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ClickedLeft(){
        AudioController.Instance.PlayButtonPressedSound();
        characterCarousel.MoveLeft();
        
    }
    void ClickedRight(){
        AudioController.Instance.PlayButtonPressedSound();
        characterCarousel.MoveRight();
    }

    void ClickedStart(){
        AudioController.Instance.PlayButtonPressedSound();
         if(characterCarousel.SetCharacter()){
            navigationScript.SwitchScreen();
         }
    }
}
