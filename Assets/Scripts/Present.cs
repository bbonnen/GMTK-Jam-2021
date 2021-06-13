using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : MonoBehaviour
{
    private float wrappingTime;
    public bool cancelFlag { get; private set; }
    public bool isWrapping { get; private set; }
    public bool isWrapped { get; private set; }

    public Transform statusBar;
    public GameObject statusBarParent;
    private float wrappingTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        isWrapping = false;
        isWrapped = false;
        cancelFlag = false;
        wrappingTime = 2.0f;
        statusBarParent.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isWrapping && !isWrapped)
        {
            wrappingTimer += Time.deltaTime;
            statusBar.localScale = new Vector3(Mathf.Lerp(0, 1, wrappingTimer / wrappingTime), statusBar.localScale.y, statusBar.localScale.z);
        }
    }

    public void StartWrapping()
    {
        if (!isWrapped && !isWrapping)
        {
            isWrapping = true;
            wrappingTimer = 0;
            statusBarParent.SetActive(true);
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

            statusBarParent.SetActive(false);
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
            statusBarParent.SetActive(false);

            GameManager.Instance.PresentWrapped();
            //PLAY ANIMATION: change appearance to wrapped present
            //PLAY SOUND: Finish wrapping
        }
        
    }

}
