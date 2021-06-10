using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dribble : State<FieldPlayer>
{
    public static Dribble instance = null;
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
        player.Team().SetControllingPlayer(player.gameObject);
    }

    public override void Execute(FieldPlayer player)
    {
        float _dot = Vector3.Dot(player.Team().HomeGoal().Facing(), player.Heading());
        if (_dot < 0)
        {
            Vector3 direction = -player.Heading();
            float sign = Vector3.Dot(player.Team().HomeGoal().Facing(), player.Heading());
            sign = sign > 0 ? 1f : -1f;
            float angle = Mathf.PI / 4 * -1 * sign;

            player.transform.Rotate(direction, angle);

            const float kickingForce = .2f;

            player.Ball().SetOwner(player.gameObject);
            player.Ball().Kick(direction, kickingForce);
        }
        else
        {
            player.Ball().SetOwner(player.gameObject);
            player.Ball().Kick(player.Team().HomeGoal().Facing(), .5f);
        }

        player.GetFSM().ChangeState(ChaseBall.instance);
        return;
    }

    public override void Exit(FieldPlayer player)
    {
    }
}