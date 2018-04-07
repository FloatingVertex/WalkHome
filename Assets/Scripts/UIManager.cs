using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Message struct for use with the UIManager
struct Message
{
    string text;
    bool requiresConfirmation;
    public Message(string txt, bool usesInput)
    {
        text = txt;
        requiresConfirmation = usesInput;
    }
}

public class UIManager : MonoBehaviour
{


    private bool displayingMessage = false;
    private string currentMessage = "The FitnessGram Pacer Test is a multistage aerobic capacity test that gets progressively more difficult as it continues.";
    private Queue<Message> msgQueue;
    private bool waitingForInput = false; // whether a message is displayed and we're waiting on the player to continue
    private Text displayBox = null;
	// Use this for initialization
	void Start ()
    {
        displayBox = GameObject.Find("MsgText").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void SendMessage(Message msg)
    {
        
    }
}
