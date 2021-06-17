using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeUpPlayers : MonoBehaviour
{
    // get list of players for each team
    public GameObject[] redTeam;
    public GameObject[] blueTeam;
    public float minDistance = 1.5f;


    // check if multiple members of a team have the same distance to the ball

    // send one of the teammembers back home


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        breakup(redTeam);
        breakup(blueTeam);
    }

    private void breakup(GameObject[] players){
        List<GameObject> pastChildren = new List<GameObject>();
        foreach(GameObject player in players){
            FieldPlayer fp = player.GetComponent<FieldPlayer>();
            pastChildren.Add(player);
            foreach(GameObject otherplayer in players){
                float distance = Vector3.Distance(player.transform.position ,otherplayer.transform.position);
                Vector3 otherPb = otherplayer.GetComponent<PlayerBase>().HomeRegion();
                if(!pastChildren.Contains(otherplayer) && distance <= minDistance){
                    // player not home
                    FieldPlayer ofp = otherplayer.GetComponent<FieldPlayer>();
                    if((ofp.Player || CoinToss() || isEqual(Vector3.Distance(otherPb, otherplayer.transform.position), 0f)) && !fp.Player){
                        player.GetComponent<PlayerBase>().warpHome();
                        // player back to home
                    }else{
                        otherplayer.GetComponent<PlayerBase>().warpHome();
                        // otherplayer back to home
                    }
                }
            }
        }
    }

    private bool CoinToss(){
        int randValue = Random.Range(1,101);
        if(randValue<=50){
            return true;
        } else {
            return false;
        }
    }

    bool isEqual(float a, float b)
    {
        if (a >= b - Mathf.Epsilon && a <= b + Mathf.Epsilon)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
