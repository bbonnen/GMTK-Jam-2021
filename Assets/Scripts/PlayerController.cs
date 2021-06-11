using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float airMoveSpeed = 5f;
    public float jumpForce = 100f;
    public Rigidbody2D myRig;
    public KeyCode[] jumpButton;

    private float horizontalInput = 0;
    private bool airborne = false;
    private bool jumpPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        if (myRig == null) myRig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        //ProcessMovement();
    }

    private void FixedUpdate()
    {
        //myRig.velocity += moveSpeed * Vector2.right * horizontalInput * Time.fixedDeltaTime;
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        transform.Translate(new Vector3(horizontalInput * (airborne ? airMoveSpeed : moveSpeed) * Time.fixedDeltaTime, 0, 0));
        //myRig.MovePosition(myRig.position + Vector2.right * horizontalInput * (airborne ? airMoveSpeed : moveSpeed) * Time.fixedDeltaTime);
        if (!airborne && jumpPressed)
        {
            airborne = true;
            myRig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    //Get Input
    void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //check all jump keys
        jumpPressed = false;
        foreach (KeyCode k in jumpButton)
            if (Input.GetKey(k)) jumpPressed = true;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" || collision.collider.tag == "Platform")
            airborne = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" || collision.collider.tag == "Platform")
            airborne = true;
    }
}
