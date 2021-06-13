using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRope : MonoBehaviour
{
    LineRenderer myLine;
    public Transform endPoint;

    // Start is called before the first frame update
    void Start()
    {
        if (myLine == null)
            myLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        myLine.SetPositions(new Vector3[] { new Vector3(endPoint.position.x, endPoint.position.y, -2), new Vector3(transform.position.x, transform.position.y, -2) });
    }
}
