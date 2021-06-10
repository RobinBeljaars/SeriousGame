using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerTeam : MonoBehaviour
{
    [SerializeField]
    private GameObject receivingPlayer;
    [SerializeField]
    private GameObject playerClosestToBall;
    [SerializeField]
    private GameObject controllingPlayer;
    [SerializeField]
    private GameObject supportingPlayer;
    [SerializeField]
    SoccerBall ball;
    [SerializeField]

    private StateMachine<SoccerTeam> m_pStateMachine;

    public TeamColor teamColor;
    public SoccerTeam opponentTeam;
    public Goal opponentsGoal;
    public Goal homeGoal;
    public Vector3[] initialRegion = new Vector3[5];
    public List<GameObject> players;

    [Header("Current_State")]
    [SerializeField]
    private string state;

    [SerializeField]
    private float distToBallOfClosestPlayer;

    public void CurStateForDebug()
    {
        if (m_pStateMachine.IsInstate(Defending.instance))
        {
            state = "Defending";   
        }
        else if (m_pStateMachine.IsInstate(Attacking.instance))
        {
            state = "Attacking";
        }
        else
        {
            state = "Nothing";
        }
    }

    void Start()
    {
        if (teamColor == TeamColor.Blue)
        {
            opponentsGoal = GameObject.Find("Reds").transform.GetChild(2).GetComponent<Goal>();
            homeGoal = GameObject.Find("Blues").transform.GetChild(2).GetComponent<Goal>();
            opponentTeam = GameObject.Find("RedTeam").GetComponent<SoccerTeam>();  
        }
        else
        {
            opponentsGoal = GameObject.Find("Blues").transform.GetChild(2).GetComponent<Goal>();
            homeGoal= GameObject.Find("Reds").transform.GetChild(2).GetComponent<Goal>();
            opponentTeam = GameObject.Find("BlueTeam").GetComponent<SoccerTeam>();
        }

        for (int i = 0; i < 5; i++)
        {
            initialRegion[i] = gameObject.transform.GetChild(i).transform.position;
        }

        // Set the FSM
        m_pStateMachine = new StateMachine<SoccerTeam>(this);
        m_pStateMachine.SetCurrentState(PrepareForKickOff.instance);

        ball = GameObject.Find("Ball").GetComponent<SoccerBall>();
        StartCoroutine(UpdateTeamState());
    }

    void CheckItIsOurTeam()
    {
        if (controllingPlayer != null)
        {
            if (controllingPlayer != ball.GetOwner())
            {
                controllingPlayer = null;
            }

        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                if (ball.GetOwner() == players[i])
                {
                    controllingPlayer = players[i];
                }
            }
        }
    }

    public IEnumerator UpdateTeamState()
    {
        while (true)
        {
            CalculateClosestPlayerToBall();
            yield return new WaitForSeconds(0.1f);
            CurStateForDebug();
            m_pStateMachine.Updating();
            CheckItIsOurTeam();
        }
    }

    public float DistToBallOfClosestPlayer()
    {
        return distToBallOfClosestPlayer;
    }

    public void CalculateClosestPlayerToBall()
    {
        float closestSoFar = Mathf.Infinity;

        foreach(var item in players)
        {
            PlayerBase pb = item.GetComponent<PlayerBase>();
            float dist = Vector3.Distance(pb.transform.position, SoccerPitch.instance.ball.transform.position);
            item.GetComponent<PlayerBase>().SetDistToBall(dist);
            if(dist< closestSoFar)
            {
                closestSoFar = dist;
                playerClosestToBall = item;
            }
        }
        distToBallOfClosestPlayer = closestSoFar;
    }

    public bool IsOpponentWithInRadius(Vector3 pos, float rad)
    {
        foreach(var item in opponentTeam.players)
        {
            if (Vector3.Distance(pos, item.transform.position) < rad)
                return true;
        }
        return false;
    }

    public bool HandleMessage(Telegram_CH4 msg)
    {
        return m_pStateMachine.HandleMessage(msg);
    }

    public void ReturnAllFieldPlayersToHome()
    {
        foreach (var item in players)
        {
            PlayerBase pb = item.GetComponent<PlayerBase>();
            if (pb.Role() != "GoalKeeper")
            {
               
                if(teamColor == TeamColor.Red)
                    MessageDispatcher_CH4.instance.DispatchMessage(0f, 1, pb.ID(), SoccerMessages.Msg_GoHome, null);
                else
                    MessageDispatcher_CH4.instance.DispatchMessage(0f, 6, pb.ID(), SoccerMessages.Msg_GoHome, null);

            }
        }
    }

    public void RequestPass(FieldPlayer requester)
    {
        const float requestFrequency = 0.7f;
        float randFloat = UnityEngine.Random.Range(0f, 1f);
        
        if (randFloat > requestFrequency) return;
        
        if (IsPassSafeFromAllOpponents(ControllingPlayer().transform.position, requester.transform.position, requester.gameObject, Prm.instance.MaxPassingForce))
        {
            MessageDispatcher_CH4.instance.DispatchMessage(0f, ControllingPlayer().GetComponent<FieldPlayer>().ID(), requester.ID(), SoccerMessages.Msg_PassToMe, requester.transform);
        }
    }

    public PlayerBase DetermineBestSupportingAttacker()
    {
        float closestSoFar = Mathf.Infinity;

        PlayerBase bestPlayer = null;

        foreach(var item in players)
        {
            if(item.GetComponent<PlayerBase>().Role() == "Attacker" && item != ControllingPlayer())
            {
                float dist = Vector3.Distance(item.transform.position, SupportSpotCalc.instance.bestSupportSpot);
                if(dist < closestSoFar)
                {
                    closestSoFar = dist;
                    bestPlayer = item.GetComponent<FieldPlayer>();
                }
            }
        }
        return bestPlayer;
    }

    public bool InControl()
    {
        if(controllingPlayer != null)
        {
            return true;
        }
        return false;
    }

    public void SetPlayerHomeRegion(int plyr, Vector3 newRegion)
    {
        players[plyr].GetComponent<PlayerBase>().SetHomeRegion(newRegion);
    }

    public PlayerBase PlayerClosestToBall()
    {
        return playerClosestToBall.GetComponent<PlayerBase>();
    }

    public void UpdateTargetsOfWaitingPlayers()
    {
        foreach (var item in players)
        {
            PlayerBase pb = item.GetComponent<PlayerBase>();
            if (pb.Role() != "GoalKeeper")
            {
                FieldPlayer fp = item.GetComponent<FieldPlayer>();
                
                if(fp.GetFSM() == null)
                {
                    Debug.LogError("No FSM");
                }

                if (fp.GetFSM().IsInstate(Wait.instance) || fp.GetFSM().IsInstate(ReturnToHomeRegion.instance))
                {
                    if (fp.Team().teamColor == TeamColor.Blue)
                        fp.Steering().SetTarget((fp.Team().initialRegion[fp.ID() - 6]));
                    else
                        fp.Steering().SetTarget((fp.Team().initialRegion[fp.ID() - 1]));
                }

            }
        }
    }

    public bool AllPlayersAtHome()
    {
        for(int i = 1; i < 5; i++)
        {
            Vector3 playerPos = gameObject.transform.GetChild(i).transform.position;

            if (Vector3.Distance(initialRegion[i], playerPos) > Mathf.Epsilon + 1f)
            {
                return false;
            }
        }
        return true;
    }

    public GameObject ControllingPlayer()
    {
        return controllingPlayer;
    }

    public GameObject SupportingPlayer()
    {
        return supportingPlayer;
    }

    public void SetControllingPlayer(GameObject player)
    {
        controllingPlayer = player;
    }

    public void SetSuppprtingPlayer(GameObject player)
    {
        supportingPlayer = player;
    }

    public void SetReceiver(GameObject player)
    {
        receivingPlayer = player;
    }

    public GameObject Receiver()
    {
        return receivingPlayer;
    }

    public void SetPlayerClosestToBall(GameObject player)
    {
        playerClosestToBall = player;
    }

public bool FindPass(PlayerBase passer, PlayerBase receiver, ref Vector3 passTarget, float power, float minPassingDistance)
    {
        float closestToGoalSoFar = Mathf.Infinity;
        Vector3 target = new Vector3(0f,1f,0f);

        foreach(var item in players)
        {
            if((item.GetComponent<PlayerBase>() != passer) && Vector3.Distance(passer.transform.position, item.transform.position) > minPassingDistance)
            {

                if (GetBestPassToReceiver(passer, item.GetComponent<PlayerBase>(), ref target, power)){
                    float dist2Goal = Mathf.Abs(target.x - opponentsGoal.Center().x);
                    
                    if (dist2Goal < closestToGoalSoFar)
                    {
                        closestToGoalSoFar = dist2Goal;
                        receiver = item.GetComponent<PlayerBase>();

                        passTarget = target;
                    }
                }
            }
        }
        if (receiver) return true;
        else return false;
    }

    public bool GetTangentPoints(Vector3 c, float r, Vector3 p, ref Vector3 t1, ref Vector3 t2)
    {
        Vector3 PmC = p - c;
        float sqrLen = Vector3.SqrMagnitude(PmC);
        float rSqr = r * r;

        if (sqrLen <= rSqr)
            return false;

        float invSqrLen = 1 / sqrLen;
        float root = Mathf.Sqrt(Mathf.Abs(sqrLen - rSqr));
        
        t1.x = c.x + r * (r * PmC.x - PmC.z * root) * invSqrLen;
        t1.z = c.z + r * (r * PmC.z - PmC.z * root) * invSqrLen;
        t2.x = c.x + r * (r * PmC.x - PmC.z * root) * invSqrLen;
        t2.z = c.z + r * (r * PmC.z - PmC.z * root) * invSqrLen;

        return true;
    }

    public bool GetBestPassToReceiver(PlayerBase passer, PlayerBase receiver, ref Vector3 passTarget, float power)
    {
        float time = SoccerPitch.instance.ball.TimeToCoverDistance(SoccerPitch.instance.ball.transform.position, receiver.transform.position, power);

        if (time < 0) return false;

        float interceptRange = time * receiver.myMaxSpeed;

        float scalingFactor = .3f;
        interceptRange *= scalingFactor;

        Vector3 ip1, ip2;
        ip1 = new Vector3(0, 1, 0);
        ip2 = new Vector3(0, 1,0);
        GetTangentPoints(receiver.transform.position, interceptRange, SoccerPitch.instance.ball.transform.position, ref ip1, ref ip2);

        const int numPassesToTry = 3;
        Vector3[] passes = new Vector3[numPassesToTry] { ip1, receiver.transform.position, ip2 };


        float closestSoFar = Mathf.Infinity;

        bool result = false;
        for(int pass = 0; pass < numPassesToTry; ++pass)
        {
            float dist = Mathf.Abs(passes[pass].x - opponentsGoal.Center().x);
            if (dist < closestSoFar &&
                IsPassSafeFromAllOpponents(SoccerPitch.instance.ball.transform.position, passes[pass], receiver.transform.gameObject, power)
                && SoccerPitch.instance.Inside(passes[pass]))
            {
                closestSoFar = dist;
                passTarget = passes[pass];
                result = true;
            }
        }

        return result;
    }

    public Vector3 GetSupportSpot(TeamColor team) { return SupportSpotCalc.instance.GetBestSupportingSpot(team); }
    public Goal HomeGoal() { return homeGoal; }

    public bool CanShoot(Vector3 ballPos, float power, ref Vector3 shotTarget)
    {
        int numAttempts = Prm.instance.NumAttemptsToFindValidStrike;

        while (numAttempts-- > 0)
        {
            shotTarget = opponentsGoal.Center();
            
            int minZVal = (int)(opponentsGoal.RightPost().z + .5f);
            int maxZVal = (int)(opponentsGoal.LeftPost().z - .5f);

            shotTarget.z = (float)UnityEngine.Random.Range(minZVal, maxZVal);
           
            float time = ball.TimeToCoverDistance(ballPos, shotTarget, power);

            if(time >= 0f)
            {   
                if(IsPassSafeFromAllOpponents(ballPos, shotTarget, null, power))
                {
                    return true;
                }
            }

            if ((ballPos - shotTarget).magnitude < 20f)
            {
                return false;
            }
        }
        return false;
    }

    public bool IsPassSafeFromOpponent(Vector3 from, Vector3 target, GameObject receiver, GameObject opp, float passingForce)
    {
        Vector3 toTarget = target - from;
        Vector3 toTargetNormalized = toTarget.normalized;

        Vector3 localPosOpp = new Vector3(0, 0);

        if (receiver)
        {
            localPosOpp.x = receiver.transform.position.x;
            localPosOpp.z = receiver.transform.position.z;
        }

        if (localPosOpp.x < 0)
        {
            return true;
        }

        if (Vector3.Distance(from, target) < Vector3.Distance(opp.transform.position, from))
        {
            if (receiver)
            {
                if (Vector3.Distance(target, opp.transform.position) > Vector3.Distance(target, receiver.transform.position))
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                return true;
            }
        }

        float timeForBall = ball.TimeToCoverDistance(new Vector3(0, 0), target, passingForce);
        float reach = opp.GetComponent<PlayerBase>().myMaxSpeed / UnityEngine.Random.Range(4.9f, 5.2f) * timeForBall + 5f;
        float expectedBall = timeForBall * Prm.instance.MaxShootingForce + .7f;

        if (expectedBall < reach)
        {
            return false;
        }
        else
            return true;
    }

    public bool IsPassSafeFromAllOpponents(Vector3 from, Vector3 target, GameObject receiver, float passingForce)
    {
        string color = "";
        if (TeamColor.Red == teamColor)
        {
            color = "BlueTeam";
        }
        else color = "RedTeam";

        var group = GameObject.Find(color).GetComponentsInChildren<PlayerBase>();
        List<PlayerBase> opponents = new List<PlayerBase>(group);
        opponents.RemoveAt(0);
        foreach(var item in opponents)
        {
            if (!IsPassSafeFromOpponent(from, target, receiver, item.gameObject, passingForce))
            {
                return false;
            }
        }

        return true;
    }

    public StateMachine<SoccerTeam> GetFSM()
    {
        return m_pStateMachine;
    }
}
