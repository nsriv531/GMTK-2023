using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiVer2 : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private GameObject player;
    private Animator animator;
    public EnemyData enemyBrain;
    [SerializeField]
    private EnemyAiVer2 enemyAi;
    #region attacking
    public bool canAttack;
    float timeUntileAttack = 1.5f;
    float attackTimer;

    public bool agressive;
    float timeUntileAgrresive = 2;
    float aggresivetimer;


    bool pursue;
    bool isAlive;

    Vector2 directionToPlayer;



    #endregion
    public float vunrable = 0;

    private void Start()
    {
        attackTimer = timeUntileAttack;
        aggresivetimer= timeUntileAgrresive;
        agressive= false;
        canAttack= false;
        rb2d= GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemyAi = this;

    }

    private void Update()
    {
            enemyBrain.CheckiFCanPersue(enemyAi);
        directionToPlayer = player.transform.position - transform.position ;
        directionToPlayer.Normalize();

        if(pursue)
        {
            Attack();
        }
        else
        {

        }
    }
    private void FixedUpdate()
    {
    }
    
    public void Attack()
    {
        if( !agressive)
        {
            if(Vector2.Distance(transform.position, player.transform.position) > 8)
            {
               MoveToTarget(4,directionToPlayer);
                AgreasiveTimer();

            }
            else
            {
                MoveToTarget(0, directionToPlayer);

                AgreasiveTimer();
            }


        }
        else if(agressive)
        {
            if( !canAttack)
            {
                if (Vector2.Distance(transform.position, player.transform.position) > 8)
                {
                    MoveToTarget(7, directionToPlayer);
                    AttackTimer();

                }
                else
                {
                    MoveToTarget(0, directionToPlayer);
                    AttackTimer();

                }

            }
            else if (canAttack)
            {
                rb2d.velocity = Vector2.zero;
                animator.SetBool("Kick", true);
            }
        }
    }
    public void Defend()
    {

    }
    public void MoveToTarget(int speed, Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        animator.SetBool("Up", (angle >= 45 && angle <= 135));
        animator.SetBool("Down", (angle <= -45 && angle >= -135));
        animator.SetBool("Left", (angle >= 135 || angle <= -135));

        animator.SetBool("Right", (angle <= 45 && angle >= -45));
        //animator.SetBool("Kick", kick);
        rb2d.velocity = direction * speed;
    }
    public void EnablePursue()
    {
        pursue = true;
    }
    public void AgreasiveTimer()
    {
        aggresivetimer -= Time.deltaTime;
        if (aggresivetimer < 0)
        {
            agressive= true;
        }
    }
    public void AttackTimer()
    {
        attackTimer -= Time.deltaTime;
        if(attackTimer < 0)
        {
            canAttack= true;
        }

    }
  
    public void DisableAttackAnimation()
    {
        canAttack = false;
        agressive = false;
        attackTimer = timeUntileAttack;
        aggresivetimer = timeUntileAttack;
        animator.SetBool("Kick", false);
    }
    public void AddforceToDirection()
    {
        rb2d.AddForce(directionToPlayer * 1, ForceMode2D.Force);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            Debug.Log("kicked");
            player.GetComponent<PlayerBallSliding>().TakeDamage();
            player.GetComponent<Rigidbody2D>().velocity = player.GetComponent<Transform>().position - gameObject.transform.position * 1 * 3;
        }
        if (collision.gameObject.CompareTag("wall") && vunrable > 0)
        {
            Destroy(gameObject);
        }
    }
}
