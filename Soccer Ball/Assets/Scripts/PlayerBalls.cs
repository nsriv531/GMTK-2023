using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Rigidbody2D;

public class PlayerBalls : MonoBehaviour
{

    public Rigidbody2D rb;
    // public float moveSpeed = 3;
    // private bool moveUp = false;
    // private bool moveDown = false;
    // private bool moveLeft = false;
    // private bool moveRight = false;
    public float thrust = 1f;

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
            rb.velocity = new Vector2(rb.velocity.x, thrust);
            // moveUp = true;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {

            // moveLeft = true;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {

            // moveDown = true;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {

            // moveRight = true;
        }

        // if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        // {
        //     moveUp = false;
        // }
        // if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        // {
        //     moveLeft = false;
        // }
        // if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        // {
        //     moveDown = false;
        // }
        // if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        // {
        //     moveRight = false;
        // }
    }
}
