using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiVer2 : MonoBehaviour,IDamagable
{
    private Rigidbody2D rb2d;
    private GameObject player;
    private Animator animator;
    public EnemyData enemyBrain;
    [SerializeField]
    private EnemyAiVer2 enemyAi;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    public AudioClip[] DeathSounds;
    #region attacking
    public bool canAttack;
    float timeUntileAttack = 1f;
    float attackTimer;

    public bool agressive;
    float timeUntileAgrresive = 2;
    float aggresivetimer;

    bool isgetingDirection;
    bool pursue;
    bool isAlive;

   public Vector2 directionToPlayer;



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
        spriteRenderer= GetComponent<SpriteRenderer>();
        isgetingDirection = true;

    }

    private void Update()
    {
            enemyBrain.CheckiFCanPersue(enemyAi);
     
        if(isgetingDirection)
        {
            directionToPlayer = player.transform.position - transform.position ;
            directionToPlayer.Normalize();

        }

        

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
                if (Vector2.Distance(transform.position, player.transform.position) > 5)
                {
                    MoveToTarget(10, directionToPlayer);
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
        if(angle >= 135 || angle <= -135)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
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
        if(attackTimer < 0.6f)
        {
            spriteRenderer.color = Color.red;
        }
        if(attackTimer < 0.5f)
        {
            isgetingDirection= false;
        }

    }
  
    public void DisableAttackAnimation()
    {
        canAttack = false;
        agressive = false;
        attackTimer = timeUntileAttack;
        aggresivetimer = timeUntileAttack;
        animator.SetBool("Kick", false);
        spriteRenderer.color = Color.white;
        isgetingDirection = true;

    }
    public void AddforceToDirection()
    {
        rb2d.velocity = directionToPlayer * 50;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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

    public void TakeDamage()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
    }
}
