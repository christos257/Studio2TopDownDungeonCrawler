using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    Boss boss;
    public static bool gamePaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameUI;
    public GameObject questMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
         }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        gameUI.SetActive(true);
        questMenuUI.SetActive(true);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        gameUI.SetActive(false);
        questMenuUI.SetActive(false);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        PlayerController.enemiesKilled = 0;
        PlayerController.pickUpAmount = 0;
        PlayerController.roomsCompleted = 0;
        PlayerController.speed = GameController.MoveSpeed;
        GameController.BossHealth = GameController.BossMaxHealth;
        GameController.Health = GameController.MaxHealth;
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
