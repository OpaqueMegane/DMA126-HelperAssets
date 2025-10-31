using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionZone : MonoBehaviour
{
    public float minCollisionSpeed = 0;

    public UnityEvent EnterActions = new();
    public UnityEvent ExitActions = new();



    private void OnCollisionEnter(Collision collision)
    {
        if (isPlayerCollider(collision.collider))
        {
            if (collision.relativeVelocity.magnitude >= minCollisionSpeed)
            {
                EnterActions.Invoke();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (isPlayerCollider(collision.collider))
        {
            ExitActions.Invoke();
        }
    }

    bool isPlayerCollider(Collider other)
    {
        return other != null && other.gameObject.CompareTag("Player");
       
    }
}
