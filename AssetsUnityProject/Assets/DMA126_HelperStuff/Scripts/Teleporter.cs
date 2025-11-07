using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destination;
    
    public bool preserveMomentum = false;

    public bool setRotation = true;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CharacterController cc = other.GetComponent<CharacterController>();
            bool needCCReenable = false;
            if (cc != null && cc.enabled)
            {
                needCCReenable = true;
                cc.enabled = false;
            }
            other.gameObject.transform.position = destination.position;
            if (setRotation)
            {
                other.gameObject.transform.rotation = destination.rotation;
            }

            if (preserveMomentum == false)
            {
                var rb = other.GetComponent<Rigidbody>();
                if (rb != null && !rb.isKinematic)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }

            if (needCCReenable)
            {
                cc.enabled = true;
            }
    
        }
    }

    //public interface ITeleportable
    //{
    //    public void Teleport(Vector3 position,  Quaternion rotation);
    //}
}
