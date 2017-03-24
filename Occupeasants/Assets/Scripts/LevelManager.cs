﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuiteGame()
    {
        Debug.Log("Game stopped");
        Application.Quit();
    }

    public void StartScene()
    {
        SceneManager.LoadScene(2);
    }

    //Button that shows when player loses, restarts current level
    public void TryAgain()
    {
        SceneManager.LoadScene(1);
    }

}