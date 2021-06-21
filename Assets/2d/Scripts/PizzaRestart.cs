using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PizzaRestart : MonoBehaviour
{

    public NavigationScript navigationScript;

    public float moneyReward;
    public float energyCost;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Game.currentGame.PlayerData.AlterMoney(moneyReward);
            Game.currentGame.PlayerData.AlterEnergy(energyCost);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        }
    }
}
