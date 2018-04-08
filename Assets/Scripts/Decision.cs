using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Decision : EventTrigger
{
    [SerializeField]
    private string prompt = "";

    [SerializeField]
    private string flagName = "None";
    
    private GameObject menu;
    private Text promptLabel;
    private Button yesButton;
    private Button noButton;

    [SerializeField]
    private EventTrigger yesTrigger;
    [SerializeField]
    private EventTrigger noTrigger;
	// Use this for initialization
	void Start ()
    {
        menu = GameObject.Find("MenuGroup");
        promptLabel = GameObject.Find("PromptText").GetComponent<Text>();
        yesButton = GameObject.Find("YesButton").GetComponent<Button>();
        noButton = GameObject.Find("NoButton").GetComponent<Button>();
    }

    public override void RunTrigger()
    {
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();
        menu.SetActive(true);
        promptLabel.text = prompt;
        yesButton.onClick.AddListener(yesTrigger.RunTrigger);
        noButton.onClick.AddListener(noTrigger.RunTrigger);
    }

    void Hide()
    {
        menu.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RunTrigger();
    }
}