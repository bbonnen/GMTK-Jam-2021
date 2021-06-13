using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : MonoBehaviour
{
    private float wrappingTime;
    public bool cancelFlag { get; private set; }
    public bool isWrapping { get; private set; }
    public bool isWrapped { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        isWrapping = false;
        isWrapped = false;
        cancelFlag = false;
        wrappingTime = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartWrapping()
    {
        if (!isWrapped && !isWrapping)
        {
            isWrapping = true;
            //start timed event in which at the end, you check the cancel flag and then complete
            StartCoroutine(FinishWrappingOnDelay(wrappingTime));

            //PLAY ANIMATION: start a timer bar for the wrapping(animation should last same time as "wrappingTime"
            //PLAY SOUND: wrappingPresent.wav
        }
    }

    public void CancelWrapping()
    {
        if (!isWrapped && isWrapping)
        {
            isWrapping = false;
            cancelFlag = true;
            //PLAY ANIMATION: Cancel timer bar wrapping animation
        }
    }

    IEnumerator FinishWrappingOnDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        if (cancelFlag)
        {
            //wrapping was canceled, flip flag and do nothing
            cancelFlag = false;
            yield break;
        }
        else
        {
            //Finish Wrapping, flip flag, call game event
            cancelFlag = false;
            isWrapped = true;

            GameManager.Instance.PinataDied();
            //PLAY ANIMATION: change appearance to wrapped present
            //PLAY SOUND: Finish wrapping
        }
        
    }

}
