using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Offset_Module : MonoBehaviour
{
    [SerializeField] public Animator animator; // Reference to the Animator component
    public float animationOffset; // The offset time for the animation

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        SetAnimationOffset(animationOffset);
    }

    void SetAnimationOffset(float offset)
    {
        if (animator == null) return;

        // Get the name hash of the default state
        int stateHash = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;

        // Calculate the normalized time offset
        float normalizedTime = offset / animator.GetCurrentAnimatorStateInfo(0).length;

        // Play the animation at the specified offset
        animator.Play(stateHash, 0, normalizedTime);
    }
    
}
