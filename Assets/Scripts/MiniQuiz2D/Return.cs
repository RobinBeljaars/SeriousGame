using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Return : MonoBehaviour
{

    public NavigationScript navigationScript;

    public Button nextButton;

    public float moodReward;
    public float energyCost;

    public float educationReward;
    // Start is called before the first frame update
    void Start()
    {
           nextButton.GetComponent<Button>().onClick.AddListener(ClickedNext);
    }

    // Update is called once per frame
    void ClickedNext()
    {
            Game.currentGame.PlayerData.DecrementEnergy(energyCost);
            Game.currentGame.PlayerData.IncrementHappiness(moodReward);
            navigationScript.SwitchScreen();
    }
}
