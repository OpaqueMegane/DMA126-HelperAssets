using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public Vector3 launchVelocity = new Vector3(0,10,0);
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody>().velocity += launchVelocity;
        }
    }
}
