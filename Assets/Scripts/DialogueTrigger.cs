using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    [TextArea]
    private string line = "Test!";

    [SerializeField]
    List<Message> msg = new List<Message>();
    UIManager manager;
	// Use this for initialization
	void Start ()
    {
        manager = GameObject.Find("EventSystem").GetComponent<UIManager>();
        msg.Add(new Message(line));
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            foreach(Message tmp in msg)
            {
                manager.CreateMessage(tmp);
            }
            gameObject.SetActive(false);
        }
    }
}