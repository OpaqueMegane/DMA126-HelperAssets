using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GeneralPurposeTimer : MonoBehaviour
{
    public float secondsElapsed = 0;
    public bool timerRunning = true;
    public float timeLimit = 5;

    public UnityEvent timeoutAction;

    void Update()
    {
        if (timerRunning)
        {
            secondsElapsed += Time.deltaTime;
            if (secondsElapsed >= timeLimit)
            {
                timerRunning = false;
                Debug.Log("Time up!");

                timeoutAction.Invoke();
            }
        }
    }
}
