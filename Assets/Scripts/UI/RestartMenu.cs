using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenu : MonoBehaviour
{
    
    public GameObject restartUI;
    public GameObject pauseUI;
    public GameObject gameUI;
    public GameObject questUI;
    Scene scene;
    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.FindGameObjectWithTag("Player"))
        {
            restartUI.SetActive(true);
        }
    }
    
    
    public void Restart()
    {
        
        SceneManager.LoadScene(scene.name);
        restartUI.SetActive(false);
        PlayerController.enemiesKilled = 0;
        PlayerController.pickUpAmount = 0;
        PlayerController.roomsCompleted = 0;
        PlayerController.speed = GameController.MoveSpeed;
        GameController.BossHealth = GameController.BossMaxHealth;
        GameController.Health = GameController.MaxHealth;
        Time.timeScale = 1f;
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
}
