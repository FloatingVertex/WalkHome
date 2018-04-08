using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : EventTrigger
{
    [SerializeField]
    private string layerTag;

    [SerializeField]
    [TextArea(3,12)]
    private string line = "Use this box to make sure a line looks right before you put it into the Script.";

    [SerializeField]
    private List<Message> script = new List<Message>();

    [SerializeField]
    private EventTrigger hasEvent;
    UIManager manager;
	// Use this for initialization
	void Start ()
    {
        manager = GameObject.Find("EventSystem").GetComponent<UIManager>();
	}

    public override void RunTrigger()
    {
        foreach (Message msg in script)
        {
            manager.CreateMessage(msg);
        }
        CheckpointManager.GetManager().RegisterObject(this);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(layerTag))
        {
            RunTrigger();
        }
    }
}