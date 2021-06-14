using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportAttacker : State<FieldPlayer>
{
    public static SupportAttacker instance = null;
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

    public override void Enter(FieldPlayer player)
    {
        player.Steering().ArriveOn();
        player.Steering().SetTarget(player.Team().GetSupportSpot(player.Team().teamColor));
    }

    public override void Execute(FieldPlayer player)
    {
        if (!player.Team().InControl())
        {
            player.GetFSM().ChangeState(ReturnToHomeRegion.instance);
            return;
        }

        if(Vector3.Distance(player.Team().GetSupportSpot(player.Team().teamColor),  player.Steering().Target()) > Mathf.Epsilon)
        {
            player.Steering().SetTarget(player.Team().GetSupportSpot(player.Team().teamColor));
            player.Steering().ArriveOn();
        }

        Vector3 t = new Vector3();
        if ( player.Ball().GetOwner() != player.gameObject && player.Team().CanShoot(player.transform.position, Prm.instance.MaxShootingForce,ref t)
            && player.Ball().GetOwner () != null && player.Ball().GetOwner().transform.parent.GetComponent<SoccerTeam>().teamColor == player.Team().teamColor)
        {
            player.Team().RequestPass(player);          
        }

        if (player.AtTarget())
        {
            player.Steering().ArriveOff();

            player.TrackBall();

            player.SetVelocity(Vector3.zero);

            if (!player.IsThreatened() && player.Ball().GetOwner() != player.gameObject && player.Ball().GetOwner() != null 
                && player.Ball().GetOwner().transform.parent.GetComponent<SoccerTeam>().teamColor == player.Team().teamColor)
            {
                player.Team().RequestPass(player);
            }
        }
    }

    public override void Exit(FieldPlayer player)
    {
        player.Team().SetSuppprtingPlayer(null);
        player.Steering().ArriveOff();
    }
}