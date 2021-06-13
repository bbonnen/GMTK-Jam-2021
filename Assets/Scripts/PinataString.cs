using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinataString : MonoBehaviour
{
    public Transform connectionPoint;
    public SpringJoint2D pinataString;
    public Vector2 stringLengthRange = new Vector2(1.5f, 8.0f);
    public Vector2 playerDistanceRange = new Vector2(1.5f, 12f);
    public KeyCode[] yankButton = { KeyCode.E, KeyCode.LeftShift };
    public float yankRecoveryTime = 0.3f;
    public float yankDistance = 0.3f;
    private bool yanking = false;

    private float currentYank = 0;
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
        pinataLength = Mathf.Lerp(stringLengthRange.y, stringLengthRange.x, Mathf.InverseLerp(playerDistanceRange.x, playerDistanceRange.y, Mathf.Clamp(ropeDistance, playerDistanceRange.x, playerDistanceRange.y)) + currentYank/yankRecoveryTime * yankDistance);

        HandleYankButton();

        pinataString.distance = pinataLength - currentYank;
    }

    void HandleYankButton()
    {
        foreach (KeyCode k in yankButton)
            if (!yanking && Input.GetKeyDown(k))
            {
                yanking = true;
                currentYank = yankRecoveryTime;
            }
        if (yanking)
        {
            currentYank -= Time.deltaTime;
            if (currentYank <= 0)
            {
                currentYank = 0;
                yanking = false;
            }
        }
    }
}