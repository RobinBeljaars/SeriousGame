using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NavigationScript : MonoBehaviour
{
    [Header("Parameters")]
    public NavigationMode mode;
    public NavigateScene sceneName;

    [Header("UI to toggle on/off on element click")]
    public GameObject[] UIToToggle;

    void OnMouseUp()
    {
        
        Debug.Log("Test");
        if (gameObject.CompareTag("SwitchScene")){
            AudioController.Instance.PlayButtonPressedSound();
            SwitchScreen();
    }
    
    }
    public void SwitchScreen(){
        
        
            //At Every SceneChange we should autosave the game
            SaveLoadGame.Save();
            switch (mode)
            {

                case NavigationMode.scene:
                Debug.Log("Switching Scenes: "+sceneName);
                    SceneManager.LoadScene(sceneName.ToString());
                    break;
                case NavigationMode.ui:
                    foreach (GameObject item in UIToToggle)
                    {
                        item.SetActive(!item.activeInHierarchy);
                    }
                    // UIToToggle.SetActive(!UIToToggle.activeInHierarchy);
                    break;
            
        }
    }
}
