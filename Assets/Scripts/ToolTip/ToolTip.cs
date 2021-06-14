using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ToolTip : MonoBehaviour
{
    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;
    public TextMeshProUGUI moneyField;
    public TextMeshProUGUI energyField;
    public TextMeshProUGUI happinessField;
    public TextMeshProUGUI itemValueField;
    public TextMeshProUGUI reputationField;
    public TextMeshProUGUI educationField;
    public LayoutElement layoutElement;
    public int characterWrapLimit;
    public RectTransform rectTransform;
    public GameObject seperator;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetText(string content, string header = "", float money = 0, float energy = 0, float happiness = 0, float itemValue = 0, float reputation = 0,float education=0)
    {
        if(string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }

        if (moneyField != null)
        {
            moneyField.color = Color.black;

            if (money == 0)
            {
                moneyField.transform.parent.gameObject.SetActive(false);
                seperator.SetActive(false);
            }
            else
            {
                if (money > 0)
                    moneyField.color = new Color(0, 166f/255f, 3f/255f);
                else if(money < 0)
                    moneyField.color = new Color(180f/255f, 0, 0);

                moneyField.transform.parent.gameObject.SetActive(true);
                if(string.IsNullOrEmpty(header) || string.IsNullOrEmpty(content))
                    seperator.SetActive(true);
                moneyField.text = "Geld: " + money;
            }
        }

        if (energyField != null)
        {
            if (energy == 0)
            {
                energyField.transform.parent.gameObject.SetActive(false);
                seperator.SetActive(false);
            }
            else
            {
                if (energy > 0)
                    energyField.color = new Color(0, 166f/255f, 3f/255f);
                else if (energy < 0)
                    energyField.color = new Color(180f/255f, 0, 0);

                energyField.transform.parent.gameObject.SetActive(true);
                if (!string.IsNullOrEmpty(header) || !string.IsNullOrEmpty(content))
                    seperator.SetActive(true);
                energyField.text = "Energie: " + energy;
            }
        }

        if (happinessField != null)
        {
            if (happiness == 0)
            {
                happinessField.transform.parent.gameObject.SetActive(false);
                seperator.SetActive(false);
            }
            else
            {
                if (happiness > 0)
                    happinessField.color = new Color(0, 166f/255f, 3f/255f);
                else if (happiness < 0)
                    happinessField.color = new Color(180f/255f, 0, 0);

                happinessField.transform.parent.gameObject.SetActive(true);
                if (!string.IsNullOrEmpty(header) || !string.IsNullOrEmpty(content))
                    seperator.SetActive(true);
                happinessField.text = "Stemming: " + happiness;
            }
        }

        if (itemValueField != null)
        {
            if (itemValue == 0)
            {
                itemValueField.transform.parent.gameObject.SetActive(false);
                seperator.SetActive(false);
            }
            else
            {
                if (itemValue > 0)
                    itemValueField.color = new Color(0, 166f/255f, 3f/255f);
                else if (itemValue < 0)
                    itemValueField.color = new Color(180f/255f, 0, 0);

                itemValueField.transform.parent.gameObject.SetActive(true);
                if (!string.IsNullOrEmpty(header) || !string.IsNullOrEmpty(content))
                    seperator.SetActive(true);
                itemValueField.text = "Bezitswaarde: " + itemValue;
            }
        }

        if (reputationField != null)
        {
            if (reputation == 0)
            {
                reputationField.transform.parent.gameObject.SetActive(false);
                seperator.SetActive(false);
            }
            else
            {
                if (reputation > 0)
                    reputationField.color = new Color(0, 166f/255f, 3f/255f);
                else if (reputation < 0)
                    reputationField.color = new Color(180f/255f, 0, 0);

                reputationField.transform.parent.gameObject.SetActive(true);
                if (!string.IsNullOrEmpty(header) || !string.IsNullOrEmpty(content))
                    seperator.SetActive(true);
                reputationField.text = "Reputatie: " + reputation;
            }
        }

        if (educationField != null)
        {
            if (education == 0)
            {
                educationField.transform.parent.gameObject.SetActive(false);
                seperator.SetActive(false);
            }
            else
            {
                if (education > 0)
                    educationField.color = new Color(0, 166f/255f, 3f/255f);
                else if (education < 0)
                    educationField.color = new Color(180f/255f, 0, 0);

                educationField.transform.parent.gameObject.SetActive(true);
                if (!string.IsNullOrEmpty(header) || !string.IsNullOrEmpty(content))
                    seperator.SetActive(true);
                educationField.text = "Scholing: " + education;
            }
        }

        contentField.text = content;

        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit);
    }

    private void Update()
    {
        if(Application.isEditor)
        {
            int headerLength = headerField.text.Length;
            int contentLength = contentField.text.Length;

            layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit);
        }

        Vector2 position    = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position  = position;
    }
}
