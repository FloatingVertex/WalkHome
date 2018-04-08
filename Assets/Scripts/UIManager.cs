using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Message struct for use with the UIManager
public struct Message
{
    public string text;
    public bool requiresConfirmation;
    public Message(string txt)
    {
        text = txt;
        requiresConfirmation = true;
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
                    currentMsg = msgQueue.Dequeue();
                    MessageSetup(currentMsg);
                }
                else
                {
                    IsDisplaying = false;
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
        if (msg.text.Length > 120)
        {
            int totalLength = msg.text.Length;
            int allocated = 0;
            int iterations = 0;
            while (allocated < totalLength && iterations < 50)
            {
                // grab the max chars allowable as a chunk
                string tempStr = msg.text.Substring(allocated, Mathf.Min(125, (totalLength - allocated)));

                // Split up into individual words, and cut the last one off for the next message
                if (tempStr.Length < 125)
                {
                    Message addMessage = new Message(tempStr);
                    msgQueue.Enqueue(addMessage);
                    allocated += addMessage.text.Length;
                }
                else
                {
                    string[] words = tempStr.Split(' ');
                    int indexOfNext;

                    indexOfNext = tempStr.IndexOf(words[words.Length - 2]);

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
