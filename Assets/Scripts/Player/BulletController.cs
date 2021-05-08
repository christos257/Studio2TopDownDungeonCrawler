using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    PlayerController player;
    GameObject boss;
    public float lifeTime;
    

    void Start()
    {
        player = new PlayerController();
        StartCoroutine(DeathDelay());
    }


    void Update()
    {

    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().Death();
            Destroy(gameObject);
        }

        if (collision.tag == "Boss")
        {
            
            GameController.DamageBoss(1);
            Destroy(gameObject);
            if (GameController.BossHealth == 0)
            {
                collision.gameObject.GetComponent<Boss>().Death();
            }
        }

    }
    

    

}
