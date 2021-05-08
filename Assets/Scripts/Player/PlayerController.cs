using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static float speed;
    public Rigidbody2D rb;
    Room room;
    public Text pickUpText;
    public Text enemiesKilledText;
    public Text roomsCompletedText;
    public static int pickUpAmount = 0;
    public static int enemiesKilled = 0;
    public static int roomsCompleted = 0;
    public static bool roomComplete = false;
    public GameObject bullet;
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        PlayerMoving();

        fireDelay = GameController.FireRate;
        speed = GameController.MoveSpeed;
        float shootHorizontal = Input.GetAxis("ShootHorizontal");
        float shootVertical = Input.GetAxis("ShootVertical");
        if ((shootHorizontal != 0 || shootVertical != 0) && Time.time > lastFire + fireDelay)
        {
            Shoot(shootHorizontal, shootVertical);
            lastFire = Time.time;
        }
        pickUpText.text = "Items Collected: " + pickUpAmount;
        enemiesKilledText.text = "Enemies Killed: " + enemiesKilled;
        roomsCompletedText.text = "Rooms Completed: " + roomsCompleted;
    }

    public void PlayerMoving()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        rb.velocity = new Vector3(horizontal * speed, vertical * speed, 0);
    }

    void Shoot(float x, float y)
    {
        GameObject bul = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        bul.AddComponent<Rigidbody2D>().gravityScale = 0;
        bul.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
            0
            );
    }

    void Death()
    {
        if (GameController.Health == 0)
        {
            Destroy(gameObject);
        }
    }

   


}
