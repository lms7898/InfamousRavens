using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	
	public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuiteGame()
    {
        Debug.Log("Game stopped");
        Application.Quit();
    }

    //Button that shows when player loses, restarts current level
    public void TryAgain()
    {
        if(SceneManager.GetActiveScene().name == "scene1")
        {
            SceneManager.LoadScene(1);
        }
    }
    
}
