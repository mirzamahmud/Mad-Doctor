// this script will handle player movement, and which position player will move

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    public float moveSpeed = 3.5f;

    [SerializeField]
    public float minBound_X = -71f, maxBound_X = 71f, minBound_Y = -3.3f, maxBound_Y = 0f; // which positions player can be moved

    private Vector3 tempPosition;
    private float xAxis, yAxis;

    private PlayerAnimation playerAnimation;

    [SerializeField]
    private float shootWaitTime = 0.5f;

    private float waitBeforeShooting;

    [SerializeField]
    private float moveWaitTime = 0.3f; // when we shoot stop movement

    private float waitBeforeMoving;
    private bool canMove = true;

    private PlayerShootingManager playerShootingManager;

    private bool playerDied;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerShootingManager = GetComponent<PlayerShootingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerDied)
        {
            return;
        }

       
        HandleMovement();
        HandleAnimation();
        HandleFacingDirection();

        HandleShooting();
        CheckIfCanMove();
        
    }

    void HandleMovement()
    {
        xAxis = Input.GetAxisRaw(TagManager.Horizontal_Axis);
        yAxis = Input.GetAxisRaw(TagManager.Vertical_Axis);

        if(!canMove)
        {
            return;
        }

        tempPosition = transform.position;

        tempPosition.x += xAxis * moveSpeed * Time.deltaTime;
        tempPosition.y += yAxis * moveSpeed * Time.deltaTime;

        if(tempPosition.x < minBound_X)
        {
            tempPosition.x = minBound_X;
        }

        if(tempPosition.x > maxBound_X)
        {
            tempPosition.x = maxBound_X;
        }

        if(tempPosition.y < minBound_Y)
        {
            tempPosition.y = minBound_Y;
        }

        if(tempPosition.y > maxBound_Y)
        {
            tempPosition.y = maxBound_Y;
        }

        transform.position = tempPosition;
    }

    void HandleAnimation()
    {

        if(!canMove)
        {
            return;
        }

        if(Mathf.Abs(xAxis) > 0 || Mathf.Abs(yAxis) > 0)
        {
            playerAnimation.PlayAnimation(TagManager.Walk_Animation_Name);
        }
        else
        {
            playerAnimation.PlayAnimation(TagManager.Idle_Animation_Name);
        }
    }

    void HandleFacingDirection()
    {
        if(xAxis > 0)
        {
            playerAnimation.SetFacingDirection(true);
        }
        else if(xAxis < 0)
        {
            playerAnimation.SetFacingDirection(false);
        }
    }

    void StopMovement()
    {
        canMove = false;
        waitBeforeMoving = Time.time + moveWaitTime;
    }

    void Shoot()
    {
        waitBeforeShooting = Time.time + shootWaitTime;
        StopMovement();
        playerAnimation.PlayAnimation(TagManager.Shoot_Animation_Name);

        // player fire bullet direction
        playerShootingManager.Shoot(transform.localScale.x);
    }

    void CheckIfCanMove()
    {
        if(Time.time > waitBeforeMoving)
        {
            canMove = true;
        }
    }

    void HandleShooting()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(Time.time > waitBeforeShooting)
            {
                Shoot();
            }
        }
    }

    public void PlayerDied()
    {
        playerDied = true;

        playerAnimation.PlayAnimation(TagManager.Death_Animation_Name);

        Invoke("DestroyPlayerAfterDelay", 2f);
    }

    void DestroyPlayerAfterDelay()
    {
        Destroy(gameObject); // destroy our player bcz its holding this script as a gameObject
    }

}
