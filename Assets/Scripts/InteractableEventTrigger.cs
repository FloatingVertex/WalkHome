using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableEventTrigger : EventTrigger {

    public float energyChange = 0;
    public int happynessChange = 0;
    public string flag;
    private GameplayFlags flagsGo;

    private void Start()
    {
        flagsGo = GameplayFlags.GetManager();
    }

    public override void RunTrigger()
    {
        if (flagsGo.Contains(flag) && !flagsGo.GetFlag(flag))
        {
            HappinessManager.EventHappened(happynessChange, energyChange);
            flagsGo.ToggleFlag(flag);
        }
    }
}
