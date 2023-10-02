using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;


public class ForestBossAi : MonoBehaviour
{
    public int damageValue = 10, attackSpeed = 1;
    private NavMeshAgent Enemy;
    public GameObject Player;
    private Animator anim;
    private HPAndArmor actionTarget;
    private float timer = 0;
    public float seePlayer;
    public bool defend = false;
    public Rig hands;
    public float lerpDefend;

    // Start is called before the first frame update
    void Start()
    {
        Enemy = gameObject.GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        actionTarget = Player.GetComponent<HPAndArmor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (defend)
        {
            hands.weight = 1;
        }
        else
        {
            hands.weight = 0;
        }
        float DistationPlayer = Vector3.Distance(Player.transform.position, gameObject.transform.position);
        if (DistationPlayer < seePlayer)
        {
            if (DistationPlayer < 8)
            {
                if (DistationPlayer < 5)
                {
                    Enemy.SetDestination(Enemy.transform.position);
                    //anim.SetBool("isMove", false);
                }
                damage(DistationPlayer);
            }
            else
            {
                anim.SetBool("isAttack", false);
                Enemy.SetDestination(Player.transform.position);
                anim.SetBool("isMove", true);
            }
        }
        else
        {
            Enemy.SetDestination(Enemy.transform.position);
            anim.SetBool("isMove", false);
        }
    }

    void damage(float DistationPlayer)
    {
        while (DistationPlayer < 8)
        {
            anim.SetBool("isAttack", true);
            timer += Time.deltaTime;
            if (timer < 1 / attackSpeed) return;

            timer = 0;

            anim.SetBool("isAttack", false);
        }
    }

    public void hitPlayer()
    {
        actionTarget.TakeDamage(damageValue);
    }
}
