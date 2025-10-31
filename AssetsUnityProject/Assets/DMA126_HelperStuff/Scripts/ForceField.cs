using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    public Vector3 force = new Vector3(0, 9.8f, 0);

    Rigidbody playerRigidBody;

    void Start()
    {

    }

    private void FixedUpdate()
    {
        if (playerRigidBody != null)
        {
            playerRigidBody.AddForce(force, ForceMode.Acceleration);
        }
    }


    void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.CompareTag("Player"))
        {
            playerRigidBody = other.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerRigidBody = null;
        }
    }
}
