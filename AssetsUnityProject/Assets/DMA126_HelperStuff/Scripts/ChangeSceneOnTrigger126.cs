using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTrigger126 : MonoBehaviour
{
    public string sceneName = "GameOver";
    public bool disableOnTriggerEnter = false;
    public enum SceneChangeMode { UnloadOthers, DeactivatateOthers}
    public SceneChangeMode sceneChangeMode = SceneChangeMode.UnloadOthers;

    void OnTriggerEnter(Collider other)
    {
        if (disableOnTriggerEnter)
        {
            return; //exit function, do nothing further
        }

        if (other.gameObject.CompareTag("Player"))
        {
            //This code will execute if the colliding
            //object has the Player tag
            ChangeScenes();
        }
    }

    public void ChangeScenes()
    {
        if (sceneChangeMode == SceneChangeMode.UnloadOthers)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            SceneUtils126.LoadOrUnpauseScene(sceneName);
        }
    }
}
