using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class NPCAnimationControl : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void StartTalking()
    {
        animator.SetBool("IsTalking", true);
    }
    public void StopTalking() 
    {
        animator.SetBool("IsTalking", false);
    }
}
