using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    private Animator animator;
    int isWalkingHash;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash =  Animator.StringToHash("isWalking");
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPressed = Input.GetKey("w");
        // Handle walking animation
        if (forwardPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        //if player is not pressing w stop 
        if (!forwardPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }
    }
}
