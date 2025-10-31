using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Yarn.Unity;

public class InteractibleChecker : MonoBehaviour
{
    List<IPlayerInteractible> _currentInteractibles = new();
    IPlayerInteractible _activeInteractible = null;
    [SerializeField] TMP_Text _interactionText;

    float _timeDialogueLastActive = 0;

    DialogueRunner _dialogueRunner;

    float _timeOfLastInteract = 0;

    public KeyCode interactKey = KeyCode.Space;
    public string interactKeyAlt = "e";

    private void Start()
    {
        _dialogueRunner = GameObject.FindFirstObjectByType<DialogueRunner>();
    }

    private void Update()
    {
        if (_dialogueRunner != null && _dialogueRunner.IsDialogueRunning)
        {
            _timeDialogueLastActive = Time.time;
        }

        bool tooSoonSinceDialogueCompleted = Time.time - _timeDialogueLastActive < .1f;
        bool tooSoonSinceLastInteract = Time.time - _timeOfLastInteract < .1f;
        //int sanity = 100;
        //while (sanity > 0 && _activeInteractible != null && !interactibleOK(_activeInteractible))
        //{
        //    sanity--;
        //    RefreshCurrentInteractible();
        //}
        RefreshCurrentInteractible();

        if (_activeInteractible != null && !tooSoonSinceDialogueCompleted && !tooSoonSinceLastInteract)
        {
            if (Input.GetKeyDown(interactKey) || Input.GetKeyDown(interactKeyAlt))
            {
                _timeOfLastInteract = Time.time;
                _activeInteractible.Interact();
            }
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        HandleInteractibleEntered(GetInteractible(other));
    }

    private void OnTriggerExit(Collider other)
    {
        HandleInteractibleExited(GetInteractible(other));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleInteractibleEntered(GetInteractible(other));
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        HandleInteractibleExited(GetInteractible(other));
    }

    public IPlayerInteractible GetInteractible(Component other)
    {
        var interactible = other.GetComponent<IPlayerInteractible>();
        return interactible;
    }

    void HandleInteractibleEntered(IPlayerInteractible interactible)
    {
        if (interactible != null && !_currentInteractibles.Contains(interactible))
        {
            _currentInteractibles.Add(interactible);
            RefreshCurrentInteractible();
        }
    }

    void HandleInteractibleExited(IPlayerInteractible interactible)
    {
        if (interactible != null)
        {
            _currentInteractibles.Remove(interactible);
            RefreshCurrentInteractible();
        }

    }


    bool shouldRemoveInteractible(IPlayerInteractible i)
    {
        return i == null || !i.gameObject.activeInHierarchy;
    }

    bool interactibleOK(IPlayerInteractible i)
    {
        return !shouldRemoveInteractible(i) && i.GetInteractionAllowed();
    }

    void RefreshCurrentInteractible()
    {
        _activeInteractible = null;

        for (int i = 0; i < _currentInteractibles.Count; i++)
        {
            var interactible = _currentInteractibles[i];
            if (!interactibleOK(interactible))
            {
                if (shouldRemoveInteractible(interactible))
                {
                    _currentInteractibles.RemoveAt(i);
                    i--;
                }
            }
            else if (_activeInteractible == null)
            {
                _activeInteractible = interactible;
            }
        }

        _interactionText.enabled = _activeInteractible != null;

        if (_activeInteractible != null)
        {
            _interactionText.text = _activeInteractible.GetInteractionDescription();
        }
        else
        {
            _interactionText.text = "";
        }
    }

    private void OnDestroy()
    {
        if (_interactionText != null)
        {
            _interactionText.text = "";
        }
    }
}

public interface IPlayerInteractible
{
    public string GetInteractionDescription();
    void Interact();

    bool GetInteractionAllowed();
    GameObject gameObject { get; }
    bool enabled { get; set; }
}