using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickBall : State<FieldPlayer>
{
    public static KickBall instance = null;
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

    IEnumerator ReadyForKick(FieldPlayer fp)
    {
        bool flag = false;
        int num = Prm.instance.PlayerKickFrequency;
        WaitForSeconds ws = new WaitForSeconds((1f / num) * Random.Range(0.1f, .5f));
        for (int i = 0; i < num; i++)
        {
            if (Random.Range(0f, 1f) < .8f)
            {
                flag = true;
                break;
            }
            yield return ws;
        }

        if (!flag)
        {
            fp.GetFSM().ChangeState(ChaseBall.instance);
        }

    }

    public override void Enter(FieldPlayer player)
    {
        player.Team().SetControllingPlayer(player.gameObject);
        StartCoroutine(ReadyForKick(player));

    }

    public Vector3 AddNoiseToKick(Vector3 pos, Vector3 target)
    {
        Vector3 tmp = new Vector3(target.x * Prm.instance.PlayerKickingAccuracy, target.y, target.z * Prm.instance.PlayerKickingAccuracy);
        return tmp;
    }

    public override void Execute(FieldPlayer player)
    {
        Vector3 toBall = player.Ball().transform.position - player.transform.position;

        float _dot = Vector3.Dot(player.Heading(), toBall.normalized);

        if (player.Team().Receiver() != null || player.Pitch().GoalKeeperHasBall() || _dot < 0)
        {
            player.GetFSM().ChangeState(ChaseBall.instance);
            return;
        }

        Vector3 ballTarget = new Vector3(0f, 1f, 0f);

        float power = Prm.instance.MaxShootingForce * _dot;
        float rf = Random.Range(0f, 1f);

        if (player.Team().CanShoot(player.Ball().transform.position, power, ref ballTarget) || rf < Prm.instance.ChancePlayerAttemptsPotShot)
        {
            Debug.Log("Power : " + power);

            ballTarget = AddNoiseToKick(player.Ball().transform.position, ballTarget);

            Vector3 kickDirection = (ballTarget - (Vector3)player.Ball().transform.position);

            player.Ball().SetOwner(player.gameObject);
            player.Ball().Kick(kickDirection, power);

            player.GetFSM().ChangeState(Wait.instance);
            player.Ball().SetOwner(null);

            player.FindSupport();
            return;
        }

        PlayerBase receiver = null;

        power = Prm.instance.MaxPassingForce * _dot;
        if (player.IsThreatened() && player.Team().FindPass(player, receiver, ref ballTarget, power, Prm.instance.MinPassDist))
        {
            Debug.Log("Power : " + power);

            ballTarget = AddNoiseToKick(player.Ball().transform.position, ballTarget);
            Vector3 kickDirection = ballTarget - (Vector3)player.Ball().transform.position;

            player.Ball().SetOwner(player.gameObject);
            player.Ball().Kick(kickDirection, power);

            GameObject ballTargetTr = new GameObject();
            ballTargetTr.transform.position = ballTarget;

            MessageDispatcher_CH4.instance.DispatchMessage(0f, player.ID(), receiver.ID(), SoccerMessages.Msg_ReceiveBall, ballTargetTr.transform);
            player.GetFSM().ChangeState(Wait.instance);
            player.FindSupport();
            return;
        }
        else
        {
            player.FindSupport();
            player.GetFSM().ChangeState(Dribble.instance);
        }

    }

    public override void Exit(FieldPlayer player)
    {

        //player.Team().SetControllingPlayer(null);
    }
}