using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void SetGrounded(bool value)
    {
        animator.SetBool("Grounded", value);
    }

    public void OnJumpInput()
    {
        animator.SetTrigger("Jump");
    }
    public void OnSlideInput()
    {
        animator.SetTrigger("Slide");
    }
    
}
