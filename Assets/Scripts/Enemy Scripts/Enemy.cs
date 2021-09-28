// this script will handle enemy movement, attack, follow our player

using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform playerTarget;

    [SerializeField]
    private float moveSpeed = 2f;

    private Vector3 tempScale;

    [SerializeField]
    private float stoppingDistance = 1.5f;

    private PlayerAnimation enemyAnimation;

    [SerializeField]
    private float attackWaitTime = 2.5f;
    private float attackTimer;

    [SerializeField]
    private float attackFinishedWaitTime = 0.5f;
    private float attackFinishTimer;

    [SerializeField]
    private EnemyDamageArea enemyDamageArea;

    private bool enemyDied;

    // this are used to when enemy move his position, then healthbar will not change its position
    [SerializeField]
    private RectTransform healthbarTransform; // here we use rectTransform because it is an ui element
    private Vector3 healthbarTempScale;

    private void Awake()
    {
        playerTarget = GameObject.FindWithTag(TagManager.Player_Tag).transform;

        enemyAnimation = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        if(enemyDied)
        {
            return;
        }

        SearchForPlayer();
    }

    void SearchForPlayer()
    {
        // we don't have player target we're just going to return
        if (!playerTarget)
        {
            return;
        }

        if(Vector3.Distance(transform.position, playerTarget.position) > stoppingDistance) // here transform.position = enemy position
        {
            // in this case we actually need to go towards the player or enemy
            transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, moveSpeed * Time.deltaTime);

            enemyAnimation.PlayAnimation(TagManager.Walk_Animation_Name);

            HandleFacingDirection();
        }
        else
        {
            CheckIfAttackFinished();
            Attack();
        }

    }

    void HandleFacingDirection() // this is for enemy direction
    {
        tempScale = transform.localScale;
        
        if(transform.position.x > playerTarget.position.x)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
        }
        else
        {
            tempScale.x = - Mathf.Abs(tempScale.x);
        }

        transform.localScale = tempScale;

        healthbarTempScale = healthbarTransform.localScale;

        if(transform.localScale.x > 0f)
        {
            healthbarTempScale.x = Mathf.Abs(healthbarTempScale.x);
        }
        else
        {
            healthbarTempScale.x = -Mathf.Abs(healthbarTempScale.x);
        }

        healthbarTransform.localScale = healthbarTempScale;

    }

    void CheckIfAttackFinished()
    {
        if(Time.time > attackFinishTimer)
        {
            enemyAnimation.PlayAnimation(TagManager.Idle_Animation_Name);
        }
    }

    void Attack()
    {
        if(Time.time > attackTimer)
        {
            attackFinishTimer = Time.time + attackFinishedWaitTime;
            attackTimer = Time.time + attackWaitTime;

            enemyAnimation.PlayAnimation(TagManager.Attack_Animation_Name);
        }
    }

    void EnemyAttacked()
    {
        enemyDamageArea.gameObject.SetActive(true);
        enemyDamageArea.ResetDeactivateTimer();
    }

    public void EnemyDied()
    {
        enemyDied = true;
        enemyAnimation.PlayAnimation(TagManager.Death_Animation_Name);
        Invoke("DestroyEnemyAfterDelay", 1.5f);
    }

    void DestroyEnemyAfterDelay()
    {
        Destroy(gameObject); // this means distory that gameobject which is holding this script, here this gameobjet = Enemy
    }

}
