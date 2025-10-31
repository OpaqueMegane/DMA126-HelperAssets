using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAnimatorStartTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var animator = this.GetComponent<Animator>();
        animator.Play(animator.GetCurrentAnimatorStateInfo(0).shortNameHash, 0, Random.Range(0f, 1f));   
    }


}
