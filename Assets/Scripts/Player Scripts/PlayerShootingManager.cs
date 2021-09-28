// this script will handle the shooting of our player

using UnityEngine;

public class PlayerShootingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform bulletSpawnPosition;

    public void Shoot(float facingDirection)
    {
        GameObject newBullet = Instantiate(bulletPrefab, bulletSpawnPosition.position, Quaternion.identity);

        if(facingDirection < 0)
        {
            newBullet.GetComponent<Bullet>().SetNegativeSpeed();
        }

        SoundManager.instance.PlayShootingSound();
    }
}
