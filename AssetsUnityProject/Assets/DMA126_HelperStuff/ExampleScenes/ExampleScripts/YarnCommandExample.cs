using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Yarn.Unity;//<-- Make sure you have this

public class YarnCommandExample : MonoBehaviour
{

    [YarnCommand("exampleYarnCommand")]
    public void exampleYarnCommand(float numberArg, string stringArg)
    {
        Debug.Log("YARN COMMAND RECEIVED. Pass args were: " + numberArg + ", and '" + stringArg + "'");
    }
}
