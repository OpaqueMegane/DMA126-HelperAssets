using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnFall : MonoBehaviour
{
    Rigidbody rb;
    Vector3 startPosition;
    public float resetHeight = -100;
    void Start()
    {
        startPosition = transform.position;
        rb = this.GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        if (transform.position.y < resetHeight)
        {
            transform.position = startPosition;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
