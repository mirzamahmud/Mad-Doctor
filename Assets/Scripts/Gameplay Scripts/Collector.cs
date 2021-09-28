// this script will handle to collect bullets

using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(TagManager.Bullet_Tag))
        {
            Destroy(collision.gameObject);
        }
    }
}
