using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField]
    private Vector3 leftPost;
    [SerializeField]
    private Vector3 rightPost;

    public Vector3 facing;
    // the center of the goal line.
    [SerializeField]
    private Vector3 center;
    // It is increased when function Scored() is called.
    [SerializeField]
    private int numGoalsScored;
    
    private void Awake()
    {
        leftPost = new Vector3(transform.position.x , transform.position.y, transform.position.z + 2.57f);
        rightPost = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2.57f);
        center = (leftPost + rightPost) / 2;
        numGoalsScored = 0;
    }

    public Vector3 Center(){
        return center;
    }

    public Vector3 LeftPost(){
        return leftPost;
    }

    public Vector3 RightPost(){
        return rightPost;
    }

    public Vector3 Facing()
    {
        return facing;
    }
}
