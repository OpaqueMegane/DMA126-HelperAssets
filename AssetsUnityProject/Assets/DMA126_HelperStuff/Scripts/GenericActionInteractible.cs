using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericActionInteractible : MonoBehaviour, IPlayerInteractible
{
    public string interactionDescription = "Interact";
    public bool oneTime = false;
    
    bool _actionAllowed = true;

    public UnityEvent action;


    public bool GetInteractionAllowed()
    {
        return _actionAllowed;
    }

    public string GetInteractionDescription()
    {
        return interactionDescription;
    }

    public void Interact()
    {
        if (oneTime)
        {
            _actionAllowed = false;
        }
        action.Invoke();
    }
}
