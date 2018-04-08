using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagTrigger : EventTrigger
{
    [SerializeField]
    private string flagId;
    [SerializeField]
    private bool setToValue;

    public override void RunTrigger()
    {
        if (setToValue != GameplayFlags.GetManager().GetFlag(flagId))
        {
            GameplayFlags.GetManager().ToggleFlag(flagId);
        }
    }
}