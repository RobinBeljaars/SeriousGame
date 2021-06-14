using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class scenariosScript : MonoBehaviour
{
    public NavigationScript footBallScene;
    public NavigationScript dishWashScene;
    public NavigationScript homeWorkScene;
    public NavigationScript lisaScene;
    public Text scenarioTextBox;
    public Text ageTextbox;
    public Button continueBtn;
    public Button answer1Btn;
    public Button answer2Btn;
    Dictionary<int, string> scenarios = new Dictionary<int, string>();
    // Start is called before the first frame update
    void Start()
    {
        Button btn = continueBtn.GetComponent<Button>();
        btn.onClick.AddListener(continueScenario);

        Button answer1 = answer1Btn.GetComponent<Button>();
        answer1.onClick.AddListener(firstAnswer);

        Button answer2 = answer2Btn.GetComponent<Button>();
        answer2.onClick.AddListener(secondAnswer);

        retrieveScenarios();

        if (ageTextbox.text.Equals("12") && Game.currentGame.scenariosCount == 0)
        {
            setStartingScenario();
        }
        if (ageTextbox.text.Equals("13") && Game.currentGame.scenariosCount == 3)
        {
            setSecondScenario();
            continueBtn.interactable = true;
        }
        if (ageTextbox.text.Equals("14") && Game.currentGame.scenariosCount == 6)
        {
            setThirdScenario();
            continueBtn.interactable = true;
        }
        if (Game.currentGame.scenariosCount % 3 == 0)
        {
            continueBtn.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Game.currentGame.scenariosCount);
        if (ageTextbox.text.Equals("12") && Game.currentGame.scenariosCount == 0)
        {
            setStartingScenario();
        }
        if (ageTextbox.text.Equals("13") && Game.currentGame.scenariosCount == 3)
        {
            setSecondScenario();
            continueBtn.interactable = true;
        }
        if (ageTextbox.text.Equals("14") && Game.currentGame.scenariosCount == 6)
        {
            setThirdScenario();
            continueBtn.interactable = true;
        }
        if (Game.currentGame.scenariosCount % 3 == 0)
        {
            continueBtn.interactable = false;
        }

        switch (Game.currentGame.scenariosCount)
        {
            case 6:
                if (Game.currentGame.scenario1Played)
                {
                    disableAnswerBtns();
                } else
                {
                    answer1Btn.gameObject.SetActive(true);
                    answer2Btn.gameObject.SetActive(true);
                }

                Text text = answer1Btn.gameObject.GetComponentInChildren<Text>(true);
                Text text2 = answer2Btn.gameObject.GetComponentInChildren<Text>(true);

                text.text = "Voetballen";
                text2.text = "Klusje doen";
                break;
            case 9:
                if (Game.currentGame.scenario2Played)
                {
                    disableAnswerBtns();
                } else
                {
                    answer1Btn.gameObject.SetActive(true);
                    answer2Btn.gameObject.SetActive(true);
                }

                Text answer1text = answer1Btn.gameObject.GetComponentInChildren<Text>(true);
                Text answer2text = answer2Btn.gameObject.GetComponentInChildren<Text>(true);
                answer1text.text = "Huiswerk maken";
                answer2text.text = "Naar Lisa gaan";
                break;
            default:
                answer1Btn.gameObject.SetActive(false);
                answer2Btn.gameObject.SetActive(false);
                break;
        }
    }

    void disableAnswerBtns()
    {
        answer1Btn.gameObject.SetActive(false);
        answer2Btn.gameObject.SetActive(false);
    }

    void firstAnswer()
    {
        if (Game.currentGame.scenariosCount == 6)
        {
            footBallScene.SwitchScreen();
            Game.currentGame.scenario1Played = true;
        } else if (Game.currentGame.scenariosCount == 9)
        {
            homeWorkScene.SwitchScreen();
            Game.currentGame.scenario2Played = true;
        }
    }

    void secondAnswer()
    {
        if (Game.currentGame.scenariosCount == 6)
        {
            dishWashScene.SwitchScreen();
            Game.currentGame.scenario1Played = true;
        }
        else if (Game.currentGame.scenariosCount == 9)
        {
            lisaScene.SwitchScreen();
            Game.currentGame.scenario2Played = true;
        }
    }

    void retrieveScenarios()
    {
        scenarios.Add(0, "Wat leuk dat je mee doet aan Future Hero. Dit spel moet je ervaren. We gaan meteen beginnen!");
        scenarios.Add(1, "Je bent aangekomen op je startscherm. De acitviteiten die je doet bepalen je succes. ");
        scenarios.Add(2, "Denk goed na over je keuzes!");
        scenarios.Add(3, "Tom vraagt of je mee gaat voetballen. Je hebt ook zin om te voetballen! ");
        scenarios.Add(4, "Maar je kan ook thuis een klusje doen waarmee je een klein bedrag mee kunt verdienen.");
        scenarios.Add(5, "Wat kies je?");
        scenarios.Add(6, "Je hebt huiswerk gekregen voor Nederlands. Lisa heeft gevraagd of je zin hebt om bij haar thuis mee te doen aan een Fifa toernooi.");
        scenarios.Add(7, "De moeder van Lisa heeft ranja gemaakt en koekjes gebakken.");
        scenarios.Add(8, "Wat kies je?");
    }

    void setStartingScenario()
    {
        scenarioTextBox.text = scenarios[Game.currentGame.scenariosCount];
        Game.currentGame.scenariosCount++;
    }

    void setSecondScenario()
    {
        scenarioTextBox.text = scenarios[Game.currentGame.scenariosCount];
        Game.currentGame.scenariosCount++;
    }

    void setThirdScenario()
    {
        scenarioTextBox.text = scenarios[Game.currentGame.scenariosCount];
        Game.currentGame.scenariosCount++;
    }

    void continueScenario()
    {
        scenarioTextBox.text = scenarios[Game.currentGame.scenariosCount];
        Debug.Log(Game.currentGame.scenariosCount);

        if (Game.currentGame.scenariosCount + 1 % 3 == 0)
        {
            continueBtn.interactable = false;
        }
        Game.currentGame.scenariosCount++;
    }
}
