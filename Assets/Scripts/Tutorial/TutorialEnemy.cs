using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TutEnemyState
{
    Idle,
    Wander,
    Follow,
    Die,
    Attack
}

public class TutorialEnemy : MonoBehaviour
{
    
    GameObject player;

    public TutEnemyState currState = TutEnemyState.Idle;

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
        player = GameObject.FindGameObjectWithTag("TutorialPlayer");
        sr = player.GetComponentInChildren<SpriteRenderer>();
        defaultColor = Color.white;
    }

    void Update()
    {
        switch (currState)
        {
            case TutEnemyState.Wander:
                Wander();
                break;
            case TutEnemyState.Follow:
                Follow();
                break;
            case TutEnemyState.Die:
                Death();
                break;
            case TutEnemyState.Attack:
                Attack();
                break;
            default:
                break;
        }

        if (!notInRoom)
        {
            if (IsPlayerInRange(range) && currState != TutEnemyState.Die)
            {
                currState = TutEnemyState.Follow;
            }
            else if (!IsPlayerInRange(range) && currState != TutEnemyState.Die)
            {
                currState = TutEnemyState.Wander;
            }
            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
            {
                currState = TutEnemyState.Attack;
            }
        }
        else
        {
            currState = TutEnemyState.Idle;
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
            currState = TutEnemyState.Follow;
        }
    }

    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void Death()
    {
        Destroy(gameObject);
        Debug.Log("dead");
    }

    void Attack()
    {

        if (!cooldownAttack)
        {
            GameController.DamagePlayer(1);
            StartCoroutine(Cooldown());
            StartCoroutine(ChangeColor());
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


