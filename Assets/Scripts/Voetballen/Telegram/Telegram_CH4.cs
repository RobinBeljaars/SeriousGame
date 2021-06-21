using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telegram_CH4
{
    int sender;
    int receiver;
    public int Receiver {
        get => receiver;
        set => receiver = value;
    }

    int msg;

    private float dispatchTime;
    public float DispatchTime
    {
        get { return dispatchTime; }
        set { dispatchTime = value; }
    }

    public Transform infos;

    public void ConstructTelegram(float delayTime, int _sender, int _receiver, int _msg, Transform info = null)
    {
        DispatchTime = delayTime;
        sender = _sender;
        receiver = _receiver;
        msg = _msg;
        infos = info;
    }

    public int GetMessageIndex()
    {
        return msg;
    }
}
