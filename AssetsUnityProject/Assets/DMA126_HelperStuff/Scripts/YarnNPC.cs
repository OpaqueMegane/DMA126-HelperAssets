using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnNPC : MonoBehaviour, IPlayerInteractible
{
    public string overrideInteractText = "";
   
    DialogueRunner _dialogueRunner;
    [SerializeField] DialogueReference yarnDialogue;

    IEnumerator Start()
    {
        yield return null;
        _dialogueRunner = GameObject.FindObjectOfType<DialogueRunner>();
    }

    public bool GetInteractionAllowed()
    {
        return _dialogueRunner == null || !_dialogueRunner.IsDialogueRunning;
    }

    public string GetInteractionDescription()
    {
        return !string.IsNullOrEmpty(overrideInteractText) ? overrideInteractText : "Talk";
    }

    public void Interact()
    {
        StartCoroutine(InteractCoroutine());
    }

    public void SetStartNode(string startNode)
    {
        yarnDialogue = new(yarnDialogue.project, startNode);
    }

    public IEnumerator InteractCoroutine()
    {
        yield return null;
        _dialogueRunner.StartDialogue(yarnDialogue.nodeName);
    }
}
