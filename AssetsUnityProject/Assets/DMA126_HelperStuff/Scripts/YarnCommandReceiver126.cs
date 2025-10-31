using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

public class YarnCommandReceiver126 : MonoBehaviour
{
    [YarnCommand]
    public void DoCustomCommand126(string commandId)
    {
        try
        {
            executeCustomCommand(commandId);
        } 
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }
    }

    [SerializeField] CustomCommand[] _customCommands = { };

    void executeCustomCommand(string commandId)
    {
        bool handled = false;
        foreach (var command in _customCommands)
        {
            if (command.commandId == commandId)
            {
                try
                {
                    handled = true;
                    command.action.Invoke();
                }
                catch (System.Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }

        if (!handled)
        {
            Debug.LogError($"Couldn't find matching custom command for '{commandId}' on '{this.name}'", this);
        }
    }

    public void Teleport(Transform destination)
    {
        this.transform.position = destination.position;
    }

    [System.Serializable]
    public class CustomCommand
    {
        public string commandId;
        public UnityEvent action;
    }
}
