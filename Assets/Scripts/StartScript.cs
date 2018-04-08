using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScript : MonoBehaviour
{
    [SerializeField]
    private int sceneId = 1;
    [SerializeField]
    private int sceneId2 = 2;
	
    public void StartGame()
    {
        SceneManager.LoadScene(sceneId, LoadSceneMode.Single);
        SceneManager.LoadScene(sceneId2, LoadSceneMode.Additive);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
