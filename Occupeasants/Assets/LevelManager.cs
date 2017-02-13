using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuiteGame()
    {
        Debug.Log("Game stopped");
        Application.Quit();
    }
}
