using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAnimationControll : MonoBehaviour
{
    Animator animator;
    float curruntTime = 3f;
    [SerializeField]
    private float repeatTime = 3f;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        curruntTime += Time.deltaTime;
        if (curruntTime > repeatTime)
        {
            ChangeAni();
            curruntTime = 0f;
        }
    }

    private void ChangeAni()
    {
        int randInt = Random.Range(0, 3);

        switch (randInt)
        {
            case 0: animator.SetFloat("Blend", 0f); break; // idle
            case 1: animator.SetFloat("Blend", 0.33f); break; // eating
            case 2: animator.SetFloat("Blend", 0.66f); break; // sitting
            case 3: animator.SetFloat("Blend", 1f); // walk
                break;
        }

    }
}
