using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Added on body of the player. Tag name : "CharacterPrefab"
public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void SetIdle()
    {
        animator.SetTrigger("Idle");
    }
    public void SetRun()
    {
        animator.SetTrigger("Run");
        
    }
    public void SetVictory()
    {
        animator.SetTrigger("Victory");
    }
    public void SetRunMultiplier(float multiplier)        
    {
        //changing the speed of run animation by forward speed
        animator.SetFloat("RunMultiplier", multiplier);
    }
}
