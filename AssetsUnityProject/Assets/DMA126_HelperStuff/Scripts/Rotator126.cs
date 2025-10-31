using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator126 : MonoBehaviour

{
    public UpdateMode updateMode = UpdateMode.Update;
    public Vector3 rotation = new Vector3(0,45,0);   

    public enum UpdateMode { Update, LateUpdate, FixedUpdate}

    private void Update()
    {
        if (updateMode == UpdateMode.Update)
        {
            Animate();
        }
    }

    private void LateUpdate()
    {
        if (updateMode == UpdateMode.LateUpdate)
        {
            Animate();
        }
    }

    private void FixedUpdate()
    {
        if (updateMode == UpdateMode.FixedUpdate)
        {
            Animate();
        }
    }

    void Animate()
    {
        this.transform.Rotate(rotation * Time.deltaTime);
    }
}
