using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public bool IsInteracting;
    [SerializeField] private bool CanWalk = false;
    [SerializeField] private bool isOnGround = false;
    public bool isJumping = false;
    public bool isDoubleJump = false;
    [SerializeField] private bool CanDoubleJump = false;
    [SerializeField] private float DoubleJumpDelay = 0.1f;
    [SerializeField] private Transform groundCheck;
    [Range(.3f, .6f)]
    [SerializeField] private float detectionRange = 0;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] public Rigidbody rigidBody;
    [SerializeField] private bool isSprinting = false;
    public bool Dodging = false;
    [Range(1f, 2f)]
    [SerializeField] private float sprintMulti = 1.5f;
    [Range(1f, 50f)]
    [SerializeField] private float speed = 10;
   
    [Range(100f, 500f)]
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float movementHorizontal;
    [SerializeField] private float movementVertical;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CameraOrbit cameraOrbit;
    [SerializeField]
    AnimatorManager animatorManager;
    public bool UsingRootMotion = false;
  
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
    }

    private void FixedUpdate()
    {   
        MovePlayer();
    }
    
    bool IsGrounded()
    {
        isOnGround = false;
        isOnGround = Physics.CheckSphere(groundCheck.position, detectionRange, groundLayer);
        if (isOnGround)
        {
            isJumping = false;
            animatorManager.animator.SetBool("IsDoubleJumping", false);
        }
        return isOnGround;
    }

    // Visualiser la zone de détection du sol dans l'éditeur
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = isOnGround ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, detectionRange);
        }
    }

    public void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementHorizontal = movementVector.x;
        movementVertical = movementVector.y;
    }

    void MovePlayer()
    {
        if (IsInteracting)
            return;

     /*   if (IsGrounded())
        {
            isJumping = false;
        }*/


        float sprintValue = 1;
        if (isSprinting) sprintValue = sprintMulti;
        else sprintValue = 1;

        if (movementHorizontal != 0 || movementVertical != 0)        
            CameraDirectionToPlayer();

        Vector3 movement = new Vector3(movementHorizontal, 0.0f, movementVertical);

        float moveSpeed = speed;
        if (!isJumping && isSprinting) moveSpeed = speed * sprintValue;
        else if (isJumping) moveSpeed = speed * .5f;
        
        transform.Translate(movement * (Time.deltaTime * moveSpeed));
      
        if (!isJumping) animatorManager.UpdateAnimatorValues(movementHorizontal, movementVertical, isSprinting, !isSprinting);
    }

    void CameraDirectionToPlayer()
    {
        cameraOrbit.PlayerMoveInCameraDirection();
    }

    void OnJump(InputValue value)
    {
        bool isGrounded = IsGrounded();

        if (isGrounded)
        {
    //        isJumping = false;
        }

        float val = value.Get<float>();
        if (val == 1 && (isGrounded && isJumping == false)
            || (isJumping && isDoubleJump == false))
        {
            if (!isJumping)
            {
                isJumping = true;
                CanDoubleJump = false;
                Invoke("InvokeCanDoubleJump", DoubleJumpDelay);
                DoJump();
            }
            else if (!isDoubleJump && CanDoubleJump)
            {
                DoDoubleJump(); 
                CanDoubleJump = false;
            }
        }
    }

    void InvokeCanDoubleJump()
    {
        CanDoubleJump = true;
    }

    void DoJump()
    {
        rigidBody.AddForce(Vector3.up * jumpForce);
        if (movementHorizontal == 0 && movementVertical == 0) SendJumpToAnimator();
        else SendRunJumpToAnimator();
    }

    void DoDoubleJump()
    {
        rigidBody.AddForce(Vector3.up * jumpForce);     
        SendDoubleJumpToAnimator();
    }
    void SendDoubleJumpToAnimator()
    {
        animatorManager.animator.SetBool("IsDoubleJumping", true);
    //    animatorManager.PlayTargetAnimation("DoubleJump", false);
    }
    void SendRunJumpToAnimator()
    {
        animatorManager.animator.SetBool("Jumping", true);
        animatorManager.PlayTargetAnimation("Jump_Run", false);
    }

    void SendJumpToAnimator()
    {
        CanWalk = false;
        animatorManager.animator.SetBool("Jumping", true);
        animatorManager.PlayTargetAnimation("Jump_Run", false, false);
    }

    void OnSprint(InputValue value)
    {
        float val = value.Get<float>();
        if (val == 1)
            isSprinting = true;

        else
            isSprinting = false;
    }
}
