using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayer : MonoBehaviour
{
    public static float speed;
    public Rigidbody2D rb;  
    public GameObject bullet;
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
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
}
