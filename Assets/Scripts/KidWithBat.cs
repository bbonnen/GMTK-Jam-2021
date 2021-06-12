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

    public float decisionTime = 0.3f;
    private float timeSinceDecision = 0f;
    private float edgeOffset = 0.2f;

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

/*    private void OnDrawGizmos()
    {
        if(assignedPlatform != null)
            Gizmos.DrawSphere(assignedPlatform.bounds.max, 0.2f);
        Gizmos.DrawCube(transform.position, Vector3.one * 0.2f);

    }
*/
}
