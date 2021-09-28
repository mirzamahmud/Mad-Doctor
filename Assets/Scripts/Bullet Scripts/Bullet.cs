// this script will handle bullet movement and its direction

using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 15f;

    [SerializeField]
    private float damageAmount = 35f;

    private Vector3 moveVector = Vector3.zero;
    private Vector3 tempScale;

    private void Update()
    {
        MoveBullet();
    }

    void MoveBullet()
    {
        moveVector.x = moveSpeed * Time.deltaTime;
        // bullet position
        transform.position += moveVector;
    }

    public void SetNegativeSpeed()
    {
        moveSpeed *= -1f;
        tempScale = transform.localScale;
        tempScale.x = -tempScale.x;
        transform.localScale = tempScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(TagManager.Enemy_Tag))
        {
            // damage to the enemy
            collision.GetComponent<EnemyHealth>().TakeDamage(damageAmount);
            Destroy(gameObject); // this means distory that gameobject which is holding this script, here this gameobjet = Bullet
        }
    }
}
