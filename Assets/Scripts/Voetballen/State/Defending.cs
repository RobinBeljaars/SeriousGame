using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defending : State<SoccerTeam>
{
    public static Defending instance = null;
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

    public override void Enter(SoccerTeam team)
    {
        Vector3[] blueRegions = GameObject.Find("BlueTeam").GetComponent<SoccerTeam>().initialRegion;
        Vector3[] redRegions = GameObject.Find("RedTeam").GetComponent<SoccerTeam>().initialRegion;

        if (team.teamColor == TeamColor.Blue)
        {
            ChangePlayerHomeRegion(team, blueRegions);
        }
        else
        {
            ChangePlayerHomeRegion(team, redRegions);

        }

        team.UpdateTargetsOfWaitingPlayers();
    }

    public override void Execute(SoccerTeam team)
    {
        if (team.InControl())
        {
            team.GetFSM().ChangeState(Attacking.instance);
            return;
        }
    }

    public override void Exit(SoccerTeam team)
    { }

    public void ChangePlayerHomeRegion(SoccerTeam team, Vector3[] region)
    {
        for (int p = 0; p < 5; p++)
        {
            team.SetPlayerHomeRegion(p, region[p]);
        }
    }
}
