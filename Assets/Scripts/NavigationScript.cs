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
        if (gameObject.CompareTag("SwitchScene"))
        {
            switch (mode)
            {

                case NavigationMode.scene:
                    SceneManager.LoadScene((int) sceneName);
                    Debug.Log("Switching Scenes");
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
}
