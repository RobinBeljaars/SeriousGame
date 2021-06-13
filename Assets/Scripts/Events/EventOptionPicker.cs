using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventOptionPicker : MonoBehaviour
{
    public EventOption option = null;

    public void PickOption()
    {
        option.GainReward();
        GetComponent<ToolTipTrigger>().OnPointerExit(null);
        FindObjectOfType<EventManager>().EndEvent();
    }
}
