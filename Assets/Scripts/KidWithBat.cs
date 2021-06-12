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


    public float decisionTime = 0.3f;
    private float timeSinceDecision = 0f;
    private float edgeOffset = 0.2f;
    private bool swingingBat = false;
    public float moveDirection { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        if (kidRig == null)
            kidRig = GetComponent<Rigidbody2D>();
        if (pinata == null)
            pinata = GameManager.Instance.Pinata.transform;
        moveDirection = 0;
        timeSinceDecision = decisionTime;
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
        if(assignedPlatform == null && PlayerController.checkTags(collision.collider.tag, groundTags))
        {
            assignedPlatform = collision.gameObject.GetComponent<BoxCollider2D>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Pinata")
        {
            Vector2 forceDirection = Vector2.ClampMagnitude(pinata.position - transform.position, 1.0f);
            Debug.Log(forceDirection);
            pinata.GetComponent<Rigidbody2D>().AddForce(forceDirection * pinataHitForce, ForceMode2D.Impulse);
            swingingBat = true;
            pinata.GetComponent<PinataHealth>().GotHit();
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
            if (pinataDist < distanceToChasePinata)
            {
                moveDirection = Mathf.Sign(pinata.position.x - transform.position.x);
            }
            else
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
