using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTrigger126 : MonoBehaviour
{
    public string sceneName = "GameOver";
    public bool disableOnTriggerEnter = false;
    public enum SceneChangeMode { Default, Gentle, GentleAndReloadDestination}
    public SceneChangeMode sceneChangeMode = SceneChangeMode.Default;

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
        if (sceneChangeMode == SceneChangeMode.Default)
        {
            SceneManager.LoadScene(sceneName);
        }
        else if (sceneChangeMode == SceneChangeMode.Gentle)
        {
            SceneUtils126.ChangeScenesGentle(sceneName);
        }
        else if (sceneChangeMode == SceneChangeMode.GentleAndReloadDestination)
        {
            SceneUtils126.ChangeScenesGentleAndReloadDestinationScene(sceneName);
        }
    }
}
