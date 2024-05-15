using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float health;
    [SerializeField] private float timeToDestroy = 5;

    private ragdollController ragdoll;

    private Hitmarker hitmarker_canvas;

    private void Awake()
    {
        health = gameObject.GetComponent<EnemyController>().enemyStat.health;
        hitmarker_canvas = GameObject.FindWithTag("MainCanvas").GetComponent<Hitmarker>();
    }

    public void GetDamage(float damage)
    {
        hitmarker_canvas.DrawHitmarker(damage);

        this.health = Mathf.Max(health - damage, 0);
        if (this.health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        /*ragdoll.ActivateRagdoll();*/
        //GetComponent<Animator>().enabled = false;
        //GetComponent<StandartAi>().enabled = false;
        Destroy(gameObject.transform.parent.gameObject, timeToDestroy);
        Destroy(gameObject, timeToDestroy);
    }
}
