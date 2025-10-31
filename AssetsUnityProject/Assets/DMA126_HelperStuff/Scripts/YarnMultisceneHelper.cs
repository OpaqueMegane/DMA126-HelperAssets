using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class YarnMultisceneHelper : MonoBehaviour
{
    public bool destroyRedundantEventSystems = true;
    void Start()
    {
        SceneManager.sceneLoaded += sceneLoaded;
    }

    private void sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (!destroyRedundantEventSystems)
        {
            return;
        }

        foreach(var es in GameObject.FindObjectsOfType<EventSystem>())
        {
            if (!es.transform.IsChildOf(this.transform))
            {
                Destroy(es.gameObject);
            }
        }
    }
}
