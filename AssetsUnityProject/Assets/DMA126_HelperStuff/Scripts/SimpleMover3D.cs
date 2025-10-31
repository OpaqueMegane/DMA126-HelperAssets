using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class SimpleMover3D : MonoBehaviour
{
    Rigidbody _rb;
    public float speed = 5;

    DialogueRunner _dialogueRunner;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _dialogueRunner = GameObject.FindAnyObjectByType<DialogueRunner>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_dialogueRunner != null && _dialogueRunner.IsDialogueRunning)
        {
            return;
        }

        Vector3 move = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //transform.position += move * Time.deltaTime * speed;
        if (move != Vector3.zero)
        {
            if (move.magnitude > 1)
            {
                move.Normalize();
            }
            _rb.MovePosition(_rb.position + move * Time.deltaTime * speed);
        }
    }
}
