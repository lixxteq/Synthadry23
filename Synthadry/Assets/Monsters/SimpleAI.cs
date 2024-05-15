using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using System;

public class SimpleAI : MonoBehaviour
{
    public int damageValue = 10, attackSpeed = 1;
    public float randomDestinationMin = -10f, randomDestinationMax = -10f;
    public int walkSpeed = 12;
    public int patrolSpeed = 5;

    private NavMeshAgent Enemy;
    private GameObject Player;
    private Animator anim;
    private float timer = 0;
    private float timerForNextPositionPatroling;
    public float seePlayer;

    private Vector3 targetRandomPostition;


    // Start is called before the first frame update
    void Start()
    {
        Enemy = gameObject.GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float DistanceToPlayer = Vector3.Distance(Player.transform.position, gameObject.transform.position);
        if (DistanceToPlayer < seePlayer)
        {
            Enemy.speed = walkSpeed;
            anim.SetBool("IsPatrolling", false);

            if (DistanceToPlayer < 8)
            {
                if (DistanceToPlayer < 5)
                {
                    Enemy.SetDestination(Enemy.transform.position);
                    //anim.SetBool("isMove", false);
                }
                damage(DistanceToPlayer);
            }
            else
            {
                anim.SetBool("isAttack", false);
                Enemy.SetDestination(Player.transform.position);
                anim.SetBool("isMove", true);
            }
        }
        else if (DistanceToPlayer < seePlayer * 2)
        {
            Patrolling();
        } else
        {
            Enemy.SetDestination(Enemy.transform.position);
            anim.SetBool("isAttack", false);
            anim.SetBool("isMove", false);
            anim.SetBool("IsPatrolling", false);
        }
    }

    void Patrolling()
    {
        if (timerForNextPositionPatroling <= 0 || Vector3.Distance(Enemy.transform.position, targetRandomPostition) <= 2)
        {
            anim.SetBool("isMove", false);
            Enemy.speed = patrolSpeed;
            anim.SetBool("IsPatrolling", true);
            targetRandomPostition = Enemy.transform.position + new Vector3(UnityEngine.Random.Range(randomDestinationMin, randomDestinationMax), 0, UnityEngine.Random.Range(randomDestinationMin, randomDestinationMax));
            Enemy.SetDestination(targetRandomPostition);
            timerForNextPositionPatroling = UnityEngine.Random.Range(3, 6);
        }
        else
        {
            UnityEngine.Debug.Log(Enemy.velocity);
            timerForNextPositionPatroling -= Time.deltaTime;
        }
    }   

    void damage(float DistanceToPlayer)
    {
        while (DistanceToPlayer < 8)
        {
            anim.SetBool("isAttack", true);
            timer += Time.deltaTime;
            if (timer < 1 / attackSpeed) return;

            timer = 0;

            anim.SetBool("isAttack", false);
        }
    }
}
