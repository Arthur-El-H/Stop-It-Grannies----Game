﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start_Manager : MonoBehaviour
{
    public Scene match;
    mainManager mainManager;

    public GameObject start1;

    private void Start()
    {
        mainManager = GameObject.Find("mainManager").GetComponent<mainManager>();
    }
    public void startMatch()
    {
        mainManager.lastScene = "Startscreen";
        mainManager.resetWins();
        SceneManager.LoadScene("InGame");
    }

    public void showRules()
    {
        mainManager.lastScene = "Startscreen";
        SceneManager.LoadScene("Rules");
    }

    public void openOptions()
    {
        mainManager.lastScene = "Startscreen";
        SceneManager.LoadScene("Options");
    }

    public void playKiss()
    {
        mainManager.playKiss();
    }

    public void closeGame()
    {
        Application.Quit();
    }
}
