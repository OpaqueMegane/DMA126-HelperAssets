using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class SimpleMover2D : MonoBehaviour
{
    Rigidbody2D _rb;
    public float speed = 5;

    DialogueRunner _dialogueRunner;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _dialogueRunner = GameObject.FindAnyObjectByType<DialogueRunner>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_dialogueRunner != null && _dialogueRunner.IsDialogueRunning)
        {
            return;
        }

        Vector2 move = new(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //transform.position += move * Time.deltaTime * speed;
        _rb.MovePosition(_rb.position + move * Time.deltaTime * speed);
    }
}
