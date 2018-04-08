using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayerForwards : EventTrigger {

    public Vector3 offset;

    public override void RunTrigger()
    {
        var player = GameObject.Find("Player");
        var kid = GameObject.Find("Child (1)");

        player.transform.position = player.transform.position + offset;
        kid.transform.position = kid.transform.position + offset;
    }
}
