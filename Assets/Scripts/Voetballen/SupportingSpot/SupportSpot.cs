using UnityEngine;

public class SupportSpot : MonoBehaviour
{
    [SerializeField]
    private Vector3 pos;
    [SerializeField]
    private float score;

    private void Awake() {
        pos = transform.position;
        score = 0f;
    }

    public float GetScore(){
        return score;
    }

    public void SetScore(float value){
        score = value;
    }
}
