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

    //  SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    public static void ReloadScene(string sceneName)
    {
        var targScene = SceneManager.GetSceneByName(sceneName);
        if (targScene.IsValid() && targScene.isLoaded)
        {
            DeactivateScene(targScene, true);
        }
        LoadOrUnpauseScene(sceneName);
    }

    public static void LoadOrUnpauseScene(string sceneName)
    {
        //Deactivate other scenes
        for (int i = 0; i < SceneManager.loadedSceneCount; i++)
        {
            var s = SceneManager.GetSceneAt(i);
            if (s.isLoaded && s.name != sceneName && !_deactivatedScenes.ContainsKey(s.name))
            {
                DeactivateScene(s);
                //SceneManager.UnloadSceneAsync(s);
            }
        }

        if (_deactivatedScenes.ContainsKey(sceneName))
        {
            ReactivateScene(sceneName);
        }
        else
        {
            var targScene = SceneManager.GetSceneByName(sceneName);
            if (targScene.IsValid() && targScene.isLoaded)
            {
                Debug.LogError($"Scene '{sceneName}' was already loaded??? ");
                return;
            }
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }

    public static void DeactivateScene(Scene scene, bool unloadCompletely = false)
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

    public static void ReactivateScene(Scene scene) => ReactivateScene(scene.name);

    public static void ReactivateScene(string sceneName)
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