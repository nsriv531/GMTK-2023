using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemiesPlayersAI : MonoBehaviour,IDamagable
{

    public Rigidbody2D rb;

    public BoxCollider2D bC1;
    public BoxCollider2D bC2;

    public float xv;
    public float yv;

    public float speed;

    public GameObject ball;
    public GameObject ballColider;
    private float distance;
    public Animator anime;
    public bool kick = false;

    public float vunrable = 0;

    public GameObject ui;
    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Player");
        ballColider = GameObject.FindGameObjectWithTag("PlayerColider");
        ui = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        if (vunrable <= 0)
        {
            distance = Vector2.Distance(transform.position, ball.transform.position);
        Vector2 direction = ball.transform.position - transform.position;
        rb.velocity = direction.normalized * speed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Debug.Log(angle);
         
        if (vunrable > 0)
        {
            vunrable -= Time.deltaTime;
        }

        anime.SetBool("Up", (angle >= 45 && angle <= 135));
        anime.SetBool("Down", (angle <= -45 && angle >= -135));
        anime.SetBool("Left", (angle >= 135 || angle <= -135));
        anime.SetBool("Right", (angle <= 45 && angle >= -45));
        anime.SetBool("Kick", kick);
        }
    }

    public void TakeDamage()
    {
        ///takes damge
        ui.GetComponent<GameUI>().score++;
        /*Destroy(gameObject);*/
        //Debug.Log("im Hit!!!");
        vunrable = 3;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
          
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == ball) {
            Debug.Log("kicked");
            kick = true;
            ball.GetComponent<PlayerBallSliding>().TakeDamage();
            ball.GetComponent<Rigidbody2D>().velocity = ball.GetComponent<Transform>().position - gameObject.transform.position * speed * 3;
        }
        if (collision.gameObject.CompareTag("wall") && vunrable > 0)
        {
            Destroy(gameObject);
        }
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
        if (collision.gameObject == ball)
        {
            kick = false;
        }
    }
}
