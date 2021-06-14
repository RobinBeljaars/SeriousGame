using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering_CH4 : MonoBehaviour
{
    void Awake()
    {
        player = GetComponent<PlayerBase>();
        ball = GameObject.Find("Ball").GetComponent<SoccerBall>();
        multSeparation = 1f;
        viewDistance = 11f;
    }
    [Header("STATE")]
    [SerializeField]
    bool seek;
    [SerializeField]
    bool arrive;
    [SerializeField]
    bool pursuit;
    [SerializeField]
    bool separate;
    [SerializeField]
    bool interpose;

    [Header("MISC")]
    [SerializeField]
    private PlayerBase player;
    [SerializeField]
    private SoccerBall ball;
    [SerializeField]
    private Vector3 steeringForce;
    [SerializeField]
    private Vector3 target;
    [SerializeField]
    private float interposeDist;
    [SerializeField]
    private float multSeparation;
    [SerializeField]
    private float viewDistance;

    [SerializeField]
    int flags;

    [SerializeField]
    enum behavior_type
    {
        none = 0x0000,
        seek = 0x0001,
        arrive = 0x0002,
        separation = 0x0004,
        pursuit = 0x0008,
        interpose = 0x0010
    }

    [SerializeField]
    bool tagged;

    [SerializeField]
    enum Deceleration
    {
        slow = 3, normal = 2, fast = 1
    }

    Vector3 Seek(Vector3 target)
    {
        Vector3 DesiredVelocity = (target - (Vector3)player.transform.position).normalized * player.myMaxSpeed;

        return (DesiredVelocity - player.Velocity());
    }

    Vector3 Arrive(Vector3 target, Deceleration decel)
    {
        Vector3 toTarget = target - (Vector3)player.transform.position;
        float dist = toTarget.magnitude;

        if (dist > 0)
        {
            const float DecelerationTweaker = 0.3f;
            float speed = dist / ((float)decel * DecelerationTweaker);
            speed = Mathf.Min(speed, player.myMaxSpeed);

            Vector3 desiredVelocity = toTarget * speed / dist;

            return (desiredVelocity - player.Velocity());
        }

        return new Vector3(0f, 1f, 0f);
    }

    Vector3 Pursuit(SoccerBall ball)
    {
        Vector3 toBall = ball.transform.position - player.transform.position;

        float lookAheadTime = 0.0f;

        if (ball.Speed() != 0.0)
        {
            lookAheadTime = toBall.magnitude / ball.Speed();
        }

        target = ball.FuturePosition(lookAheadTime);

        return Arrive(target, Deceleration.fast);
    }

    Vector3 Separation()
    {
        //iterate through all the neighbors and calculate the vector from the
        Vector3 _steeringForce = new Vector3(0f, 1f, 0f);

        List<PlayerBase> allPlayers = new List<PlayerBase>(transform.parent.GetComponentsInChildren<PlayerBase>());

        foreach (var item in allPlayers)
        {
            if (item != player && item.Steering().Tagged())
            {
                Vector3 toAgent = player.transform.position - item.transform.position;
                _steeringForce += (toAgent / toAgent.magnitude).normalized;
            }
        }
        return _steeringForce;
    }

    Vector3 Interpose(SoccerBall ball, Vector3 pos, float distFromTarget)
    {
        return Arrive(target + ((Vector3)ball.transform.position - target).normalized *
                distFromTarget, Deceleration.normal);
    }

    void FindNeighbours()
    {
        List<PlayerBase> allPlayers = new List<PlayerBase>(transform.parent.GetComponentsInChildren<PlayerBase>());

        foreach (var item in allPlayers)
        {
            item.Steering().UnTag();
            Vector3 to = item.transform.position - player.transform.position;

            if (to.magnitude < (viewDistance))
            {
                item.Steering().Tag();
            }
        }
    }

    bool On(behavior_type bt) { return (flags & (int)bt) == (int)bt; }

    bool AccumulateForce(ref Vector3 sf, Vector3 forceToAdd)
    {
        float magnitudeSoFar = sf.magnitude;
        float magnitudeRemaining = player.myMaxSpeed - magnitudeSoFar;

        if (magnitudeRemaining <= 0f) return false;

        float magnitudeToAdd = forceToAdd.magnitude;

        if (magnitudeToAdd > magnitudeRemaining)
            magnitudeToAdd = magnitudeRemaining;
        sf += (forceToAdd).normalized * magnitudeToAdd;

        return true;
    }

    public Vector3 Calculate()
    {
        steeringForce = new Vector3(0f, 0f, 0f);

        steeringForce = SumForces();

        if (steeringForce.sqrMagnitude >= player.myMaxForce.sqrMagnitude)
            steeringForce = player.myMaxForce;

        return steeringForce;
        
    }

     Vector3 SumForces()
    {
        Vector3 force = new Vector3(0f,0f, 0f);
        FindNeighbours();
     
        if (On( behavior_type.separation))
        {
            force += Separation() * multSeparation;

            if (!AccumulateForce(ref steeringForce, force)) return steeringForce;
        }

        if (On(behavior_type.seek))
        {
            force += Seek(target);

            if (!AccumulateForce(ref steeringForce, force)) return steeringForce;
        }
        

        if (On(behavior_type.arrive))
        {
            force += Arrive(target, Deceleration.fast);

            if (!AccumulateForce(ref steeringForce, force)) return steeringForce;
        }
        
        if (On(behavior_type.pursuit))
        {
            force += Pursuit(ball);

            if (!AccumulateForce(ref steeringForce, force)) return steeringForce;
        }
        
        if (On(behavior_type.interpose))
        {
            force += Interpose(ball, target, interposeDist);

            if (!AccumulateForce(ref steeringForce, force)) return steeringForce;
        }
        
        return steeringForce;
    }

    public float ForwardComponent()
    {
        return Vector3.Dot(player.Heading(), steeringForce);
    }

    public float SideComponent()
    {
        return Vector3.Dot(player.SideVector(), steeringForce * player.myMaxTurnRate);
    }

    public Vector3 Force() { return steeringForce; }

    public Vector3 Target() { return target; }

    public void SetTarget(Vector3 t) { target = t; }

    public float InterposeDistance() { return interposeDist; }

    public void SetInterposeDistance(float d) { interposeDist = d; }

    public bool Tagged() { return tagged; }

    public void Tag() { tagged = true; }
    public void UnTag() { tagged = false; }

    public void SeekOn() { flags |= (int)behavior_type.seek; seek = true; }

    public void ArriveOn() { flags |= (int)behavior_type.arrive; arrive = true; }

    public void PursuitOn() { flags |= (int)behavior_type.pursuit; pursuit = true; }

    public void SeperationOn() { flags |= (int)behavior_type.separation; separate = true; }

    public void InterposeOn(float d) { flags |= (int)behavior_type.interpose; interpose = true; interposeDist = d; }

    public void SeekOff() { flags ^= (int)behavior_type.seek; seek = false; }

    public void ArriveOff() { flags ^= (int)behavior_type.arrive; arrive = false; }

    public void PursuitOff() { flags ^= (int)behavior_type.pursuit; pursuit = false; }

    public void SeperationOff() { flags ^= (int)behavior_type.separation; separate = false; }

    public void InterposeOff() { flags ^= (int)behavior_type.interpose; interpose = false; }

    public bool SeekIsOn() { return On(behavior_type.seek); }

    public bool ArriveIsOn() { return On(behavior_type.arrive); }

    public bool PursuitIsOn() { return On(behavior_type.pursuit); }

    public bool SeperationIsOn() { return On(behavior_type.separation); }

    public bool InterposeIsOn() { return On(behavior_type.interpose); }
}
