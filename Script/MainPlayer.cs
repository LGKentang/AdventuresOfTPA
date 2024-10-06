using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPlayer : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float sprintSpeed;
    float trueSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    public Animator playerAnimator;
    private InteractionArea questInteractableArea;
    private InteractionArea shopInteractableArea;


    bool allowMove;
    Vector3 moveDirection;

    Scene currentScene;

    Rigidbody rb;

    private void Start()
    {

        currentScene = SceneManager.GetActiveScene();
        //UnityEngine.Debug.Log(currentScene.name);
        LockCursor();
        playerAnimator = GetComponent<Animator>();
        questInteractableArea = GameObject.Find("QuestInteractionArea")?.GetComponent<InteractionArea>();
        shopInteractableArea = GameObject.Find("ShopInteractionArea")?.GetComponent<InteractionArea>();

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        trueSpeed = moveSpeed;
        allowMove = true;
        readyToJump = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position + Vector3.up * (playerHeight * 0.5f), Vector3.down, playerHeight * 0.5f + 0.03f, whatIsGround);


        KbInput();
        SpeedControl();
   
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void StopPlayer()
    {
        rb.velocity = Vector3.zero;
        playerAnimator.SetBool("isMoving", false);
        ToggleMovement();
    }

    private void ToggleMovement()
    {
        if (allowMove)
        {
            allowMove = false;
        }
        else
        {
            allowMove = true;
        }
    }

    private void CanvasMovementLock()
    {
        StopPlayer();
        ThirdPersonCam.ToggleRotate();
    }

    private void KbInput()
    {
        if (currentScene.name.Equals("Main_Scene"))
        {
            //UnityEngine.Debug.Log("In Main Scene");
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UnityEngine.Debug.Log("ResMenu");
                CanvasMovementLock();
                allowMove = Resume.ToggleShow();
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (questInteractableArea.IsInInteractableArea())
            {
                UnityEngine.Debug.Log("Quest Area");
                CanvasMovementLock();
                allowMove = QuestCanvas.ToggleShow();
            }
            else if (shopInteractableArea.IsInInteractableArea())
            {
                UnityEngine.Debug.Log("Shop Area");
                CanvasMovementLock();
                allowMove = ShopCanvas.ToggleShow();
            }
        }


        AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!(stateInfo.IsName("NormalAttack01_SwordShield") && stateInfo.normalizedTime < 1f))
            {
                playerAnimator.SetTrigger("primaryTrigger");
            }
            //print("l");
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (!(stateInfo.IsName("NormalAttack02_SwordShield") && stateInfo.normalizedTime < 1f))
            {
                playerAnimator.SetTrigger("secondaryTrigger");
            }
            
            //print("r");
        }

       

        if (allowMove)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
            playerAnimator.SetBool("isMoving", horizontalInput != 0 || verticalInput != 0);

            if (Input.GetKey(KeyCode.Space) && readyToJump && grounded)
            {
                UnityEngine.Debug.Log("jUMPED");

                readyToJump = false;

                Jump();

                Invoke(nameof(ResetJump), jumpCooldown);
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                trueSpeed = sprintSpeed;
                playerAnimator.SetBool("isSprinting", true);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                trueSpeed = moveSpeed;
                playerAnimator.SetBool("isSprinting", false);
            }

        }

        else
        {
            horizontalInput = 0f;
            verticalInput = 0f;
            playerAnimator.SetBool("isMoving", false);
            playerAnimator.SetBool("isSprinting", false);
            
        }

    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
            rb.AddForce(moveDirection.normalized * trueSpeed * 10f, ForceMode.Force);

        else if (!grounded)
            rb.AddForce(moveDirection.normalized * trueSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > trueSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * trueSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }


    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0.5f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    public static void LockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }
    public static void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
