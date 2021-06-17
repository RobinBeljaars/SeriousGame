using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalKeeperState : State<GoalKeeper>
{
    public static GlobalKeeperState instance = null;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        // DontDestroyOnLoad(this.gameObject);
    }

    public override bool OnMessage(GoalKeeper keeper, Telegram_CH4 telegram)
    {
        switch (telegram.GetMessageIndex())
        {
            case SoccerMessages.Msg_GoHome:
                {
                   keeper.Steering().SetTarget(keeper.Team().initialRegion[0]);
                    keeper.GetFSM().ChangeState(ReturnHome.instance);

                    return true;
                }
            case SoccerMessages.Msg_ReceiveBall:
                {
                    keeper.GetFSM().ChangeState(InterceptBall.instance);
                    return true;
                }
        }
        return false;
    }

}