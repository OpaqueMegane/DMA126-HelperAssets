using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarder : MonoBehaviour
{
    public bool horizontalOnly = false;
    Transform cameraTransform;
    public Vector3 offset;

    void LateUpdate()
    {
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }

        if (cameraTransform != null)
        {
            Vector3 look = cameraTransform.forward;
            if (horizontalOnly)
            {
                look.y = 0;
                if (look.sqrMagnitude < .01f)
                {
                    return;
                }
            }
           
            this.transform.rotation = Quaternion.LookRotation(look);
            this.transform.eulerAngles += offset;
        }
    }
}
