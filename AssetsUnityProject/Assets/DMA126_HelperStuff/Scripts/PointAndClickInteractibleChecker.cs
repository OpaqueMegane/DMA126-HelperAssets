using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Yarn.Unity;

public class PointAndClickInteractibleChecker : MonoBehaviour
{
    IPlayerInteractible _activeInteractible = null;
    [SerializeField] TMP_Text _interactionText;

    float _timeDialogueLastActive = 0;

    DialogueRunner _dialogueRunner;

    float _timeOfLastInteract = 0;

    public Camera overrideCamera;
    public LayerMask layersToCheck = ~0;

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
            if (Input.GetMouseButtonDown(0))
            {
                _timeOfLastInteract = Time.time;
                _activeInteractible.Interact();
            }
        }


    }

    public IPlayerInteractible GetInteractible(Component other)
    {
        var interactible = other.GetComponent<IPlayerInteractible>();
        return interactible;
    }

    //void HandleInteractibleEntered(IPlayerInteractible interactible)
    //{
    //    if (interactible != null && !_currentInteractibles.Contains(interactible))
    //    {
    //        _currentInteractibles.Add(interactible);
    //        RefreshCurrentInteractible();
    //    }
    //}

    //void HandleInteractibleExited(IPlayerInteractible interactible)
    //{
    //    if (interactible != null)
    //    {
    //        _currentInteractibles.Remove(interactible);
    //        RefreshCurrentInteractible();
    //    }

    //}


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

        var cam = overrideCamera != null ? overrideCamera : Camera.main;

        bool hitSomething = Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out var hitInfo, float.MaxValue, this.layersToCheck, QueryTriggerInteraction.Collide);

        var interactible = hitSomething ? GetInteractible(hitInfo.collider) : null;

        if (interactible != null)
        {
            if (!interactibleOK(interactible))
            {
                if (shouldRemoveInteractible(interactible))
                {
                    interactible = null;
                }
            }
            else
            {
                _activeInteractible = interactible;
            }
        }

        if (_interactionText != null)
        {
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
    }

    private void OnDestroy()
    {
        if (_interactionText != null)
        {
            _interactionText.text = "";
        }
    }
}