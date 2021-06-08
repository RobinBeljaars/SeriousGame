using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipManager : MonoBehaviour
{
    public ToolTip toolTip;
    private static ToolTipManager current;

    private void Awake()
    {
        current = this;
    }

    public static void Show(string content, string header = "", float money = 0, float energy = 0, float happiness = 0, float itemValue = 0, float reputation = 0)
    {
        current.toolTip.SetText(content, header, money, energy, happiness, itemValue, reputation);
        current.toolTip.gameObject.SetActive(true);
        LeanTween.alphaCanvas(current.toolTip.gameObject.GetComponent<CanvasGroup>(), 1, 0.2f);
    }

    public static void Hide()
    {
        current.toolTip.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        current.toolTip.gameObject.SetActive(false);
    }
}
