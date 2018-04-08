using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private string layerTag;

    [SerializeField]
    [TextArea(3,12)]
    private string line = "Test!";

    private Message msg;

    UIManager manager;
	// Use this for initialization
	void Start ()
    {
        manager = GameObject.Find("EventSystem").GetComponent<UIManager>();
        msg = new Message(line);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(layerTag))
        {
                manager.CreateMessage(msg);
        }
    }
}