using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    Animator anim;
    PlayerController playerController;
    SpriteRenderer playerSprite;
    Rigidbody2D rb;

    public bool shouldFlip;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        playerSprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (anim != null)
        {
            Debug.Log("AnimationScript found the PlayerController");
        }
        if (playerController != null)
        {
            Debug.Log("AnimationScript found the Animator");
        }
    }

    // Update is called once per frame
    void Update()
    {
        FlipCheck(playerController.horizontalInput);

        if (playerController.horizontalInput != 0 && playerController.airborne == false)
        {
            anim.Play("PinkRun");
        }
        else if (rb.velocity.y > 0 && playerController.airborne == true)
        {
           // if (playerController.horizontalInput <) 
            anim.Play("PinkJump");
        }
        else if (rb.velocity.y < 0 && playerController.airborne == true)
        {
            anim.Play("PinkFall");
        }
        else if (playerController.horizontalInput == 0 && playerController.airborne == false)
        {
            anim.Play("PinkIdle");
        }
    }

    public void FlipCheck(float hor)
    {
        if (hor > 0)
        {
            playerSprite.flipX = false;
        }
        else if (hor < 0)
        {
            playerSprite.flipX = true;
        }
        else if (hor == 0)
        {

        }
    }
}
