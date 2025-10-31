using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTest : MonoBehaviour
{
    public float timer1 = 0;
    public float timer2 = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer1 = timer1 + Time.deltaTime;
        timer2 = timer2 + (Time.deltaTime * 2);
    }
}
