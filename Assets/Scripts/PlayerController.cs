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
    public Transform groundCheck;
    public string[] groundTags = { "Ground", "Platform", "Pinata" };

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
        if (checkTags(collision.collider.tag) && collision.collider.transform.position.y + collision.gameObject.GetComponent<BoxCollider2D>().size.y < groundCheck.position.y)
        {
            airborne = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (checkTags(collision.collider.tag) && collision.collider.transform.position.y + collision.gameObject.GetComponent<BoxCollider2D>().size.y < groundCheck.position.y)
            airborne = true;
    }

/*    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (airborne && (collision.collider.tag == "Ground" || collision.collider.tag == "Platform") && collision.collider.transform.position.y + collision.gameObject.GetComponent<BoxCollider2D>().size.y/2f < groundCheck.position.y)
            airborne = false;
    }
*/

    private bool checkTags(string tag)
    {
        foreach(string t in groundTags)
        {
            if (tag == t)
                return true;
        }
        return false;
    }
}
