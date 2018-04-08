using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScript : MonoBehaviour
{
    [SerializeField]
    private string sceneName = "";
	
    public void StartGame()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
