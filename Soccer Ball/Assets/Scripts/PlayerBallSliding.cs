using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBallSliding : MonoBehaviour,IDamagable
{

    #region Refranceses
    private Rigidbody2D rb;
    private Animator anime;
    private AttackComponent attack;
    private TrailRenderer trailRenderer;
    private SpriteRenderer ballSprite;
    #endregion
    public float DashValue;
    float dashChargDuration = 1;
    float elapsetime;

    public bool isDashing;
    public float dashDuration = 0.2f;
    public int Dashspeed = 80;
    float dashElpssetime;

    public Vector2 Direction;


    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anime= GetComponent<Animator>();
        attack= GetComponentInChildren<AttackComponent>();
        trailRenderer= GetComponent<TrailRenderer>();
        ballSprite= GetComponent<SpriteRenderer>();
        
    }

    private void Update()
    {
        if (!isDashing)

             Direction = new Vector2Int((int)Input.GetAxisRaw("Horizontal"), (int)Input.GetAxisRaw("Vertical"));
        
        Dash();


    }
    // Update is called once per frame
    void FixedUpdate()
    {


    }
    public void MoveToDirection(float speed)
    {
        Debug.Log(speed);
        rb.velocity = speed * Direction.normalized; 
    }

    public void TakeDamage(int takeDamage)
    {
        throw new System.NotImplementedException();
    }
    public void Dash()
    {
        if (Input.GetKey(KeyCode.Space) && !isDashing)
        {
                float completetime = elapsetime/dashChargDuration;
                elapsetime += Time.deltaTime;
                DashValue = Mathf.Lerp(0,1,completetime);
            
        }else if (Input.GetKeyUp(KeyCode.Space) && !isDashing)
        {
            dashElpssetime = Time.time;
            isDashing= true;
        }
        if(Time.time < dashElpssetime + dashDuration && isDashing)
         {
                MoveToDirection(Dashspeed * DashValue);
         }
         else if(Time.time > dashElpssetime + dashDuration && isDashing)
          {
            MoveToDirection(10);
            DashValue= 0;
            elapsetime= 0;
            isDashing= false;
          }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemies"))
        {
            attack.AattackTheEnemy();
        }
    }
}


