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
    public bool isHit;
    private float onHitTravelDuration = 1f;
    public float onHitElapseTravelTime;
    private Vector2 positionWhenHit;
    [SerializeField]
    private AnimationCurve curve;
    #endregion

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
        if(isControlsEnabled)
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
            if(Input.GetKeyDown(KeyCode.C))
            {
                TakeDamage();
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
    public Vector3 CheckIfShouldFlip(int XInput)
    {
        if (XInput != 0 && XInput != facingDirection)
        {
            return Flip();
        }
        return new Vector3(0, 0, 0);
    }
    public void CalculateMovementDirection()
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
        positionWhenHit = transform.position;
        isHit = true;
          StartCoroutine( OnHitMovement());
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
        }
        
    }
    public void DashActivated()
    {
        isDashing = true;

        trailRenderer.startColor = Color.red;

        ballSprite.color = Color.red;

        trailRenderer.time = 0.6f;

        speed += dashSpeedBoostAmount;
    }
    public void DashDisabled()
    {

        isDashing = false;

        dashtimer = dashDuration;

        trailRenderer.startColor = Color.white;

        trailRenderer.time = 0.4f;

        ballSprite.color = Color.white;

        speed -= dashSpeedBoostAmount;
    }
    public IEnumerator OnHitMovement()
    {
        float percentageOfTravelCompleted = 0;
        Debug.Log("ji");

        while (percentageOfTravelCompleted <= 1)
        {
            onHitElapseTravelTime += Time.deltaTime;
            percentageOfTravelCompleted = onHitElapseTravelTime / onHitTravelDuration;
            Debug.Log("ji");

            rb.MovePosition(Vector3.Lerp(positionWhenHit, netPosition.position, curve.Evaluate(percentageOfTravelCompleted)))  ;

            yield return null;
        }
    }
}


