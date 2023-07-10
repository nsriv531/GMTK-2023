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
    public Transform Net;
    public Camera camera;
    #endregion
    public float DashValue;
    float dashChargDuration = 1;
    float elapsetime;

    public bool isDashing;
    public float dashDuration = 0.2f;
    public int Dashspeed = 80;
    float dashElpssetime;
    private float dashAngle;

    public Vector2 DashAimDirection;
    public Vector2Int MovementDirection;

    public float elpseMoveTime;
    public float duration = 0.4f;
    public bool isHit;

    public bool canmove;


    public PlayerEvents playerEvents;
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        anime= GetComponent<Animator>();
        attack= GetComponentInChildren<AttackComponent>();
        trailRenderer= GetComponent<TrailRenderer>();
        ballSprite= GetComponent<SpriteRenderer>();
        canmove = true;
        
    }

    private void Update()
    {
        if(canmove)
        {
         MovementDirection = new Vector2Int((int)Input.GetAxisRaw("Horizontal"), (int)Input.GetAxisRaw("Vertical"));

        }

        if (!isHit)
        {
             Dash();

        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            TakeDamage(5);
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (canmove && MovementDirection != Vector2Int.zero && !isHit)
        {
        rb.velocity = 10 * MovementDirection;

        }
    }
    public void MoveToDirection(float speed)
    {
        rb.velocity = ( speed * transform.right);
    }
    public void AddForce(float speed)
    {
        rb.AddForce (speed * transform.right, ForceMode2D.Impulse);
    }
    public void TakeDamage(float takeDamage)
    {
        Debug.Log("PlayerHit");
        AddForce(10);

        StopDashing(true);
    }
    public void Dash()
    {
        if (Input.GetKey(KeyCode.Space) && !isDashing )
        {
            MovementDirection= Vector2Int.zero;
            float completedTime = elapsetime/dashChargDuration;
            elapsetime += Time.deltaTime ;
            DashValue = Mathf.Lerp(0,1,completedTime);
            dashAngle = MouseDirection();
            rb.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);

            playerEvents.onChargeValue?.Invoke(DashValue);
        }
        else if (Input.GetKeyUp(KeyCode.Space) && !isDashing)
        {
            canmove= false;
            dashElpssetime = Time.time;
            elapsetime= 0;
            rb.transform.rotation = Quaternion.AngleAxis(dashAngle, Vector3.forward);

            isDashing = true;
        }
        if(Time.time < dashElpssetime + dashDuration && isDashing)
         {
                attack.AattackTheEnemy(5 * DashValue);
            ballSprite.color = Color.red;
                MoveToDirection(Dashspeed * DashValue);
         }
         else if(Time.time > dashElpssetime + dashDuration && isDashing)
          {
           StopDashing(false);
          }
    }
   
    private IEnumerator MoveToGoal()
    {
        
         elpseMoveTime = duration;

        while(elpseMoveTime > 0){

            elpseMoveTime-= Time.deltaTime;

           
             yield return null;
        }
        rb.velocity = Vector2.one;
        isHit= false;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isDashing)
        {
             attack.AattackTheEnemy(5 * DashValue);

        }
        StopDashing(true);

    }
    public void StopDashing(bool isHitWall)
    {
        AddForce(5);

      
        canmove = true;
        ballSprite.color = Color.white;

        isDashing = false;
    }
    public float  MouseDirection()
    {
        Vector3 p = Input.mousePosition;
        DashAimDirection = camera.ScreenToWorldPoint(p) - transform.position;
        float angle = Mathf.Atan2(DashAimDirection.y, DashAimDirection.x) * Mathf.Rad2Deg;

        playerEvents.onChargeFirection?.Invoke(angle);
        return angle;
    }
}


