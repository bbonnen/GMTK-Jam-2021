using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip jumpSound;

    private PlayerController pc;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        pc = GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        if (pc.jumpPressed)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }
}
