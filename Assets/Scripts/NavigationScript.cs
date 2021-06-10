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

    void Start(){
       if(UICache.GetLastMapSceneUI()!=null){
            Debug.Log("Reloading with: "+UICache.GetLastMapSceneUI());
            foreach (GameObject item in UIToToggle){
                 if(!item.activeInHierarchy&&item.tag.Equals(UICache.GetLastMapSceneUI())){
                        //Last active UI is not allready active AND the items equals or saved tag, we set it now
                        SwitchScreen();
                    }
                 }
            }
    }
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
                   // UICache.setObjects(UIToToggle);
                    SceneManager.LoadScene(sceneName.ToString());
                    break;
                case NavigationMode.ui:
                    foreach (GameObject item in UIToToggle)
                    {
                        if(!item.activeInHierarchy){
                            Debug.Log("Switching to: "+item.tag);
                            UICache.SetLastMapSceneUI(item.tag);
                            item.SetActive(true);
                        }else{
                            item.SetActive(false);
                        }
                        
                    }
                    // UIToToggle.SetActive(!UIToToggle.activeInHierarchy);
                    break;
            
        }
    }
}
