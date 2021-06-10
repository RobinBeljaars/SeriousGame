using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    public Button Exit;

  
    // Start is called before the first frame update
    void Start()
    {
        Exit.GetComponent<Button>().onClick.AddListener(clickedExit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void clickedExit(){
        AudioController.Instance.PlayButtonPressedSound();
        Application.Quit();
    }
}
