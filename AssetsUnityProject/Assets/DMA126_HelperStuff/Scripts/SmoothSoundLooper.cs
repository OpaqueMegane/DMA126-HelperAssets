using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SmoothSoundLooper : MonoBehaviour
{
    int MAX_CONCURRENT_SOUNDS = 5;
    List<AudioSource> _sources = new List<AudioSource>();
    public AudioClip startSound;
    AudioClip _desiredClip = null;
    public float crossfadeTime = 2;


    [Header("                                                   2D <--> 3D")]
    [Range(0, 1)]
    public float spatialBlend = 0;

    [Space(10)]
    [SerializeField] List<SoundWithID> _soundWithIDs = new List<SoundWithID>();

    void Awake()
    {
        for (int i = 0; i < MAX_CONCURRENT_SOUNDS; i++)
        {
            _sources.Add(this.gameObject.AddComponent<AudioSource>());
            _sources[i].loop = true;
        }

        if (startSound != null)
        {
            this.SetSound(startSound);
        }
    }

    public void Stop()
    {
        this.SetSound(null);
    }

    public void SetSound(AudioClip sound)
    {
        bool needToUpdate = sound != _desiredClip && sound != null;
        _desiredClip = sound;
        if (!needToUpdate)
        {
            return;
        }

        AudioSource fallbackSource = null;
        AudioSource finalOutput = null;
        foreach (AudioSource source in _sources)
        {
            source.spatialBlend = spatialBlend;
            //if we don't have a final source, take the first one without a clip,
            //and always take a source already with the existing clip
            if ((source.clip == null && finalOutput == null) ||  source.clip == _desiredClip)
            {
                finalOutput = source;
            }

            //pick the current quietest source as a backup
            if (fallbackSource == null || fallbackSource.volume > source.volume)
            {
                fallbackSource = source;
            }
        }

        finalOutput = finalOutput != null ? finalOutput : fallbackSource;
        if (finalOutput != null)
        {
            finalOutput.clip = _desiredClip;
            finalOutput.volume = 0;
            if (!finalOutput.isPlaying)
            {
                finalOutput.Play();
            }
        }
    }

    public void SetSoundWithID(string soundId)
    {
        foreach(var soundWithId in _soundWithIDs)
        {
            if (soundWithId.id == soundId)
            {
                this.SetSound(soundWithId.sound);
                break;
            }
        }
    }

    void Update()
    {
        foreach (AudioSource source in _sources)
        {
            if (source.clip != null)
            {
                bool shouldPlay = source.clip == _desiredClip;
                if ( crossfadeTime < 0)
                {
                    source.volume = shouldPlay ? 1 : 0;
                }
                else
                {
                    source.volume = Mathf.MoveTowards(source.volume, shouldPlay ? 1 : 0, Time.deltaTime / crossfadeTime);
                }
                
                if (!shouldPlay && source.volume <= .01)
                {
                    source.Stop();
                    source.clip = null;
                }
            }
        }
    }

    [System.Serializable]
    public class SoundWithID
    {
        public string id;
        public AudioClip sound;
    }

    #region test code
    //public AudioClip testClip;
    //public string testClipID;

    //[ContextMenu("testApply")]
    //void testApply() => this.SetSound(testClip);

    //[ContextMenu("testApplyID")]
    //void testApplyID() => this.SetSound(testClipID);
    #endregion
}
