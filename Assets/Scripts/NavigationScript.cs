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

    public bool ignoreUIConstraint =false; //Default is false

    [Header("Leave at 0 to not use")]
    public int minAgeToAllowSwitch =0;
    [Header("Leave at 0 to not use")]
    public float minEnergytoAllowSwitch =0;

    [Header("Do not assign when this script is not a assinged to a child of the map scene")]
    public Text feedBack;

    [Header("UI to toggle on/off on element click")]
    public GameObject[] UIToToggle;

    void Start(){
       if(UICache.GetLastMapSceneUI()!=null){
            Debug.Log("Reloading with: "+UICache.GetLastMapSceneUI());
            foreach (GameObject item in UIToToggle){
                 if(!item.activeInHierarchy&&item.tag.Equals(UICache.GetLastMapSceneUI())){
                        //Last active UI is not allready active AND the items equals or saved tag, we set it now
                        SwitchScreen(true);
                    }
                 }
            }
    }
    void OnMouseUp()
    {
        BaseCharacter playerData = Game.currentGame.PlayerData; 
        Debug.Log("Test");
        if (gameObject.CompareTag("SwitchScene")){

            if(playerData.GetAge()<minAgeToAllowSwitch){
                AudioController.Instance.PlayImpossibleChoice();
                feedBack.text="Je bent niet oud genoeg om dit te mogen...\nJe kunt deze actie uit voeren vanaf "+minAgeToAllowSwitch+" jaar..";
                return;
            }

            if(playerData.getEnergy()<minEnergytoAllowSwitch){
                AudioController.Instance.PlayImpossibleChoice();
                feedBack.text="Je bent nu te moe om deze actie uit te voeren...";
                return;
            }
            SwitchScreen();
        }
    }


    
    
    public void SwitchScreen(bool initialLoad=false){
        
        if(Game.currentGame.GetScenarioStatus()||Game.currentGame.GetEventStatus()){
            //SceneSwitching is NOT allowed when a scenario or event is active
            //If we want to exclude buttons from this behaviour, this is being checked here.
            if(!ignoreUIConstraint&&!initialLoad){
                return;
            }
        }
            if(!initialLoad){
            AudioController.Instance.PlayButtonPressedSound();
            }
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
                    break;
            
        }
    }
}
