using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingFollower : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject thingToFollow;
    public float moveSpeed = 3;
    public float followDistance = 1.5f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        bool tooFarAway = Vector3.Distance(rb.position, thingToFollow.transform.position) > followDistance;
        if (tooFarAway)
        {
            Vector3 desiredPosition = Vector3.MoveTowards(
                rb.position,
                thingToFollow.transform.position,
                moveSpeed * Time.deltaTime);

            rb.MovePosition(desiredPosition);
        }
    }
}
