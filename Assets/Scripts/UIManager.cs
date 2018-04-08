using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Message struct for use with the UIManager
[System.Serializable]
public struct Message
{
    [SerializeField]
    [TextArea(3, 12)]
    public string text;
    public bool hasEvent;
    public EventTrigger choice;
    public Message(string txt)
    {
        text = txt;
        hasEvent = false;
        choice = null;
    }
    public Message(string txt, EventTrigger chc)
    {
        text = txt;
        hasEvent = true;
        choice = chc;
    }
}

public class UIManager : MonoBehaviour
{

    private bool displayingMessage = true;
    public Message currentMsg = new Message("Test");

    // Fields used to display the current message
    private string currentDisplayed = "";
    private int currentLength = -1;
    private int typewriteIterator = 0;
    private float writeTimer = 0;
    [SerializeField]
    private float timeToNextChar = .5f;

    private Queue<Message> msgQueue = new Queue<Message>();
    private bool waitingForInput = false; // whether a message is displayed and we're waiting on the player to continue
    private Text displayBox;
    private GameObject panel;
	// Use this for initialization
	void Start ()
    {
        displayBox = GameObject.Find("MsgText").GetComponent<Text>();
        panel = GameObject.Find("MessagePanel");
        panel.SetActive(false);
        bool active = panel.activeInHierarchy;
        IsDisplaying = false;
        GameObject.Find("MenuGroup").SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        // when the current message has finished printing, wait for user input before continuing
        if (waitingForInput)
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (msgQueue.Count > 0)
                {
                    if (currentMsg.hasEvent && !(currentMsg.choice is Decision))
                    {
                        currentMsg.choice.RunTrigger();
                    }
                    currentMsg = msgQueue.Dequeue();
                    MessageSetup(currentMsg);
                }
                else
                {
                    IsDisplaying = false;
                    if (currentMsg.hasEvent)
                    {
                        currentMsg.choice.RunTrigger();
                    }
                }
                waitingForInput = false;
            }
        }
        // if not waiting for input but still displaying, typewriter-print the current message out
        else if (displayingMessage)
        {
            writeTimer += Time.deltaTime;
            if(writeTimer >= timeToNextChar)
            {
                currentDisplayed += currentMsg.text[typewriteIterator++];
                displayBox.text = currentDisplayed;
                writeTimer = 0;
                // if we hit the end of the message
                if (typewriteIterator == currentLength)
                {
                    waitingForInput = true;
                }
            }
        }

        // if no message is displayed but the queue has at least one in it, display the first one
		if (msgQueue.Count > 0 && !displayingMessage && !waitingForInput)
        {
            currentMsg = msgQueue.Dequeue();
            MessageSetup(currentMsg);
            IsDisplaying = true;
        }
	}

    public void CreateMessage(Message msg)
    {
        // if the message is too long, split it up
        if (msg.text.Length > 125)
        {
            // variables to track how much of the string's total length has been allocated into different messages, as well as iteration count for limiting purposes
            int totalLength = msg.text.Length;
            int allocated = 0;
            int iterations = 0;

            // Loop through until the whole message has been taken care of
            while (allocated < totalLength && iterations < 50)
            {
                // grab the max chars allowable as a chunk
                string tempStr = msg.text.Substring(allocated, Mathf.Min(125, (totalLength - allocated)));

                if (tempStr.Length < 125)
                {
                    Message addMessage = new Message(tempStr);
                    msgQueue.Enqueue(addMessage);
                    allocated += addMessage.text.Length;
                }
                else
                {
                    // Split up into individual words, and cut the last one off for the next message to be short enough
                    string[] words = tempStr.Split(' ');

                    // find the overflow point
                    int i = -1;
                    int charCount = 0;
                    do
                    {
                        i++;
                        charCount += words[i].Length + 1; // extra 1 accounts for the space
                    }
                    while (charCount < 125);
                    int indexOfNext;
                    if (i == words.Length - 1)
                    {
                        indexOfNext = tempStr.IndexOf(words[i]);
                    }
                    else
                    {
                        indexOfNext = tempStr.IndexOf(words[i] + " ");
                    }
                    Message addMessage = new Message(tempStr.Substring(0, indexOfNext - 1));
                    msgQueue.Enqueue(addMessage);

                    allocated += indexOfNext;
                    iterations++;
                }

            }
        }

        // if the message is shorter than 100 characters
        else
        {
            msgQueue.Enqueue(msg);
        }
    }
    void MessageSetup(Message msg)
    {
        currentLength = currentMsg.text.Length;
        currentDisplayed = "";
        displayBox.text = "";
        typewriteIterator = 0;
        writeTimer = 0;
    }

    public bool IsDisplaying
    {
        get { return displayingMessage; }
        set
        {
            bool temp = displayingMessage;
            displayingMessage = value;
            if (displayingMessage != temp)
            {
                panel.SetActive(displayingMessage);
            }
        }
    }
}
