using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInput : MonoBehaviour
{
    public CylinderController cylinderController;
    public FieldPlayer player;

    private void Update()
    {
        if(Input.GetKeyUp("e")){
            player.TogglePassLock();
        } else if(Input.GetKeyUp("r")){
            var team = GameObject.Find("RedTeam").GetComponent<SoccerTeam>();
            team.Reload();
            team.opponentTeam.Reload();
        }
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, player.transform.position.y, moveVertical);

        cylinderController.Move(movement * Time.deltaTime);
    }
}

