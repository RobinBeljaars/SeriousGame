using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NavigationScript : MonoBehaviour
{
    public string sceneName;

    void OnMouseOver() {
        
    }

    void OnMouseUp()
    {
        Debug.Log("Test");
        if (gameObject.CompareTag("SwitchScene"))
        {
            SceneManager.LoadScene(sceneName);
            Debug.Log("Switching Scenes");
        }
    }
}
