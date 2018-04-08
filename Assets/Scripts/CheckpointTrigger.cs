using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : EventTrigger
{
    [SerializeField]
    private bool killPlayer = false;
    private CheckpointManager cpManager;
    [SerializeField]
    private EventTrigger nextEvent;
	// Use this for initialization
	void Start ()
    {
        cpManager = GameObject.Find("EventSystem").GetComponent<CheckpointManager>();
	}

    public override void RunTrigger()
    {
        if (killPlayer)
        {
            cpManager.ResetToCheckPoint();
        }
        else
        {
            cpManager.RegisterCheckpoint();
        }
        if(nextEvent != null)
        {
            nextEvent.RunTrigger();
        }
    }
}
