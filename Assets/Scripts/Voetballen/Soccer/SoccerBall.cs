using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBall : MovingEntity_CH4
{
    [SerializeField]
    private Vector3 oldPos;
    [SerializeField]
    private Vector3 velocity;
    [SerializeField]
    // pointer of a player who got a ball
    private GameObject owner;

    [SerializeField]
    private Rigidbody ballRb;
    private void Start()
    {
        oldPos = transform.position;
        StartCoroutine(Updating());
    }
    public void ResetBallState()
    {
        oldPos = Vector3.zero;
        velocity = Vector3.zero;
        owner = null;
        Vector3 loc = Vector3.zero;
        loc.y = 1;
        transform.position = loc;
    }

    public float Speed()
    {
        return velocity.magnitude;
    }

    public new Vector3 Velocity()
    {
        return velocity;
    }

    private void Awake()
    {
        ballRb = GetComponent<Rigidbody>();
        transform.position = new Vector3(0, 1, 0);
    }

    public IEnumerator Updating()
    {
        while (true)
        {
            oldPos = transform.position;

            velocity += (velocity).normalized * -0.03f;

            transform.position = (Vector3)transform.position + velocity;

            v_heading = velocity.normalized;

            if (transform.position.x < -24 || transform.position.x > 24 || transform.position.z > 10 || transform.position.z < -10)
            {
                velocity = new Vector3(0, 0, 0);
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    public bool HandleMessage(Telegram_CH4 msg)
    {
        return false;
    }

    public void Kick(Vector3 direction, float force)
    {
        velocity = new Vector3(0, 0, 0);
        direction.Normalize();

        Vector3 acc = (direction * force) / ballRb.mass;

        velocity = acc;
    }

    public float TimeToCoverDistance(Vector3 from, Vector3 to, float force)
    {
        float speed = force / ballRb.mass;

        float distanceToCover = Vector3.Distance(from, to);

        float term = speed * speed + 2f * distanceToCover * ballRb.drag;

        if (term <= 0) return 0f;

        float v = Mathf.Sqrt(term);

        return (v - speed) / ballRb.drag;
    }

    public Vector3 FuturePosition(float time)
    {
        // Use Expression : x = ut  + 1/2at^2;

        Vector3 ut = v_velocity * time;

        float half_a_t_squared = 0.5f * ballRb.drag * time * time;

        Vector3 scalarToVector = half_a_t_squared * v_velocity.normalized;

        return (Vector3)transform.position + ut + scalarToVector;
    }

    public void Trap(GameObject owner)
    {
        v_velocity = new Vector3(0,0,0);
        this.owner = owner;
    }

    public void SetOwner(GameObject ow)
    {
        owner = ow;
    }

    public GameObject GetOwner()
    {
        return owner;
    }

    public Vector3 OldPos()
    {
        return oldPos;
    }

    public void PlaceAtPosition(Vector3 newPos)
    {
        transform.position = newPos;
    }
}
