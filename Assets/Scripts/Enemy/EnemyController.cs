using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public enum EnemyState
{
    Idle,
    Wander,
    Follow,
    Die,
    Attack
}   

public class EnemyController : MonoBehaviour
{

    GameObject player;

    public EnemyState currState = EnemyState.Idle;

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
    public Color defaultColor;
    public bool notInRoom = false;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sr = player.GetComponentInChildren<SpriteRenderer>();
        defaultColor = Color.white;
    }
    
    void Update()
    {
        switch (currState)
        {
            case EnemyState.Wander:
                Wander();
                break;
            case EnemyState.Follow:
                Follow();
                break;
            case EnemyState.Die:
                Death();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            default:
                break;
        }

        if (!notInRoom)
        {
            if (IsPlayerInRange(range) && currState != EnemyState.Die)
            {
                currState = EnemyState.Follow;
            }
            else if (!IsPlayerInRange(range) && currState != EnemyState.Die)
            {
                currState = EnemyState.Wander;
            }
            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
            {
                currState = EnemyState.Attack;
            }
        }
        else
        {
            currState = EnemyState.Idle;
        }
        
    }

    private bool IsPlayerInRange(float range)
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
            currState = EnemyState.Follow;
        }
    }

    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void Death()
    {
        PlayerController.enemiesKilled++;
        RoomController.instance.StartCoroutine(RoomController.instance.RoomCoroutine());
        Destroy(gameObject);
    }

    void Attack()
    {
        
        if (!cooldownAttack)
        {
            GameController.DamagePlayer(1);
            StartCoroutine(Cooldown());
            StartCoroutine(ChangeColor());
            if (GameController.Health == 0)
            {
                player.SetActive(false);

            }
        }
    }
    

    private IEnumerator Cooldown()
    {
        cooldownAttack = true;
        yield return new WaitForSeconds(cooldown);
        cooldownAttack = false;
    }

    private IEnumerator ChangeColor()
    {
        changedColor = true;
        sr.material.color = Color.red;
        yield return new WaitForSeconds(backToOrigin);
        sr.material.color = defaultColor;
        changedColor = false;
    }
}
