using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static GameController instance;
    public static PlayerController player;
    public static Boss boss;

    private static float health = 6;
    private static float maxHealth = 6;
    private static float moveSpeed = 5;
    private static float fireRate = 0.5f;

    private static float bossHealth = 5;
    private static float bossMaxHealth = 5;
    
    public static float Health { get => health; set => health = value; }
    public static float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static float FireRate { get => fireRate; set => fireRate = value; }

    public static float BossHealth { get => bossHealth; set => bossHealth = value; }
    public static float BossMaxHealth { get => bossMaxHealth; set => bossMaxHealth = value; }

    public Text healthText;
    public Text bossHealthText;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        player = GetComponent<PlayerController>();
        boss = GetComponent<Boss>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene(7);
        }
    }
    

    public static void DamagePlayer(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;
        }
    }

    public static void DamageBoss(int damage)
    {
        bossHealth -= damage;
        if (bossHealth <= 0)
        {
            bossHealth = 0;
        }
    }

    public static void HealPlayer(float healAmount)
    {
        health = Mathf.Min(maxHealth, health + healAmount);
    }

    public static void MoveSpeedChange(float speed)
    {
        moveSpeed += speed;
    }
    

    public static void FireRateChange(float rate)
    {
        fireRate -= rate;
    }
    
    
}
