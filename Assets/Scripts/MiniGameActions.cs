using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameActions : MonoBehaviour
{
    PlayerController controller;
    public KeyCode[] actionButton;
    public bool touchingPresent { get; private set; }
    public bool actionPressed { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        actionPressed = false; 
        touchingPresent = false;
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        actionPressed = false;
        foreach (KeyCode k in actionButton)
            if (Input.GetKey(k)) actionPressed = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Present collidingPresent = other.GetComponent<Present>();
        //if unwrapped present is colliding
        if (collidingPresent != null)
        {
            Debug.Log("enter:startwrapping");
            touchingPresent = true;
            if (!controller.airborne && !collidingPresent.isWrapping && actionPressed)
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
