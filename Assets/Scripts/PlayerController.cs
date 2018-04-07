using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class PlayerController : MonoBehaviour {

    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode climbUpKey = KeyCode.W;
    public KeyCode climbDownKey = KeyCode.S;
    public KeyCode jumpKey = KeyCode.Space;

    private void Update()
    {
        int movement = 0;
        if(Input.GetKey(leftKey))
        {
            movement += -1;
        }
        if(Input.GetKey(rightKey))
        {
            movement += 1;
        }
        int climbing = 0;
        if(Input.GetKey(climbUpKey))
        {
            climbing += 1;
        }
        if(Input.GetKey(climbDownKey))
        {
            climbing += -1;
        }
        GetComponent<Character>().jump = Input.GetKey(jumpKey);
        GetComponent<Character>().Movement = movement;
        GetComponent<Character>().ClimbState = climbing;
    }
}
