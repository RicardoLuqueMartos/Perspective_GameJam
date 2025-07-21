using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBool : StateMachineBehaviour
{
    public bool ActOnGrounded;
    public string IsGroundedBool;
    public bool IsGroundedStatus;

    public bool ActOnJumping;
    public string IsJumpingBool;
    public bool IsJumpingStatus;

    public bool ActOnUsingRootMotion;
    public string UsingRootMotionBool;
    public bool UsingRootMotionStatus;

    public string IsInteractingBool;
    public bool IsInteractingStatus;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(IsInteractingBool, IsInteractingStatus);
        if (ActOnJumping) animator.SetBool(IsJumpingBool, IsJumpingStatus);
        if (ActOnGrounded) animator.SetBool(IsGroundedBool, IsGroundedStatus);
        if (ActOnUsingRootMotion)
        {
            animator.SetBool(UsingRootMotionBool, UsingRootMotionStatus);
        }
    }
}
