using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameActions : MonoBehaviour
{
    PlayerController controller;
    public bool touchingPresent { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        touchingPresent = false;
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Present collidingPresent = other.GetComponent<Present>();
        //if unwrapped present is colliding
        if (collidingPresent != null)
        {
            touchingPresent = true;
            if (!collidingPresent.isWrapping)
            {
                //Start present wrapping
                collidingPresent.StartWrapping();
            }
            else
            {
                //cancel wrapping
                collidingPresent.CancelWrapping();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Present collidingPresent = other.GetComponent<Present>();
        if (collidingPresent != null)
        {
            touchingPresent = false;
            //if present is still wrapping, cancel wrapping
            if(collidingPresent.isWrapping)
                collidingPresent.CancelWrapping();
        }
    }

}
