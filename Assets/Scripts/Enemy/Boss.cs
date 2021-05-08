using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum BossState
{
    Idle,
    Wander,
    Follow,
    Attack,
    Die
}
public class Boss : MonoBehaviour
{
    public GameObject boss;
    GameObject player;
    public GameObject bossHealthBar;
    Room room;

    public BossState currState = BossState.Idle;
    
    public float range;
    public float speed;
    private bool chooseDirection = false;
    private bool dead = false;
    private Vector3 randomDirection;
    public float attackRange;
    private bool cooldownAttack = false;
    public float cooldown;
    private bool changedColor = false;
    public float backToOrigin;
    public SpriteRenderer sr;
    public SpriteRenderer srB;
    public Color defaultColor;
    public bool notInRoom;

    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        player = GameObject.FindGameObjectWithTag("Player");
        bossHealthBar = GameObject.FindGameObjectWithTag("HealthBar");
        sr = player.GetComponentInChildren<SpriteRenderer>();
        srB = boss.GetComponentInChildren<SpriteRenderer>();
        defaultColor = Color.white;
        bossHealthBar.SetActive(false);
        notInRoom = true;
    }

    void Update()
    {
        switch (currState)
        {
            case BossState.Wander:
                Wander();
                break;
            case BossState.Follow:
                Follow();
                break;
            case BossState.Die:
                Death();
                break;
            case BossState.Attack:
                Attack();
                break;
            default:
                break;
        }

        if (!notInRoom)
        {
            if (IsPlayerInRange(range) && currState != BossState.Die)
            {
                currState = BossState.Follow;
                bossHealthBar.SetActive(true);
            }
            else if (!IsPlayerInRange(range) && currState != BossState.Die)
            {
                currState = BossState.Wander;
            }
            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
            {
                currState = BossState.Attack;
            }
        }
        else
        {
            currState = BossState.Idle;
        }

    }

    public bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection()
    {
        chooseDirection = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        randomDirection = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randomDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        chooseDirection = false;
    }

    void Wander()
    {
        if (!chooseDirection)
        {
            StartCoroutine(ChooseDirection());
        }

        transform.position += -transform.right * speed * Time.deltaTime;
        if (IsPlayerInRange(range))
        {
            currState = BossState.Follow;
        }
    }

    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void Death()
    {
        //bossHealthBar.SetActive(false);
        Destroy(gameObject);
        SceneManager.LoadScene(7);

    }

    void Attack()
    {

        if (!cooldownAttack)
        {
            GameController.DamagePlayer(1);
            StartCoroutine(Cooldown());
            StartCoroutine(ChangeColorPlayer());
        }
    }


    private IEnumerator Cooldown()
    {
        cooldownAttack = true;
        yield return new WaitForSeconds(cooldown);
        cooldownAttack = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            StartCoroutine(ChangeColorBoss());
        }
    }

    private IEnumerator ChangeColorPlayer()
    {
        changedColor = true;
        sr.material.color = Color.red;
        yield return new WaitForSeconds(backToOrigin);
        sr.material.color = defaultColor;
        changedColor = false;
    }

    private IEnumerator ChangeColorBoss()
    {
        changedColor = true;
        srB.material.color = Color.red;
        yield return new WaitForSeconds(backToOrigin);
        srB.material.color = defaultColor;
        changedColor = false;
    }

    public void SpawnBoss(GameObject name, Vector3 pos, Room room)
    {
        GameObject obj = Instantiate(name, boss.transform);
        obj.GetComponent<Transform>().position = new Vector2(room.GetRoomCenter().x, room.GetRoomCenter().y);
    }
}
