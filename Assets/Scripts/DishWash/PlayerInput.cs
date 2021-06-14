using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public SpongeController spongeController;

    // Update is called once per frame
    public void Update()
    {
        float MoveHorizontal = Input.GetAxis("Horizontal");
        float MoveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(MoveHorizontal, 0.0f, MoveVertical);

        spongeController.Move(movement * Time.deltaTime);

    }
}
