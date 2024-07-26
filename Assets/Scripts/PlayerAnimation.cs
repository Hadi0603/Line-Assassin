using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerActions.isRunning == true)
        {
            animator.SetBool("isRunning", true);
        }
        if(PlayerActions.isRunning == false)
        {
            animator.SetBool("isRunning", false);
        }
        if (PlayerActions.isAttacking == true)
        {
            animator.SetBool("isAttacking", true);
        }
        if(PlayerActions.isAttacking == false)
        {
            animator.SetBool("isAttacking", false);
        }
    }
}
