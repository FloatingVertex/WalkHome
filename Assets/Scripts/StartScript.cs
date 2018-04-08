using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScript : MonoBehaviour
{
    [SerializeField]
    private int sceneId = 1;
	
    public void StartGame()
    {
        SceneManager.LoadScene(sceneId, LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
