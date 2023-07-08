using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallSliding : MonoBehaviour
{


    public Rigidbody2D rb;
    public Animator anime;
    // public float moveSpeed = 3;
    // private bool moveUp = false;
    // private bool moveDown = false;
    // private bool moveLeft = false;
    // private bool moveRight = false;
    public float thrust = 1f;
    public float dashMax = 5f;

    public bool canFire = false;
    public float timer = 0;
    public float fireRate = 3;
    public float dashing = 1;

    private float xv = 0;
    private float yv = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        transform.position = new Vector3(0.0f, -2.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {

        // if (moveUp)
        // {
        //     gameObject.transform.position = transform.position + (Vector3.up * moveSpeed) * Time.deltaTime;
        // }
        // if (moveLeft)
        // {
        //     gameObject.transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
        // }
        // if (moveDown)
        // {
        //     gameObject.transform.position = transform.position + (Vector3.down * moveSpeed) * Time.deltaTime;
        // }
        // if (moveRight)
        // {
        //     gameObject.transform.position = transform.position + (Vector3.right * moveSpeed) * Time.deltaTime;
        // }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up.normalized * thrust, ForceMode2D.Impulse);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            rb.velocity = Vector2.zero;

            rb.AddForce(Vector2.down.normalized * thrust, ForceMode2D.Impulse);
        }
        else
        {
            yv = 0;
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rb.velocity = Vector2.zero;

            rb.AddForce(Vector2.left.normalized * thrust, ForceMode2D.Force);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            rb.velocity = Vector2.zero;

            rb.AddForce(Vector2.right.normalized * thrust, ForceMode2D.Force);
        }
        else
        {
            xv = 0;
        }


        // rb.velocity = new Vector2(xv, yv) * dashing;

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > fireRate)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            canFire = false;
            dashing = dashMax;
        }

        if (dashing >= 1)
        {
            dashing -= Time.deltaTime;
        }
        if (dashing <= 1)
        {
            dashing = 1f;
        }

        anime.SetBool("Up", (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)));
        anime.SetBool("Down", (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)));
        anime.SetBool("Left", (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)));
        anime.SetBool("Right", (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)));
    }
}


