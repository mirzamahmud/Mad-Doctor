using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField]
    private AudioClip shootingSound;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void PlayShootingSound()
    {
        AudioSource.PlayClipAtPoint(shootingSound, transform.position);
    }
}
