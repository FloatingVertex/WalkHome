using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingDialogTrigger : EventTrigger {

    public EventTrigger event1;
    public EventTrigger event2;
    public EventTrigger event3;
    public EventTrigger event4;

    // Use this for initialization
    void Start () {
		
	}

    public override void RunTrigger()
    {
        var hs = HappinessManager.singleton.happyness;
        if(hs < 4)
        {
            event1.RunTrigger();
        }
        else if (hs >= 4 && hs < 7)
        {
            event2.RunTrigger();
        }else if (hs < 10)
        {
            event3.RunTrigger();
        }else
        {
            event4.RunTrigger();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
