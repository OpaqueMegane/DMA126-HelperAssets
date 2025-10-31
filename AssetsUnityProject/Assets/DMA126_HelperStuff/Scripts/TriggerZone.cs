using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTriggerZone : MonoBehaviour
{
    public UnityEvent EnterActions = new();
    public UnityEvent ExitActions = new();

    public bool ignoreTriggerOnEnable = false;
    int onEnableFrame = 0;

    private void OnEnable()
    {
        onEnableFrame = Time.frameCount;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool ignore = ignoreTriggerOnEnable && Time.frameCount <= onEnableFrame + 10;
        if (!ignore && isPlayerCollider(other))
        {
            EnterActions.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPlayerCollider(other))
        {
            ExitActions.Invoke();
        }
    }

    bool isPlayerCollider(Collider other)
    {
        return other != null && other.gameObject.CompareTag("Player");
       
    }
}
