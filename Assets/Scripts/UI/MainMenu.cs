using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //private GameObject tutorialCanvas;

    private void Start()
    {
        //tutorialCanvas = GameObject.Find("Tutorial Menu");
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        //tutorialCanvas.SetActive(true);
        //SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void YesTutorial()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(8);
    }

    public void NoTutorial()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}
