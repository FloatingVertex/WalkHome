using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class PlayerController : MonoBehaviour {

    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;

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
        GetComponent<Character>().Movement = movement;
    }
}
