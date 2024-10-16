using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnimalAnimationControll : MonoBehaviour
{
    Animator animator;
    float curruntTime = 3f;
    [SerializeField]
    private float repeatTime = 3f;
    [SerializeField]
    private float moveSpeed = 3f;
    private bool walkingFlag = false;
    
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
        AnimalMove();
    }

    private void ChangeAni()
    {
        int randInt = Random.Range(0, 4);
        walkingFlag = false; // 걷고 있지 않음

        switch (randInt)
        {
            case 0: animator.SetFloat("Blend", 0f); break; // idle
            case 1: animator.SetFloat("Blend", 0.33f); break; // eating
            case 2: animator.SetFloat("Blend", 0.66f); break; // sitting
            case 3: animator.SetFloat("Blend", 1f); // walk
                walkingFlag = true;
                randInt = Random.Range(0, 360);
                gameObject.transform.eulerAngles = new Vector3(0, randInt, 0);
                break;
        }

    }

    private void AnimalMove()
    {
        if (walkingFlag)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }
}
