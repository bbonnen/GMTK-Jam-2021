using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidWithBat : MonoBehaviour
{
    public Rigidbody2D kidRig;
    public BoxCollider2D assignedPlatform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GameManager.Instance.player.horizontalInput);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(assignedPlatform == null )
        {

        }
    }
}
