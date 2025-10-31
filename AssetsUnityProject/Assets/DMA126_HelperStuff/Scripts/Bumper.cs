using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public float bumpSpeed = 10;
    float coolDownCompleteTime = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if  (Time.time <= coolDownCompleteTime)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            coolDownCompleteTime = Time.time + .1f;
            var rb = collision.gameObject.GetComponent<Rigidbody>();


            //Simple
            //rb.velocity -= collision.contacts[0].normal * bumpSpeed;
            
            Vector3 contactNormal = -collision.GetContact(0).normal;
            rb.velocity -= Vector3.Project(rb.velocity, contactNormal);
            rb.velocity += contactNormal * bumpSpeed;
            //Debug.Log(rb.velocity.normalized * bumpSpeed, rb);
        }
    }
}
