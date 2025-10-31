using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterTwoWay : MonoBehaviour
{
    public TeleporterTwoWay destination;
    public float ignoreCollisionEndTime = 0;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (Time.time <= ignoreCollisionEndTime)
        {
            return;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position = destination.transform.position;
            destination.ignoreCollisionEndTime = Time.time + .1f;

            var rb = other.GetComponent<Rigidbody>();
            if (rb != null && !rb.isKinematic)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
