using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneUtils126
{
    static Dictionary<string, List<System.Tuple<GameObject, bool>>> _deactivatedScenes = new();

    static SceneUtils126()
    {
        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
    }

    private static void SceneManager_sceneUnloaded(Scene scene)
    {
        _deactivatedScenes.Remove(scene.name);
    }

    public static void LoadOrUnpauseScene(string destinationSceneName)
    {
        ChangeScenesGentleFull(destinationSceneName, false);
    }

    public static void ChangeScenesGentle(string destinationSceneName)
    {
        ChangeScenesGentleFull(destinationSceneName, false);
    }

    public static void ChangeScenesGentleAndReloadDestinationScene(string destinationSceneName)
    {
        ChangeScenesGentleFull(destinationSceneName, true);
    }

    static void ChangeScenesGentleFull(string destinationSceneName, bool reloadDestScene)
    {
        //Deactivate other scenes
        for (int i = 0; i < SceneManager.loadedSceneCount; i++)
        {
            var s = SceneManager.GetSceneAt(i);
            if (s.isLoaded && !_deactivatedScenes.ContainsKey(s.name))
            {
                if (s.name != destinationSceneName)
                {
                    DeactivateScene(s);
                }
            }

            //Force reload the destination scene if necessary
            if (reloadDestScene && s.name == destinationSceneName)
            {
                DeactivateScene(s, true);
                _deactivatedScenes.Remove(destinationSceneName);
            }
        }

        if (_deactivatedScenes.ContainsKey(destinationSceneName))
        {
            ReactivateScene(destinationSceneName);
        }
        else
        {
            var targScene = SceneManager.GetSceneByName(destinationSceneName);
            if (targScene.IsValid() && targScene.isLoaded)
            {
                Debug.LogError($"Scene '{destinationSceneName}' was already loaded??? ");
                return;
            }
            SceneManager.LoadScene(destinationSceneName, LoadSceneMode.Additive);
        }
    }

    static void DeactivateScene(Scene scene, bool unloadCompletely = false)
    {
        if (!unloadCompletely)
        {
            _deactivatedScenes[scene.name] = new();
        }

        foreach (var obj in scene.GetRootGameObjects())
        {
            try
            {
                _deactivatedScenes[scene.name].Add(new(obj, obj.activeSelf));
                obj.gameObject.SetActive(false);
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
            }
        }

        if (unloadCompletely)
        {
            SceneManager.UnloadSceneAsync(scene);
        }
    }

    static void ReactivateScene(Scene scene) => ReactivateScene(scene.name);

    static void ReactivateScene(string sceneName)
    {
        if (_deactivatedScenes.ContainsKey(sceneName))
        {
            var activeList = _deactivatedScenes[sceneName];
            _deactivatedScenes.Remove(sceneName);

            foreach (var objAndActive in activeList)
            {
                if (objAndActive.Item1 != null)
                {
                    objAndActive.Item1.SetActive(objAndActive.Item2);
                }
            }
            
            if (SceneManager.GetActiveScene().name != sceneName)
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
            }
        }
    }
}