using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportSpotCalc : MonoBehaviour
{
    public static SupportSpotCalc instance = null;

    public List<SupportSpot> blueZone;
    public List<SupportSpot> redZone;
    public Vector3 bestSupportSpot;
    public int frame;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start() {
        var group = GameObject.Find("BlueZone").GetComponentsInChildren<SupportSpot>();
        if(group != null)
        {
            redZone = new List<SupportSpot>(group);
        }

        bestSupportSpot = new Vector3(0,0,0);
    }

    public Vector3 GetBestSupportingSpot(TeamColor team){
        if(bestSupportSpot != Vector3.zero){
            return bestSupportSpot;
        } else return DetermineBestSupportingPosition(team);
    }

    public Vector3 DetermineBestSupportingPosition(TeamColor team){
        bestSupportSpot = new Vector3(0,0,0);
        float bestScoreSoFar = 0;

        List<SupportSpot> list = team == TeamColor.Red ? redZone : blueZone;
        GameObject teamName = team == TeamColor.Red ? GameObject.Find("RedTeam").gameObject : GameObject.Find("BlueTeam").gameObject;

        foreach(SupportSpot item in list){
            item.SetScore(1f);
            if (teamName.GetComponent<SoccerTeam>().ControllingPlayer() != null)
            {
                if (teamName.GetComponent<SoccerTeam>().IsPassSafeFromAllOpponents(teamName.GetComponent<SoccerTeam>().ControllingPlayer().transform.position, item.transform.position, null, Prm.instance.MaxPassingForce))
                {
                    item.SetScore(item.GetScore() + Prm.instance.Spot_PassSafeStrength);
                }
                Vector3 tmp = new Vector3();
                if (teamName.GetComponent<SoccerTeam>().CanShoot(item.transform.position, Prm.instance.MaxShootingForce, ref tmp))
                {
                    item.SetScore(item.GetScore() + Prm.instance.Spot_CanScoreStrength);
                }
                
                if (teamName.GetComponent<SoccerTeam>().SupportingPlayer())
                {
                    const float optimalDistance = 5f;
                    float dist = Vector2.Distance(teamName.GetComponent<SoccerTeam>().ControllingPlayer().transform.position, item.transform.position);
                    float temp = Mathf.Abs(optimalDistance - dist);

                    if(temp< optimalDistance){
                        item.SetScore(item.GetScore() + Prm.instance.Spot_DistFromControllingPlayerStrength * (optimalDistance - temp) / optimalDistance);
                    }
                }

            }

            if(item.GetScore() > bestScoreSoFar){
                bestScoreSoFar = item.GetScore();
                bestSupportSpot = item.transform.position;
            }
        }
        return bestSupportSpot;
    }
}
