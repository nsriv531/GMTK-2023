using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBallSliding : MonoBehaviour
{

    #region Refranceses
    private Rigidbody2D rb;
    private Animator anime;
    private AttackComponent attack;
    private TrailRenderer trailRenderer;
    private SpriteRenderer ballSprite;
    #endregion
    #region DashAndMovement

    public bool isControlsEnabled;

    public float speed = 20f;
    private float dashSpeedBoostAmount = 25f;

    private bool isDashing;
    [SerializeField]
    private float dashDuration;
    private float dashtimer;

    private Vector2 rawMovemntDirection;
    private Vector2 movemntDirection;
    private int facingDirection;
    #endregion
    #region Hitlogic
    [SerializeField]
    private Transform netPosition;
    [SerializeField]
    private Transform previouseStageEntrance;
    public bool isHit;
    private float onHitTravelDuration = 1f;
    public float onHitElapseTravelTime;
    private Vector2 positionWhenHit;
    [SerializeField]
    private AnimationCurve curve;
    #endregion

    public float controllCoolDown = 0;
    public float coolDown = 10;

    public GameObject ui;

    // Start is called before the first frame update
    void Start()
    {
        isControlsEnabled = true;
        facingDirection = 1;
        dashtimer = dashDuration;
        rb = GetComponent<Rigidbody2D>();
        anime= GetComponent<Animator>();
        attack= GetComponentInChildren<AttackComponent>();
        trailRenderer= GetComponent<TrailRenderer>();
        ballSprite= GetComponent<SpriteRenderer>();
        transform.position = new Vector3(0.0f, -2.0f, 0.0f);
        trailRenderer.enabled = true;
    }

    private void Update()
    {
        if (isControlsEnabled)
        {
            CheckIfShouldFlip((int)rawMovemntDirection.x);
            CalculateMovementDirection();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!isDashing)
                {
                    DashActivated();
                }

            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                TakeDamage();
            }

        }
        else {
            controllCoolDown -= Time.deltaTime;
            if (controllCoolDown <= 0) {
                isControlsEnabled = true;
            }
        }



    }
    // Update is called once per frame
    void FixedUpdate()
    {


       

        MoveCharacter(movemntDirection);


       

        

        DashTImer();

        if(movemntDirection == Vector2.zero)
        {
            CheckIfShouldFlip((int)rb.velocity.x);
            anime.SetFloat("YAxisRoll", Mathf.Abs(rb.velocity.y));
            anime.SetFloat("Xaxisroll", Mathf.Abs(rb.velocity.x));

        }
        else
        {
            anime.SetFloat("YAxisRoll", Mathf.Abs(movemntDirection.y));
            anime.SetFloat("Xaxisroll", Mathf.Abs(movemntDirection.x));
        }
        
    }
    public void MoveCharacter(Vector2 direction)
    {
        
        rb.AddForce(direction.normalized * speed);

    }
    public void DashTImer()
    {
        if(isDashing && dashtimer > 0)
        {
            attack.AattackTheEnemy();
            dashtimer -= Time.deltaTime;
            if(dashtimer <= 0)
            {
                DashDisabled();
            }
        }
        
    }
    private Vector2 Flip()
    {
        facingDirection *= -1;
        Vector3 rotation = new Vector3(0.0f, 180.0f, 0f);
        rb.transform.Rotate(0.0f, 180f, 0.0f);
        return rotation;
    }
    private Vector3 CheckIfShouldFlip(int XInput)
    {
        if (XInput != 0 && XInput != facingDirection)
        {
            return Flip();
        }
        return new Vector3(0, 0, 0);
    }
    private void CalculateMovementDirection()
    {
        rawMovemntDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (rawMovemntDirection.x != 0 && rawMovemntDirection.y == 0)
        {
            movemntDirection = new Vector2(1 * facingDirection, 0);
        }
        else if (rawMovemntDirection.y > 0 && rawMovemntDirection.x == 0)
        {
            movemntDirection = new Vector2(0, 1);

        }
        else if (rawMovemntDirection.y < 0 && rawMovemntDirection.x == 0)
        {
            movemntDirection = new Vector2(0, -1);

        }
        else if (rawMovemntDirection.x != 0 && rawMovemntDirection.y > 0)
        {
            movemntDirection = new Vector2(1 * facingDirection, 1);

        }
        else if (rawMovemntDirection.x != 0 && rawMovemntDirection.y < 0)
        {
            movemntDirection = new Vector2(1 * facingDirection, -1);

        }
        else if (rawMovemntDirection.x == 0 && rawMovemntDirection.y == 0)
        {
            movemntDirection = new Vector2(0, 0);

        }
    }

    public void TakeDamage()
    {
        Debug.Log(ui.GetComponent<GameUI>().health);
        if (!isDashing) {
            /*positionWhenHit = transform.position;*/
            isControlsEnabled = false;
            if (controllCoolDown <= 0) {
                controllCoolDown = coolDown;
            }
            ui.GetComponent<GameUI>().health--;
        }
        /*if(SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
            if(!isHit)
            {
                isHit = true;
              StartCoroutine(OnHitMovement(netPosition));

            }
        }
        else
        {
            if(isHit)
                 StartCoroutine(OnHitMovement(previouseStageEntrance));

        }*/

    }
    private void DashActivated()
    {
        isDashing = true;

        trailRenderer.startColor = Color.red;

        ballSprite.color = Color.red;

        trailRenderer.time = 0.6f;

        speed += dashSpeedBoostAmount;
    }
    private void DashDisabled()
    {

        isDashing = false;

        dashtimer = dashDuration;

        trailRenderer.startColor = Color.white;

        trailRenderer.time = 0.4f;

        ballSprite.color = Color.white;

        speed -= dashSpeedBoostAmount;
    }
    /// <summary>
    /// moves the ball to a location when it is hit
    /// </summary>
    /// <param name="Destination"> either the net, or the entrance to the previous map</param>
    /// 
    /// <returns></returns>
    private IEnumerator OnHitMovement(Transform Destination)
    {
        float percentageOfTravelCompleted = 0;
        Physics2D.IgnoreLayerCollision(0, 6,true);
        Debug.Log("ji");

        while (percentageOfTravelCompleted <= 1)
        {
            onHitElapseTravelTime += Time.deltaTime;
            percentageOfTravelCompleted = onHitElapseTravelTime / onHitTravelDuration;

            rb.MovePosition(Vector3.Lerp(positionWhenHit, Destination.position, curve.Evaluate(percentageOfTravelCompleted)))  ;

            yield return null;
        }
        onHitElapseTravelTime = 0 ;
        Physics2D.IgnoreLayerCollision(0, 6, false);
        isHit = false;
        isControlsEnabled = true;
    }
    
    
}


