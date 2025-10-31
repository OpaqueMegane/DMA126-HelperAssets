using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-9999999)]
public class AutoSingletonGameObject : MonoBehaviour
{
    static Dictionary<string, AutoSingletonGameObject> _instances = new();

    void Awake()
    {
        if (_instances.ContainsKey(gameObject.name))
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject.gameObject);
        _instances[this.gameObject.name] = this;
    }
}
