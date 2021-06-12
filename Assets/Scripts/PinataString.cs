using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinataString : MonoBehaviour
{
    public Transform connectionPoint;
    public SpringJoint2D pinataString;
    public Vector2 stringLengthRange = new Vector2(1.5f, 8.0f);
    public Vector2 playerDistanceRange = new Vector2(1.5f, 12f);

    private float ropeDistance;
    private float pinataLength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ropeDistance = Vector2.Distance(connectionPoint.position, transform.position);
        //god this is a mess. Basically sets the string length to the equivalent point between its min & max range to match the distance's current point between its min & max range
        pinataLength = Mathf.Lerp(stringLengthRange.y, stringLengthRange.x, Mathf.InverseLerp(playerDistanceRange.x, playerDistanceRange.y, Mathf.Clamp(ropeDistance, playerDistanceRange.x, playerDistanceRange.y))); 
        pinataString.distance = pinataLength;
    }
}