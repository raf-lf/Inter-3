using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeedIncrement = 1;
    public float moveSpeedMax = 6;
    public float crouchSpeedMax = 2;
    public float climbSpeedMax = 2;
    public float walkSpeedMax = 1;
    public float rollSpeedBoost = 4;

    private float currentTopSpeed;

    [Header("Jumping")]
    public float jumpForce; //21 if no boost, 10 if boost
    public float jumpForceBoost; //0 if no boost, 1.75 if boost
    public float jumpBoostWindow; //0.15
    private float jumpBoostTimer;
    [SerializeField]
    private bool canJumpBoost = false;

    [Header("Fall Damage")]
    public float fallVelocityMemory;
    public int fallDamageLow;
    public int fallDamageHigh;
    public float fallDamageLowVelocity;
    public float fallDamageHighVelocity;


    [Header("States")]
    public bool move;
    public bool jumping;
    public bool crouching;
    public bool onGround;
    public bool canStand;
    public bool climbing;
    public bool walking;

    private bool movementHeld;
    private RaycastHit2D onGroundRay;

    public static bool facingLeft = false;

    public Rigidbody2D rb;
    public Animator animator;

    private LayerMask jumpableMask;
    private float jumpFallTimer;

    public SpriteRenderer head;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpableMask = LayerMask.GetMask("Default");
    }

    public void ShootingFaceDirection(bool shootingOpposite)
    {
        head.flipX = shootingOpposite;
    }

    public void JumpAttempt()
    {
        if (crouching)
        {
            if(HasRoomToStand())
            {
                Crouch(false);
                Jump();
            }

        }
        else
        {
            Jump();
        }
    }

    public void Jump()
    {
        if (climbing) Climb(false);
        Vector2 velocityChange = new Vector2(0, jumpForce);
        
        rb.velocity += velocityChange;

        animator.SetBool("jump", true);
        jumping = true;
        jumpFallTimer = Time.time + 0.1f;
        canJumpBoost = true;
        jumpBoostTimer = Time.time + jumpBoostWindow;

        GameManager.scriptPlayerAudio.JumpUpSfx();
    }

    public void JumpLand()
    {
        //Debug.Log("Landing speed = " + rb.velocity.y);

        jumping = false;
        animator.SetBool("jump", false);

        
        //Fall Damage
        
        if (rb.velocity.y <= fallDamageHighVelocity * -1) GameManager.scriptPlayer.Damage(fallDamageHigh, 0, null);
        else if (rb.velocity.y <= fallDamageLowVelocity * -1) GameManager.scriptPlayer.Damage(fallDamageLow, 0, null);
        
        

    }

    private void JumpBoost()
    {
        Vector2 velocityChange = new Vector2(0, jumpForceBoost);
        rb.velocity += velocityChange;
    }

    public void Knockback(float knockback, Transform sourcePosition)
    {
        if (climbing) Climb(false);

        //If you're, stand up only if there's room. Applies knockback either way.
        if (crouching)
        {
            if (HasRoomToStand())
            {
                Crouch(false);
                jumping = true;
            }
        }
        else jumping = true;

        Vector3 difference = GameManager.PlayerCharacter.transform.position - sourcePosition.position;
        float distance = difference.magnitude;
        Vector2 direction = difference / distance;
        direction.Normalize();

        rb.velocity = direction * knockback;
    }

    public void HaltMovement(bool hold)
    {
        movementHeld = hold;
        move = false;
    }

    public void Crouch(bool crouched)
    {
        if (crouched)
        {
            crouching = true;
            animator.SetBool("crouch", true);


        }
        else if (HasRoomToStand())
        {
            crouching = false;
            animator.SetBool("crouch", false);
        }
    }

    bool HasRoomToStand()
    {
        if (Physics2D.Raycast(transform.position, Vector2.up, 1.2f, jumpableMask)) return false;
        else return true;
    }   

    void Fall()
    {
        animator.SetBool("jump", true);
        jumping = true;
        jumpFallTimer = Time.time + 0.1f;

    }

    public void Roll()
    {
        Player.PlayerControls = false;   
        Vector2 speedChange;

        if (facingLeft) speedChange = new Vector2(rollSpeedBoost * -1, 0);
        else speedChange = new Vector2(rollSpeedBoost, 0);

        rb.velocity += speedChange;
        
    }
    public void RollEnd()
    {
        Player.PlayerControls = true;
        if (PlayerWeapons.equipedWeapon == 0) GameManager.scriptWeapons.UpdateMeleeWeaponRotation();
    }

    public void Climb(bool climbOn)
    {
        if (GameManager.GamePaused == false)
        {

            if (climbOn)
            {
                jumping = false;
                rb.gravityScale = 0;

                GameManager.scriptPlayerAudio.LadderStepSfx();
                GameManager.scriptWeapons.equipedWeaponMemory = PlayerWeapons.equipedWeapon;
                GameManager.scriptWeapons.SwapWeapon(PlayerWeapons.equipedWeapon);
                Player.CantAct = true;
            }
            else
            {
                Fall();
                rb.gravityScale = 5;

                Player.CantAct = false;
                GameManager.scriptWeapons.SwapWeapon(GameManager.scriptWeapons.equipedWeaponMemory);
            }

            animator.SetInteger("climbDirection", 0);
            animator.SetBool("climbing", climbOn);
            climbing = climbOn;
        }

    }

    void MovementClimb(bool moving, int direction)
    {
        animator.SetInteger("climbDirection", direction);
        rb.velocity = new Vector2(0, climbSpeedMax * direction);

    }

    public void Movement (bool moving, bool moveLeft)
    {
        if (moving)
        {
            move = true;
            facingLeft = moveLeft;

            //Rotates player object
            if (moveLeft) transform.rotation = new Quaternion(0, 180, 0, 0);
            else transform.rotation = new Quaternion(0, 0, 0, 0);

            //Sets top speed based on movement type
            if (crouching) currentTopSpeed = crouchSpeedMax;
            else if (walking) currentTopSpeed = walkSpeedMax;
            else currentTopSpeed = moveSpeedMax;

            Vector2 speedChange = new Vector2(moveSpeedIncrement, 0);

            if (moveLeft)
            {
                if (rb.velocity.x > currentTopSpeed * -1)
                {
                    rb.velocity -= speedChange;
                }
                
                
                
            }
            else
            {
                if (rb.velocity.x < currentTopSpeed)
                {
                    rb.velocity += speedChange;
                }       
            }
        }

        else
        {
            move = false;

            if (jumping == false) rb.velocity = new Vector2(rb.velocity.x / 2, rb.velocity.y);
        }

    }

    void Update()
    {
        if (movementHeld) Movement(false, false);

        //Detect if the player can stand for crouching purposes
        canStand = HasRoomToStand();

        //Detect if player is sitting on ground
        onGroundRay = Physics2D.Linecast(new Vector2(transform.position.x - 0.25f, transform.position.y - 0.02f), new Vector2(transform.position.x + 0.25f, transform.position.y - 0.02f), jumpableMask);
        if (onGroundRay)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }

        //Detect if player is moving
        if (move == true)
        {
            animator.SetBool("move", true);

        }
        else
        {
            animator.SetBool("move", false);
        }

        //Jump landing check
        if (jumping == true && onGround && Time.time >= jumpFallTimer)
        {
            JumpLand();
        }

        //Ends Jump Boost Window
        if (canJumpBoost == true && Time.time >= jumpBoostTimer)
        {
            canJumpBoost = false;
        }

        //If player is falling, set them to falling animation
        if (rb.velocity.y < -8 && jumping == false)
        {
            Fall();
        }

        if (Player.PlayerControls && GameManager.GamePaused == false)
        {
            //Jump when key pressed
            if (Input.GetKeyDown(PlayerActions.keyJump) && jumping == false)
            {
                if(onGround || climbing) JumpAttempt();
            }

            if (climbing)
            {
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
                {
                    if (Input.GetKey(KeyCode.W) == false && Input.GetKey(KeyCode.S) == false)
                    {
                        Climb(false);
                        Fall();
                    }
                }

                if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)) MovementClimb(false, 0);
                else if (Input.GetKey(KeyCode.W)) MovementClimb(true, 1);
                else if (Input.GetKey(KeyCode.S)) MovementClimb(true, -1);
                else MovementClimb(false, 0);

            }
            else if (GameManager.scriptPlayer.inCover == false)
            {
                //Stop movement when not trying to move
                if (Input.GetKey(KeyCode.D) == false && Input.GetKey(KeyCode.A) == false) Movement(false, false);

                if (Input.GetKey(PlayerActions.keyWalk) && crouching == false && GameManager.scriptPlayer.inCover == false)
                {
                    animator.SetBool("walk", true);
                    walking = true;
                }
                else
                {
                    animator.SetBool("walk", false);
                    walking = false;
                }

                //Stop movement when pressing both move keys
                if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A)) Movement(false, false);

                //Move right
                else if (Input.GetKey(KeyCode.D) == true) Movement(true, false);

                //Move left
                else if (Input.GetKey(KeyCode.A) == true) Movement(true, true);

                //Attempts to crouch if pressing crouch key, not jumping and only if on ground
                if (GameManager.scriptPlayer.inCover == false)
                {
                    if (Input.GetKey(PlayerActions.keyCrouch) && jumping == false && onGround && climbing == false)
                    {
                        if (crouching == false) Crouch(true);
                    }
                    //Tries to standup when crouch key is not pressed
                    else
                    {
                        //Stand up only if there is room
                        if (crouching == true && HasRoomToStand())
                        {
                            Crouch(false);
                        }

                    }
                }
            }

        }
    }

    private void FixedUpdate()
    {

        //Boosts jump force if jump key continues being pressed
        if (Input.GetKey(PlayerActions.keyJump) && jumping && canJumpBoost) JumpBoost();

    }
}
