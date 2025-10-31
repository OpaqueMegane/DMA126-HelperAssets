using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTrackerBase : MonoBehaviour
{
    public Collider currentTouchingPlayerCollider;
    public List<Collider> currentColliders = new();

    private void Start()
    {
        //if (this.GetComponentsInChildren<Collider>().Length > 1)
        //{
        //    Debug.LogError("This script doesn't support");
        //}
    }

    protected void OnTriggerEnter(Collider other)
    {
        handleCollider(other, true);
    }

    protected void OnTriggerExit(Collider other)
    {
        handleCollider(other, false);
    }


    protected void handleCollider(Collider other, bool enter)
    {
        bool isPlayer = other.gameObject.CompareTag("Player");

        if (isPlayer)
        {
            currentTouchingPlayerCollider = enter ? other : null ;
        }
        else
        {
            if (enter)
            {
                currentColliders.Add(other);
            }
            else
            {
                currentColliders.Remove(other);
            }
        }
    }
}
