using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class RBPlayer : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Walk speed of the player.")]
    public float walkSpeed = 6f;
    [Tooltip("Run speed of the player.")]
    public float runSpeed = 12f;
    [Tooltip("Crouch movement speed.")]
    public float crouchSpeed = 3f;
    [Tooltip("Jumping power.")]
    public float jumpPower = 7f;
    [Tooltip("Gravity applied to the player.")]
    public float gravity = 10f;

    [Header("Camera Settings")]
    public Camera playerCamera;
    [Tooltip("Speed of camera rotation.")]
    public float lookSpeed = 2f;
    [Tooltip("Maximum vertical camera angle.")]
    public float lookXLimit = 45f;

    [Header("Crouching Settings")]
    [Tooltip("Default height of the player.")]
    public float defaultHeight = 2f;
    [Tooltip("Height of the player while crouching.")]
    public float crouchHeight = 1f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    private bool canMove = true;
    public bool canRotate = true;
    public AnimatorManager animatorManager;
    [SerializeField] bool correctInclination = true;
    [Range(0.01f, .5f)][SerializeField] float inclinationCorrectionSpeed = .1f;

    public static RBPlayer instance;

    public Material material;

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

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedZ = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedZ);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.C) && canMove)
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;

        }
        else
        {
            characterController.height = defaultHeight;
            walkSpeed = 6f;
            runSpeed = 12f;
        }

        characterController.Move(moveDirection * Time.deltaTime);
     //   if (!isJumping) 
            animatorManager.UpdateAnimatorValues(curSpeedZ, curSpeedX, false, false);
        if (canRotate)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        
    }

    private void FixedUpdate()
    {
        CorrectInclination();
    }

    void CorrectInclination()
    {
        if (correctInclination)
        {

            //try to make player stand straight when tilted
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(0, transform.eulerAngles.y, 0),
                inclinationCorrectionSpeed
            );

        }
    }
    void SendJumpToAnimator()
    {
        animatorManager.animator.SetBool("IsGrounded", false);
        animatorManager.animator.SetBool("Jumping", true);
        animatorManager.PlayTargetAnimation("Jump_Run", false, false);
    }

    public void LockMovements()
    {
        canMove = false;    
    }
    public void LockRotate()
    {
        canRotate = false;
    }
    public void UnlockMovements()
    {
        canMove = true;
    }

    public void UnlockRotate()
    {
        canRotate = true;
    }

}

