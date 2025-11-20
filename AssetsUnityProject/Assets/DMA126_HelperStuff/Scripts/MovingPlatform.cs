using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : TriggerTrackerBase
{
    Vector3 lastPosition = Vector3.zero;
    public Quaternion lastRotation;
    private void Start()
    {
        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }
    private void Update()
    {
        Vector3 delta = transform.position - lastPosition;


        if (this.currentTouchingPlayerCollider != null && !this.currentTouchingPlayerCollider.enabled)
        {
            this.currentTouchingPlayerCollider = null;
        }
        
        if (this.currentTouchingPlayerCollider != null)
        {
            currentTouchingPlayerCollider.transform.position += delta;
            Vector3 diffToPlayer = currentTouchingPlayerCollider.transform.position - this.transform.position;

            var diffRot = Quaternion.Inverse(lastRotation) * this.transform.rotation;
            Vector3 finalDestPos = this.transform.position + diffRot * diffToPlayer;
            var cc = currentTouchingPlayerCollider as CharacterController;
            var safeRotation = Quaternion.Euler(0, diffRot.eulerAngles.y, 0);
            currentTouchingPlayerCollider.transform.rotation *= safeRotation;

            if (cc == null)
            {
                currentTouchingPlayerCollider.transform.position = finalDestPos;
            }
            else
            {
                cc.Move(finalDestPos - cc.transform.position);
            }
        }

        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

}
