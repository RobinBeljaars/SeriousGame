using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : State<SoccerTeam>
{
    public static Attacking instance = null;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public override void Enter(SoccerTeam team)
    {
        Vector3[] blueRegions = new Vector3[5]; 
        Vector3[] redRegions = new Vector3[5]; 

        for(int i = 0; i < 5; i++)
        {
            blueRegions[i] = GameObject.Find("BlueTeam").GetComponent<SoccerTeam>().initialRegion[i];
            redRegions[i] = GameObject.Find("RedTeam").GetComponent<SoccerTeam>().initialRegion[i];
        }


        if (team.teamColor == TeamColor.Blue)
        {
            blueRegions[3] = redRegions[1];
            blueRegions[4] = redRegions[2];
            blueRegions[1] = redRegions[3];
            blueRegions[2] = redRegions[4];
            Defending.instance.ChangePlayerHomeRegion(team, blueRegions);
        }
        else
        {
            redRegions[1] = blueRegions[3];
            redRegions[2] = blueRegions[4];
            redRegions[3] = blueRegions[1];
            redRegions[4] = blueRegions[2];
            Defending.instance.ChangePlayerHomeRegion(team, redRegions);
        }
        
        team.UpdateTargetsOfWaitingPlayers();
    }

    public override void Execute(SoccerTeam team)
    {
        if (!team.InControl())
        {     
            team.GetFSM().ChangeState(Defending.instance);
        }

        SupportSpotCalc.instance.DetermineBestSupportingPosition(team.teamColor);
    }

    public override void Exit(SoccerTeam team)
    {
        team.SetSuppprtingPlayer(null);
    }

}
