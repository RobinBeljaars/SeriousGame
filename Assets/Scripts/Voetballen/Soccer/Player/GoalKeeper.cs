using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKeeper : FieldPlayer
{
    [SerializeField]
    StateMachine<GoalKeeper> m_pStateMachine;

    void Start()
    {
        m_pStateMachine = new StateMachine<GoalKeeper>(this);
        m_pStateMachine.SetCurrentState(ReturnHome.instance);
        m_pStateMachine.SetGlobalState(GlobalKeeperState.instance);
        EntityManager_CH4.instance.RegisterEntity(this);
        StartCoroutine(Updating());
    }

    public Vector3 GetRearInterposeTarget()
    {
        float x = Team().HomeGoal().Center().x;
        float z = 0f - 1 * .5f + (Ball().transform.position.z * 10f) / 20f;
        return new Vector3(x, transform.position.y, z);
    }

    public new void SetTarget(Vector3 newPos)
    {
        transform.position = newPos;
    }

    public new bool HandleMessage(Telegram_CH4 msg)
    {
        return m_pStateMachine.HandleMessage(msg);
    }

    public new IEnumerator SteeringUpdate()
    {
        while (true)
        {
            Vector3 desiredVelocity = Steering().Calculate() / 70f;
            transform.position = (Vector3)transform.position + desiredVelocity;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public new IEnumerator Updating()
    {
        StartCoroutine(SteeringUpdate());
        while (true)
        {

           // CurStateForDebug();
            m_pStateMachine.Updating();
            yield return new WaitForSeconds(0.2f);
        }
    }

    public new bool BallWithKickingRange()
    {
        return Vector3.Distance(Ball().transform.position, transform.position) < Prm.instance.PlayerKickingDistance;
    }

    public new  bool BallWithinReceivingRange()
    {

        return Vector3.Distance(Ball().transform.position, transform.position) < Prm.instance.BallWithinReceivingRange;
    }

public bool BallWithinKeeperRange()
    {
        return Vector3.Distance(transform.position, Ball().transform.position) < Prm.instance.KeeperInBallRange;
    }

    public bool BallWithinRangeForIntercept()
    {
        return (Vector3.Distance(Team().HomeGoal().Center(), Ball().transform.position) <= Prm.instance.GoalKeeperInterceptRange);
    }

    public bool TooFarFromGoalMouth()
    {
        return Vector3.Distance(transform.position, GetRearInterposeTarget()) > Prm.instance.GoalKeeperInterceptRange;
    }

    public new StateMachine<GoalKeeper> GetFSM()
    {
        return m_pStateMachine;
    }
}
