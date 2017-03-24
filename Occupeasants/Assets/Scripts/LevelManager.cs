using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	
	public void StartGame()
    {
        SceneManager.LoadSceneAsync("scene1");
    }

    public void QuiteGame()
    {
        Debug.Log("Game stopped");
        Application.Quit();
    }

    //Button that shows when player loses, restarts current level
    public void TryAgain()
    {
        SceneManager.LoadSceneAsync("scene1");
    }
    
}
