using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject player;
    //Rigidbody2D rb;

    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        this.transform.position = player.transform.position;
        //rb.gravityScale = 0;
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        mouse.Normalize();
        float angle = Mathf.Atan2(mouse.y, mouse.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        //rb.rotation = angle;
    }
}

