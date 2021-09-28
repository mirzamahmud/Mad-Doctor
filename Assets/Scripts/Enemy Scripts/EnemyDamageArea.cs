

using UnityEngine;

public class EnemyDamageArea : MonoBehaviour
{
    [SerializeField]
    private float deactivateWaitTime = 0.1f; // deactivate this script
    private float deactivateTimer;

    [SerializeField]
    private LayerMask playerLayer;

    private bool canDealDamage;

    [SerializeField]
    private float damageAmount = 5f;

    // player health
    private PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = GameObject.FindWithTag(TagManager.Player_Tag).GetComponent<PlayerHealth>();
        gameObject.SetActive(false);
    }

    private void Update() // Update function is called once per Frame
    {
        if(Physics2D.OverlapCircle(transform.position, 1f, playerLayer))
        {
            if(canDealDamage)
            {
                canDealDamage = false; //we will do it to ensure that enemy will not able to damage again our player in frame second.

                // deal damage to player
                playerHealth.TakeDamage(damageAmount);
            }
        }

        DeactivateDamageArea();
    }

    void DeactivateDamageArea()
    {
        // deactivate gameobject
        if(Time.time > deactivateTimer)
        {
            gameObject.SetActive(false);
        }
    }

    public void ResetDeactivateTimer()
    {
        canDealDamage = true;
        deactivateTimer = Time.time + deactivateWaitTime;
    }

}
