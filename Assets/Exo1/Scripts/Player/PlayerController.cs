using DG.Tweening;
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
    public Material material; // Material for the dissolve effect
                              //   public bool IsInteracting;
    public bool useDoubleJump = false;
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
    public enum CameraTypeEnum { CameraFollowPlayer, PlayerFollowCamera, Advanced, other }
    public CameraTypeEnum cameraType = new CameraTypeEnum();
    public bool LockedCamera = false;
    public static PlayerController instance;
    #region Init
    private void Awake()
    {
        CreateInstance();
    }

    void CreateInstance()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion Init
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;

        
    }

    private void FixedUpdate()
    {   
        MovePlayer();

        if (!LockedCamera)
        {
            cameraOrbit.HandleCameraFollowBackMovement();

        }
    }
    
    bool IsGrounded()
    {
        isOnGround = false;
        isOnGround = Physics.CheckSphere(groundCheck.position, detectionRange, groundLayer);
        if (isOnGround)
        {
         //   isJumping = false;
          //  animatorManager.animator.SetBool("IsDoubleJumping", false);
        }
        animatorManager.animator.SetBool("IsGrounded", isOnGround);
        return isOnGround;
    }

    // Visualiser la zone de d�tection du sol dans l'�diteur
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
    //    if (IsInteracting)
    //        return;

        if (IsGrounded())
        {
        }


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
                if (useDoubleJump) Invoke("InvokeCanDoubleJump", DoubleJumpDelay);
                DoJump();
            }
            else if (useDoubleJump && !isDoubleJump && CanDoubleJump)
            {
                Debug.Log("useDoubleJump && !isDoubleJump && CanDoubleJump");
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
        Debug.Log("SendDoubleJumpToAnimator");
        animatorManager.animator.SetBool("IsDoubleJumping", true);
        animatorManager.PlayTargetAnimation("DoubleJump", false);
    }
    void SendRunJumpToAnimator()
    {
        animatorManager.animator.SetBool("IsGrounded", false);
        animatorManager.animator.SetBool("Jumping", true);
        animatorManager.PlayTargetAnimation("Jump_Run", false);
    }

    void SendJumpToAnimator()
    {
        CanWalk = false;
        animatorManager.animator.SetBool("IsGrounded", false);
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

/*    void OnInteract(InputValue value)
    {
        Debug.Log("OnInteract " + value.ToString());
        float val = value.Get<float>();
        if (val == 1)
            IsInteracting = true;
        else
            IsInteracting = false;
    } */ 
    
    public void KillPlayer()
    {
        StartCoroutine(FadeOutToRespawn());
    }
    public IEnumerator FadeOutToRespawn()
    {
        UiManager.instance.fadingPanel.enabled = true;
        UiManager.instance.fadingPanel.DOFade(1, 1);
        material.DOFloat(1.1f, "_dissolveAmount", 2f).SetEase(Ease.InOutQuad); // Start with dissolve effect
        SoundLauncher.instance.PlayDissolve();

        
        yield return new WaitForSeconds(2);

        // D�sactivation du CharacterController avant de d�placer le joueur
        var controller = PlayerController.instance;
        if (controller != null) controller.enabled = false;

        PlayerController.instance.gameObject.transform.position = GameProgressManager.instance.currentRespawnTransform.position;

        // R�activation du CharacterController apr�s le d�placement
        if (controller != null) controller.enabled = true;

        UiManager.instance.fadingPanel.DOFade(0, 2);
        material.DOFloat(0.1f, "_dissolveAmount", 2f).SetEase(Ease.InOutQuad); // resummoning disolve
        SoundLauncher.instance.PlayDissolve();
        yield return new WaitForSeconds(2);
        UiManager.instance.fadingPanel.enabled = false;
        yield break;
    }
}
