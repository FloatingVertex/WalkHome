using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : EventTrigger
{
    [SerializeField]
    private bool isKillBox = false;
    private CheckpointManager cpManager;
	// Use this for initialization
	void Start ()
    {
        cpManager = GameObject.Find("EventSystem").GetComponent<CheckpointManager>();
	}

    public override void RunTrigger()
    {
        if (isKillBox)
        {
            cpManager.ResetToCheckPoint();
        }
        else
        {
            cpManager.RegisterCheckpoint();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            RunTrigger();
        }
    }

}
