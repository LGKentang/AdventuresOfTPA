using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public FreeLookCamera playerCamera;

    public bool IsBeingControlled;
    bool IsDead;
    bool isFPS;

    [Header("Movement")]
    public float moveSpeed;
    public float sprintSpeed;
    float trueSpeed;
    public float rotationSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Player Object")]
    public Transform player;
    public Transform playerObj;
    public Transform orientation;

    [Header("Health Bar")]
    public HealthBar hb;
    //public HealthBar currHb;

    [Header("Player Name (main|dog|wiz)")]
    public string PlayerName;
    float horizontalInput;
    float verticalInput;
    public Animator playerAnimator;
    Vector3 moveDirection;
    private Rigidbody rb;

    [Header("Wizard")]
    public Camera wizcam;
    public SkinnedMeshRenderer r;
    public Canvas crosshair;

    public Ally currentAlly;

    public List<GameObject> enemies;
    public List<EnemyController> enemiesAttr;
    Vector3 inputDir;

    AStar astar;
    public AStar_Grid ag;

    Transform seekerContainer;
    bool playerAdded = false;
    //bool playerRemoved = false;

    private void Start()
    {
        if (PlayerName == "main")
        {
            currentAlly = new Tim();
        }
        else if (PlayerName == "dog")
        {
            currentAlly = new Patrick();
        }
        else if (PlayerName == "wiz")
        {
            currentAlly = new Araskiewicz();
            wizcam.gameObject.SetActive(false);
            playerCamera.gameObject.SetActive(false);
            isFPS = true; r.enabled = true;
            crosshair.gameObject.SetActive(false);
        }
        else
        {
            throw new MissingComponentException();
        }
        hb.SetMaxHealth(currentAlly.Health);
        //currHb.SetMaxHealth(currentAlly.Health);
        astar = gameObject.AddComponent<AStar>();

        //playerAdded = false;
        //playerRemoved = false;

        moveSpeed *= currentAlly.MovementSpeed;
        sprintSpeed *= currentAlly.MovementSpeed;

        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        IsBeingControlled = false;
        rb.freezeRotation = true;
        trueSpeed = moveSpeed;
        readyToJump = true;
        IsDead = false;

        astar.grid = ag;
        //List<Transform> t = new List<Transform>();
        //foreach (GameObject go in enemies)
        //{
        //    t.Add(go.transform);

        //}
        //playerAdded = false;

        //astar.assignTarget(t);
        //astar.addSeek(player);
        //astar.Assign();
        //print(astar.seekers.Count);
    }

    public bool getDeath()
    {
        return IsDead;
    }

    private void Update()
    {
        UpdateHealth();

        if (currentAlly.CheckDeath())
        {
            Destroy(gameObject);
            gameObject.SetActive(false);
            IsDead = true;
        }
        else
        {
            //print(astar.seekers.Count);
            if (IsBeingControlled)
            {
                astar.toldToSeek = false;
                //if (astar.seekers.Count > 0 && playerAdded) astar.removeSeek(player);
                //playerAdded = false;

                
                grounded = Physics.Raycast(transform.position + Vector3.up * (playerHeight * 0.5f), Vector3.down, playerHeight * 0.5f + .1f, whatIsGround);
                KbInput();

                //print(grounded);

                SpeedControl();

                if (grounded)
                {
                    rb.drag = groundDrag;
                }
                else
                {
                    rb.drag = 0;
                }

            }
            else
            {
                //if (!playerAdded)
                {
                    astar.toldToSeek = true;
                    //astar.addSeek(player);
                    //print("Adding player");
                    //print(player);
                }

                //playerAdded = true;
            }
        }  

    }

    void UpdateHealth()
    {
        hb.SetHealth(currentAlly.Health);
        //currHb.SetHealth(currentAlly.Health);
    }

    void debugGroundDetection()
    {
        if (readyToJump && grounded)
        {
            print("Ready to jump");
        }
        else if (readyToJump)
        {
            print("Jump = true, Grounded = false");
        }
        else
        {
            print("Jump = false, Grounded = true");
        }
    }

    private void KbInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        playerAnimator.SetBool("isMoving", horizontalInput != 0 || verticalInput != 0);
        //Debug.Log(string.Format("{0} {1}", readyToJump, grounded));

        AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (PlayerName == "main")
            {
                if (!(stateInfo.IsName("NormalAttack01_SwordShield") && stateInfo.normalizedTime < 1f) && !(stateInfo.IsName("NormalAttack02_SwordShield") && stateInfo.normalizedTime < 1f))
                {
                    playerAnimator.SetTrigger("primaryTrigger");
                    float maxDistance = 3f;
                    Quaternion characterRotation = playerObj.transform.rotation;
                    float initialHeightOffset = 1f;
                    Vector3 raycastOrigin = playerObj.transform.position + new Vector3(0, initialHeightOffset, -1);

                    RaycastHit hit;
                    if (Physics.Raycast(raycastOrigin, characterRotation * Vector3.forward, out hit, maxDistance))
                    {
                        GameObject hitObject = hit.collider.gameObject;
                        if (hitObject.transform != playerObj)
                        {
                            Debug.DrawLine(raycastOrigin, hit.point, Color.red, 0.5f);
                            Debug.Log("Object in front: " + hitObject.name);

                            int index = enemies.IndexOf(hitObject);

                            if (enemies.IndexOf(hitObject) != -1)
                            {
                                enemiesAttr[index].enemy.Health -= currentAlly.PrimaryDamage;
                            }


                        }

                    }

                    Debug.DrawRay(raycastOrigin, characterRotation * Vector3.forward * maxDistance, Color.red, 0.5f);

                }
            }
            else if (PlayerName == "wiz")
            {
                if (wizcam.gameObject.activeSelf)
                {
                    playerAnimator.SetTrigger("primaryTrigger");
                    RaycastHit hit;
                    if (Physics.Raycast(wizcam.transform.position, wizcam.transform.forward, out hit, 50f))
                    {
                        //Debug.Log(hit.transform.name);
                        GameObject hitObject = hit.collider.gameObject;
                        int index = enemies.IndexOf(hitObject);
                        if (enemies.IndexOf(hitObject)!=-1)
                        {
                            enemiesAttr[index].enemy.Health -= currentAlly.PrimaryDamage;
                        }
                    }
                }
            }
            //print("l");
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (PlayerName == "main")
            {
                if (!(stateInfo.IsName("NormalAttack02_SwordShield") && stateInfo.normalizedTime < 1f) && !(stateInfo.IsName("NormalAttack01_SwordShield") && stateInfo.normalizedTime < 1f))
                {
                    playerAnimator.SetTrigger("secondaryTrigger");
                    float maxDistance = 3f;
                    Quaternion characterRotation = playerObj.transform.rotation;
                    float initialHeightOffset = 1f;
                    Vector3 raycastOrigin = playerObj.transform.position + new Vector3(0, initialHeightOffset, -1);

                    RaycastHit hit;
                    if (Physics.Raycast(raycastOrigin, characterRotation * Vector3.forward, out hit, maxDistance))
                    {
                        GameObject hitObject = hit.collider.gameObject;
                        if (hitObject.transform != playerObj)
                        {
                            Debug.DrawLine(raycastOrigin, hit.point, Color.red, 0.5f);
                            Debug.Log("Object in front: " + hitObject.name);

                            int index = enemies.IndexOf(hitObject);

                            if (enemies.IndexOf(hitObject) != -1)
                            {
                                enemiesAttr[index].enemy.Health -= currentAlly.SecondaryDamage;
                            }


                        }

                    }

                    Debug.DrawRay(raycastOrigin, characterRotation * Vector3.forward * maxDistance, Color.red, 0.5f);
                } 
            }
            else if (PlayerName == "wiz")
            {
                SwitchPerspective();
                
            }

            //print("r");
        }

        

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            HealthPotion hp = new();
            if (UserAttributes.SpecificPotionExist("Health Potion"))
            {
                UserAttributes.RemovePotion("Health Potion");
            }
            hp.UsePotion(currentAlly);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ManaPotion mp = new();
            if (UserAttributes.SpecificPotionExist("Mana Potion"))
            {
                UserAttributes.RemovePotion("Mana Potion");
            }
            UserAttributes.Mana += 20;
            mp.UsePotion(currentAlly);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            
            HybridPotion hyp = new();
            if (UserAttributes.SpecificPotionExist("Hybrid Potion"))
            {
                UserAttributes.RemovePotion("Hybrid Potion");
            }
            UserAttributes.Mana += 20;
            hyp.UsePotion(currentAlly);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!(stateInfo.IsName("NormalAttack02_SwordShield") && stateInfo.normalizedTime < 1f))
            {
                playerAnimator.SetTrigger("secondaryTrigger");


                float maxDistance = 3f;
                Quaternion characterRotation = playerObj.transform.rotation;
                float initialHeightOffset = 1f;
                Vector3 raycastOrigin = playerObj.transform.position + new Vector3(0, initialHeightOffset, -1);

                RaycastHit hit;
                if (Physics.Raycast(raycastOrigin, characterRotation * Vector3.forward, out hit, maxDistance))
                {
                    GameObject hitObject = hit.collider.gameObject;
                    if (hitObject.transform != playerObj)
                    {
                        if (PlayerName == "main")
                        {

                        }
                        else if (PlayerName == "dog")
                        {

                        }
                        else if (PlayerName == "wiz")
                        {

                        }
                        UserAttributes.Mana -= 5;
                    }

                }




            }
        }

        if (Input.GetKey(KeyCode.Space) && readyToJump && grounded)
        {
            UnityEngine.Debug.Log("JUMPED");

            readyToJump = false;

            Jump();

            Debug.Log("Jump");

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

    private void FixedUpdate()
    {
        if (IsBeingControlled) MovePlayer();
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * trueSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * trueSpeed * 10f * airMultiplier, ForceMode.Force);
        }
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

    public void SetActiveCamera(bool isActive)
    {
        playerCamera.gameObject.SetActive(isActive);
    }

    void SwitchPerspective()
    {
        print(isFPS);
        if (isFPS)
        {
            SwitchToFPS();
        }
        else
        {
            SwitchToTPS();
        }
    }


    public void SwitchToFPS()
    {
        wizcam.gameObject.SetActive(true);
        playerCamera.gameObject.SetActive(false);
        isFPS = false;
        r.enabled = false;
        crosshair.gameObject.SetActive(true);
    }

    public void SwitchToTPS()
    {
        wizcam.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(true);
        isFPS = true;
        r.enabled = true;
        crosshair.gameObject.SetActive(false);
    }



}
