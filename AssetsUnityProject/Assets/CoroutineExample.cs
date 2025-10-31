using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineExample : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(myRoutine());
    }

    IEnumerator myRoutine()
    {
        Debug.Log("Start!");
        //yield return new WaitForSeconds(2);
        //Debug.Log("2 sec later!");

        //yield return new WaitForSeconds(5);
        //Debug.Log("7 sec later!");

        //yield return new WaitForSeconds(3);
        //Debug.Log("10 sec later!");

        yield return StartCoroutine(waitForNKeyPress());
        Debug.Log("Pressed the n-key 1x");

        yield return StartCoroutine(waitForNKeyPress());
        Debug.Log("Pressed the n-key 2x");

        Debug.Log("FINALLY DONE");

    }

    IEnumerator waitForNKeyPress()
    {
        //waiting for a keypress
        while (Input.GetKeyDown(KeyCode.N) == false)
        {
            yield return null;
        }
        //wait a frame after the key press to prevent multiple keys detected in one frame
        yield return null;
    }
}
