using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

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
        yesButton.onClick.AddListener(Hide);
        noButton.onClick.AddListener(noTrigger.RunTrigger);
        noButton.onClick.AddListener(Hide);
    }

    void Hide()
    {
        menu.SetActive(false);
    }
}