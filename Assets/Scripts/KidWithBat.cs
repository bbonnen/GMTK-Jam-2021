using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidWithBat : MonoBehaviour
{
    public Rigidbody2D kidRig;
    public BoxCollider2D assignedPlatform;
    public string[] groundTags = { "Ground", "Platform" };

    // Start is called before the first frame update
    void Start()
    {
        if (kidRig == null)
            kidRig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GameManager.Instance.player.horizontalInput);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(assignedPlatform == null && PlayerController.checkTags(collision.collider.tag, groundTags))
        {
            assignedPlatform = collision.gameObject.GetComponent<BoxCollider2D>();
        }
    }
}
