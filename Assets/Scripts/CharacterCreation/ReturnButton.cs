using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReturnButton : MonoBehaviour
{

    public Button Return;

    public NavigationScript navigationScript;


    // Start is called before the first frame update
    void Start()
    {
        Return.GetComponent<Button>().onClick.AddListener(clickedReturn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void clickedReturn(){
        AudioController.Instance.PlayButtonPressedSound();
        navigationScript.SwitchScreen();
    }
  
}
