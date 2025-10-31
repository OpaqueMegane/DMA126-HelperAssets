using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldKeyToSing : MonoBehaviour
{
    public AudioClip soundToPlay;
    AudioSource audioSource;
    public float soundTimer = 0;

    void Start()
    {
        //automatically add and set up 
        //an audio source to play our sound clip
        audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = soundToPlay;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            if (soundTimer <= 0)
            {
                //only trigger the sound every .75 seconds
                soundTimer = .75f;

                //randomize the pitch
                audioSource.pitch = Random.Range(.75f, 1.25f);

                audioSource.Play();
            }
        }

        if (soundTimer >= 0)
        {
            soundTimer -= Time.deltaTime;
        }

    }
}
