using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float health = 100f;

    private Enemy enemyScript; // reference Enemy Script

    [SerializeField]
    private Slider enemyHealthSlider;

    private void Awake()
    {
        enemyScript = GetComponent<Enemy>();
    }

    public void TakeDamage(float damageAmount)
    {
        if(health <= 0)
        {
            return;
        }

        health -= damageAmount;
        if(health <= 0f)
        {
            health = 0;

            // kill the enemy
            enemyScript.EnemyDied();

            EnemySpawner.instance.EnemyDied(gameObject);

            GameplayController.instance.EnemyKilled();
        }

        enemyHealthSlider.value = health;

    }

}
