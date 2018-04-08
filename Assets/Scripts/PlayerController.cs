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

    private bool onJumpableService = false;

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
        GetComponent<Character>().jump = (Input.GetKey(jumpKey) && onJumpableService);
        if((Input.GetKey(jumpKey) && onJumpableService)) { onJumpableService = false; }
        GetComponent<Character>().Movement = movement;
        GetComponent<Character>().ClimbState = climbing;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach(var cp in collision.contacts)
        {
            if(Mathf.Abs(Vector2.Dot(cp.normal,Vector2.up)) > 0.9f)
            {
                onJumpableService = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onJumpableService = false;
    }
}
