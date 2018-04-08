using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToCredits : EventTrigger
{
    public override void RunTrigger()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}