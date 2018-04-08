using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private List<GameObject> objectsToReset = new List<GameObject>();

    private Vector2 currentCheckpoint;
    private GameObject player;
	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ResetToCheckPoint()
    {
        player.transform.position = currentCheckpoint;
        foreach(GameObject obj in objectsToReset)
        {
            obj.SetActive(!obj.activeInHierarchy);
        }
    }

    public void RegisterObject(GameObject obj)
    {
        objectsToReset.Add(obj);
    }

    // TODO: Set up a CP Trigger script to call this method, attach it to the checkpoint
    public void RegisterCheckpoint(GameObject plyr)
    {
        currentCheckpoint = plyr.transform.position;
        player = plyr;
        objectsToReset = new List<GameObject>(); // clear the list of objs to reset
    }
}
