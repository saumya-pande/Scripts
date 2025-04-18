using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    private Animator animator;
    int isWalkingHash;
    int isRunningHash;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash =  Animator.StringToHash("isWalking");
        isRunningHash =  Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    void Update()
    {
        bool isrunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");

        // if player presses w key
        if (!isWalking && forwardPressed)
        {
            // then set the isWalking boolean to be true
            animator. SetBool(isWalkingHash, true);
        }
        
        // if player is not pressing w key
        if (isWalking && !forwardPressed)
        {
            // then set the isWalking boolean to be false
            animator.SetBool(isWalkingHash, false);
        }
       

        // if player is walking and presses left shift
        if (!isrunning && (forwardPressed && runPressed))
        {
            // then set the isRunning boolean to be true
            animator.SetBool(isRunningHash, true);
        }

        

        // if player stops running or stops walking
        if (isrunning && (!forwardPressed || !runPressed))
        {
            // then set the isRunning boolean to be false
            animator.SetBool(isRunningHash, false);
            animator.SetBool(isWalkingHash, true);
        }        
    }
}
