using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField]
    private bool isKillBox = false;
    private Checkpoint cpManager;
	// Use this for initialization
	void Start ()
    {
        cpManager = GameObject.Find("EventSystem").GetComponent<Checkpoint>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isKillBox)
        {
            cpManager.ResetToCheckPoint();
        }
        else
        {
            cpManager.RegisterCheckpoint(collision.gameObject);
        }
    }
}
