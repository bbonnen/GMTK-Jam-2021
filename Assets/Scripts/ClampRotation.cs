using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampRotation : MonoBehaviour
{
    public Vector3 minAngle = new Vector3(0,0, -30);
    public Vector3 maxAngle = new Vector3(0, 0, 30);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.localEulerAngles.z);
        transform.eulerAngles = new Vector3(Mathf.Clamp(transform.eulerAngles.x, minAngle.x, maxAngle.x), Mathf.Clamp(transform.eulerAngles.y, minAngle.y, maxAngle.y), Mathf.Clamp(transform.eulerAngles.z, minAngle.z, maxAngle.z));
    }
}
