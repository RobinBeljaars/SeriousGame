using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Multiline()]
    public string content;
    public string header;
    public float money = 0;
    public float energy = 0;
    public float happiness = 0;
    public float itemValue = 0;
    public float reputation = 0;
    private static LTDescr delay;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (money != 0 || energy != 0 || happiness != 0 || itemValue != 0 || reputation != 0)
        {
            delay = LeanTween.delayedCall(0.4f, () =>
            {
                ToolTipManager.Show(content, header, money, energy, happiness, itemValue, reputation);
            });
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.cancel(delay.uniqueId);
        ToolTipManager.Hide();
    }
}
