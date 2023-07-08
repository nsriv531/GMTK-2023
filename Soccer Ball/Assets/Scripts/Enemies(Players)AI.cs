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
    private float distance;
    public Animator anime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, ball.transform.position);
        Vector2 direction = ball.transform.position - transform.position;
        rb.velocity = direction.normalized * speed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

       // Debug.Log(angle);

        anime.SetBool("Up", (angle >= 45 && angle <= 135));
        anime.SetBool("Down", (angle <= -45 && angle >= -135));
        anime.SetBool("Left", (angle >= 135 || angle <= -135));
        anime.SetBool("Right", (angle <= 45 && angle >= -45));


    }

    public void TakeDamage()
    {
        ///takes damge
        Debug.Log("im Hit!!!");
       
    }
}
