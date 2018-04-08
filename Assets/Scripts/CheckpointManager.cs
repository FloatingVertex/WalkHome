using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private List<GameObject> objectsToReset = new List<GameObject>();

    private Vector2 currentCheckpoint;
    private GameObject player;
    private GameObject kid;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");
        kid = GameObject.Find("Child");
	}

    public void ResetToCheckPoint()
    {
        player.transform.position = currentCheckpoint;
        foreach(GameObject obj in objectsToReset)
        {
            obj.SetActive(!obj.activeInHierarchy);
        }
    }

    public void RegisterObject(EventTrigger trg)
    {
        objectsToReset.Add(trg.gameObject);
    }

    // TODO: Set up a CP Trigger script to call this method, attach it to the checkpoint
    public void RegisterCheckpoint()
    {
        currentCheckpoint = player.transform.position;
        objectsToReset = new List<GameObject>(); // clear the list of objs to reset
    }

    public static CheckpointManager GetManager()
    {
        return GameObject.Find("EventSystem").GetComponent<CheckpointManager>();
    }
}
