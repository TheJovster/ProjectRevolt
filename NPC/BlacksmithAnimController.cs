using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BlacksmithAnimController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    public void AnimationTrigger() 
    {
        animator.SetTrigger("Greet");
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, transform.position.z));
        Debug.Log("Trigger set");
    }

    public void SetAnimBool() 
    {
        animator.SetBool("Speaking", true);
        Debug.Log("Bool set");
    }

    public void UnsetAnimBool() 
    {
        animator.SetBool("Speaking", false);
        Debug.Log("Bool unset");
    }
}
