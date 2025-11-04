using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAnimatorStartTime : MonoBehaviour
{
    public float minRandomizedSpeed = .95f;
    public float maxRandomizedSpeed = 1.05f;
    void Start()
    {
        var animator = this.GetComponent<Animator>();
        animator.speed = Random.Range(minRandomizedSpeed, maxRandomizedSpeed);
        animator.Play(animator.GetCurrentAnimatorStateInfo(0).shortNameHash, 0, Random.Range(0f, 1f));   
    }


}
