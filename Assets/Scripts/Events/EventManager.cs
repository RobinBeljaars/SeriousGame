using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public GameObject window;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descText;
    public GameObject optionObject;
    public GameObject horizontalOptionList;
    public GameObject verticalOptionList;
    public List<Event> events = new List<Event>();
    private int currentEventId = -1;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            GenerateEvent();
    }

    public void GenerateEvent(int id = -1)
    {
        if (id != -1)
        {
            SetEventData(events[id]);
        }

        if (events.Count >= 1)
        {
            int randomId = 0;
            int healthCounter = 0;

            do
            {
                randomId = Random.Range(0, events.Count);

                if (PlayerProperties.age >= events[randomId].minAge && PlayerProperties.age <= events[randomId].maxAge)
                    break;

                healthCounter++;

                if (healthCounter >= 100)
                {
                    Debug.Log("Geen events gevonden die in de juiste leeftijdscategorie vallen.");
                    randomId = 0;
                    break;
                }
            } while (true);

            SetEventData(events[randomId]);
        }
        else
            Debug.Log("Geen events gevonden!");
    }

    private void SetEventData(Event randomEvent)
    {
        currentEventId = events.IndexOf(randomEvent);
        titleText.text = randomEvent.title;
        descText.text = randomEvent.desc;

        window.SetActive(true);

        horizontalOptionList.SetActive(true);
        verticalOptionList.SetActive(true);

        foreach (Transform child in horizontalOptionList.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in verticalOptionList.transform)
        {
            Destroy(child.gameObject);
        }

        if (randomEvent.options.Count <= 2)
        {
            verticalOptionList.SetActive(false);
            horizontalOptionList.SetActive(true);
        }
        else
        {
            verticalOptionList.SetActive(true);
            horizontalOptionList.SetActive(false);
        }

        foreach (EventOption option in randomEvent.options)
        {
            GameObject _optionObject = null;

            if (randomEvent.options.Count <= 2)
                _optionObject = Instantiate(optionObject, horizontalOptionList.transform);
            else
                _optionObject = Instantiate(optionObject, verticalOptionList.transform);

            _optionObject.GetComponentInChildren<TextMeshProUGUI>().text = option.title;
            _optionObject.GetComponent<EventOptionPicker>().option  = option;
            _optionObject.GetComponent<ToolTipTrigger>().money      = option.moneyReward;
            _optionObject.GetComponent<ToolTipTrigger>().energy     = option.energyReward;
            _optionObject.GetComponent<ToolTipTrigger>().happiness  = option.happinessReward;
            _optionObject.GetComponent<ToolTipTrigger>().itemValue  = option.itemValueReward;
            _optionObject.GetComponent<ToolTipTrigger>().reputation = option.reputationReward;
        }
    }

    public void EndEvent()
    {
        window.SetActive(false);
    }
}
