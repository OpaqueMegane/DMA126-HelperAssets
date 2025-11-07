using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-9999999)]
public class AutoSingletonGameObject : MonoBehaviour
{
    static Dictionary<string, AutoSingletonGameObject> _instances = new();

    public string key = "Dialogue System_126"; //won't be affected by duplications, like the raw 'this.name' will

    void Awake()
    {
        if (_instances.ContainsKey(key))
        {
            this.gameObject.SetActive(false); //<-- IMPORTANT!!!!
            //The above deactivate prevents GameObject.FindFirtObjectByType from
            //grabbing a soon-to-be, but not-immediately destroyed redundant object
            //Could use DestroyImmediate as well, but discouraged by Unity
            
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject.gameObject);
        _instances[key] = this;
    }
}
