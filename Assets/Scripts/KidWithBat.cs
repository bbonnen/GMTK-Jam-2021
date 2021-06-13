using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidWithBat : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Rigidbody2D kidRig;
    public BoxCollider2D assignedPlatform;
    public string[] groundTags = { "Ground", "Platform" };
    public Transform pinata;
    public float distanceToChasePinata = 5.0f;
    public float pinataHitForce = 8f;
    public Vector2 batSwingRange = new Vector2(-30f, -70f);
    public Transform batSprite;
    public Transform batPivot;
    public float batSwingTime = 0.2f;
    private float currentSwingTime = 0;
    public float jumpForce = 3f;


    public float decisionTime = 0.3f;
    private float timeSinceDecision = 0f;
    private float edgeOffset = 0.2f;
    private bool swingingBat = false;
    public bool jumping { get; private set; }
    public float moveDirection { get; private set; }

    //havokk
    public AudioSource audioSource;
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        if (kidRig == null)
            kidRig = GetComponent<Rigidbody2D>();
        if (pinata == null)
            pinata = GameManager.Instance.Pinata.transform;
        moveDirection = 0;
        timeSinceDecision = decisionTime;
        jumping = false;

    }

    // Update is called once per frame
    void Update()
    {
        DecideWhereToGo();
        swingBat();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (assignedPlatform == null && PlayerController.checkTags(collision.collider.tag, groundTags))
        {
            assignedPlatform = collision.gameObject.GetComponent<BoxCollider2D>();
        }
        else if (jumping && collision.collider == (BoxCollider2D)assignedPlatform)
            jumping = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Pinata")
        {
            Vector2 forceDirection = Vector2.ClampMagnitude(pinata.position - transform.position, 1.0f);
            pinata.GetComponent<Rigidbody2D>().AddForce(forceDirection * pinataHitForce, ForceMode2D.Impulse);
            swingingBat = true;
            pinata.GetComponent<PinataHealth>().GotHit();

            //havokk
            audioSource.Play();
        }
    }

    private void Move()
    {
        //move
        transform.Translate(Vector3.right * moveDirection * moveSpeed * Time.fixedDeltaTime);
        //check platform bounds so you don't run off the edge
        if (assignedPlatform != null)
        {
            if ((moveDirection > 0 && transform.position.x + edgeOffset > assignedPlatform.bounds.max.x) || (moveDirection < 0 && transform.position.x - edgeOffset < assignedPlatform.bounds.min.x))
                moveDirection = -moveDirection;
        }
    }

    private void DecideWhereToGo()
    {
        timeSinceDecision += Time.deltaTime;
        if(timeSinceDecision > decisionTime)
        {
            timeSinceDecision = 0;
            float pinataDist = Vector2.Distance(transform.position, pinata.position);
            if (pinataDist < distanceToChasePinata && !jumping)
            {
                Vector2 pinataDirection = pinata.position - transform.position;
                moveDirection = Mathf.Sign(pinata.position.x - transform.position.x);
                if(pinataDirection.y > Mathf.Abs(pinataDirection.x))
                {
                    jumping = true;
                    moveDirection = 0;
                    kidRig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }

            }
            else if(!jumping)
                moveDirection = Mathf.Sign(Random.Range(-1, 1));
        }

        
    }

    private void swingBat()
    {
        if (!swingingBat)
            return;

        currentSwingTime += Time.deltaTime;
        batSprite.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(batSwingRange.x, batSwingRange.y, currentSwingTime / batSwingTime));
        if(currentSwingTime > batSwingTime)
        {
            currentSwingTime = 0;
            swingingBat = false;
            batSprite.localEulerAngles = new Vector3(0, 0, batSwingRange.x);
        }
    }

/*    private void OnDrawGizmos()
    {
        if(assignedPlatform != null)
            Gizmos.DrawSphere(assignedPlatform.bounds.max, 0.2f);
        Gizmos.DrawCube(transform.position, Vector3.one * 0.2f);

    }
*/
}
