using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

public class DisableForDialogue : MonoBehaviour
{
    //public List<Component> toDisable = new List<Component>();
    DialogueRunner _dialogueRunner;
    public UnityEvent onDialogueStart = new();
    public UnityEvent onDialogueEnd = new();
    void Start()
    {

        _dialogueRunner = GameObject.FindAnyObjectByType<DialogueRunner>();
        _dialogueRunner.onDialogueStart.AddListener(() => HandleDialogueStateChange(true));
        _dialogueRunner.onDialogueComplete.AddListener(() => HandleDialogueStateChange(false));
    }

    void HandleDialogueStateChange(bool dialogueRunning)
    {
        this.StartCoroutine(HandleDialogueStateChangeRoutine(dialogueRunning));
    }
    IEnumerator HandleDialogueStateChangeRoutine(bool dialogueRunning)
    {
        if (dialogueRunning)
        {
            onDialogueStart.Invoke();
        }
        else
        {
            yield return new WaitForSeconds(.05f);
            onDialogueEnd.Invoke();
        }
    }
}
