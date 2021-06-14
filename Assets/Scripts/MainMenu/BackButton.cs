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
        
        navigationScript.SwitchScreen();
    }

}
