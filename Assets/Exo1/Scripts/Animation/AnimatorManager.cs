using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    PlayerController playerController;
    int horizontal;
    int vertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
     //   playerLocomotion = GetComponent<PlayerLocomotion>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void PlayTargetAnimation(string targetAnimation, bool IsInteracting)
    {
        PlayTargetAnimation(targetAnimation, IsInteracting, false);
    }

    public void PlayTargetAnimation(string targetAnimation, bool IsInteracting, bool useRootMotion)
    {
        animator.SetBool("UsingRootMotion", useRootMotion);
        animator.CrossFade(targetAnimation, 0.2f);
    }

    public void UpdateAnimatorValues(float horizontalMovement, float VerticalMovement, bool sprinting, bool walking)
    {

        float snappedHorizontal;
        float snappedVertical;

        #region Animation snapping
        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            snappedHorizontal = 0.5f;
        else if (horizontalMovement > 0.55f)
            snappedHorizontal = 1;
        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            snappedHorizontal = -0.5f;
        else if (horizontalMovement < -0.55f)
            snappedHorizontal = -1;
        else snappedHorizontal = 0;

        if (VerticalMovement > 0 && VerticalMovement < 0.55f)
            snappedVertical = 0.5f;
        else if (VerticalMovement > 0.55f)
            snappedVertical = 1;
        else if (VerticalMovement < 0 && VerticalMovement > -0.55f)
            snappedVertical = -0.5f;
        else if (VerticalMovement < -0.55f)
            snappedVertical = -1;
        else snappedVertical = 0;
        #endregion Animation snapping

        if (sprinting && VerticalMovement > 0)
        {
            snappedVertical = 2;
        }
        else if (walking && VerticalMovement > 0)
        {
            snappedVertical = 0.5f;
        }

        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
    }

    private void OnAnimatorMove()
    {
        if (playerController.UsingRootMotion)
        {
            playerController.rigidBody.linearDamping = 0;
            Vector3 deltaPosition = animator.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / Time.deltaTime;
            playerController.rigidBody.angularVelocity = velocity;

        }
    }
}
