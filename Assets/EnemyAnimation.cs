using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    Animator anim;
    KidWithBat kwb;
    //SpriteRenderer kidSprite;
    Rigidbody2D rb;

    public bool shouldFlip;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        kwb = GetComponentInParent<KidWithBat>();
        //kidSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (anim != null)
        //{
        //    Debug.Log("FoundAnim");
        //}
        //if (kwb != null)
        //{
        //    Debug.Log("FoundKWB");
        //}

        //FlipCheck(playerController.horizontalInput);

        anim.Play("EnemyRun");

        if (kwb.moveDirection < 0)
        {
            anim.SetFloat("EnemyDirection", -1);
        }
        else if (kwb.moveDirection > 0)
        {
            anim.SetFloat("EnemyDirection", 1);
        }

    }

    //public void FlipCheck(float hor)
    //{
    //    if (hor > 0)
    //    {
    //        kidSprite.flipX = false;
    //    }
    //    else if (hor < 0)
    //    {
    //        kidSprite.flipX = true;
    //    }
    //    else if (hor == 0)
    //    {

    //    }
    //}
}
