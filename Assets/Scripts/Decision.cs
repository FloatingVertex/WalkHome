using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Decision : MonoBehaviour
{
    [SerializeField]
    private string prompt = "";

    [SerializeField]
    private MonoBehaviour yesOption;

    [SerializeField]
    private MonoBehaviour noOption;

    private GameObject menu;
    private Text promptLabel;
    private Button yesButton;
    private Button noButton;

	// Use this for initialization
	void Start ()
    {
        menu = GameObject.Find("MenuGroup");
        promptLabel = GameObject.Find("PromptText").GetComponent<Text>();
        yesButton = 
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
