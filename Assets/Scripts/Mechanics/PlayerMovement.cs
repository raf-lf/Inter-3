using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float moveSpeedCurrent;
    public float jumpForce;

    public bool move;
    public bool jumping;
    public bool crouching;
    public bool onGround;
    public bool canStand;
    private RaycastHit2D onGroundRay;

    public static bool facingLeft = false;

    private Rigidbody2D rb;
    private Animator animator;

    private LayerMask jumpableMask;
    private float jumpFallTimer;

    public SpriteRenderer head;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpableMask = LayerMask.GetMask("Default");
        moveSpeedCurrent = moveSpeed;
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
        rb.velocity = new Vector2(0, jumpForce);

        animator.SetBool("jump", true);
        jumping = true;
        jumpFallTimer = Time.time + 0.1f;
    }

    public void JumpLand()
    {
        jumping = false;
        animator.SetBool("jump", false);
        //rb.velocity = new Vector2(0, 0);
    }

    public void Knockback(float knockback, Transform sourcePosition)
    {
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

        Vector3 difference = Player.PlayerCharacter.transform.position - sourcePosition.position;
        float distance = difference.magnitude;
        Vector2 direction = difference / distance;
        direction.Normalize();

        rb.velocity = direction * knockback;
    }

    public void HaltMovement()
    {
        if (jumping) JumpLand();
        if (move) move = false;
        rb.velocity = new Vector2(0,0);
    }

    public void Crouch(bool crouched)
    {
        if (crouched)
        {
            crouching = true;
            animator.SetBool("crouch", true);
            //moveSpeedCurrent /= 2;

        }
        else if (HasRoomToStand())
        {
            crouching = false;
            animator.SetBool("crouch", false);
            //moveSpeedCurrent *= 2;
        }
    }

    bool HasRoomToStand()
    {
        if (Physics2D.Raycast(transform.position, Vector2.up, 1.2f, jumpableMask)) return false;
        else return true;
    }   

    void Update()
    {
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
            //rb.sharedMaterial.friction = 0;
        }
        else
        {
            animator.SetBool("move", false);
           // if (!jumping) rb.sharedMaterial.friction = 100000;
        }

        //Jump landing check
        if (jumping == true && onGround && Time.time >= jumpFallTimer)
        {
            JumpLand();
        }

        //If player is falling, set them to falling animation
        if (rb.velocity.y < -5 && jumping == false)
        {
            animator.SetBool("jump", true);
            jumping = true;
            jumpFallTimer = Time.time + 0.1f;
        }

        if (Player.PlayerControls == true && Player.scriptPlayer.inCover == false)
        {
            //Stop movement
            if (Input.GetKey(KeyCode.D) == false && Input.GetKey(KeyCode.A) == false)
            {
                move = false;

                if(jumping ==false) rb.velocity = new Vector2(rb.velocity.x/2, rb.velocity.y);
            }

            //Walk right
            if (Input.GetKey(KeyCode.D) == true)
            {
                move = true;

                transform.rotation = new Quaternion(0,0,0,0);
                facingLeft = false;

                rb.velocity = new Vector2(moveSpeedCurrent, rb.velocity.y);
            }

            //Walk left
            if (Input.GetKey(KeyCode.A) == true)
            {
                move = true;

                transform.rotation = new Quaternion(0, 180, 0, 0);
                facingLeft = true;

                rb.velocity = new Vector2(moveSpeedCurrent * -1, rb.velocity.y);
            }

            //Jump
            if (Input.GetKeyDown(PlayerActions.keyJump) && onGround && jumping == false)
            {
                JumpAttempt();
            }

            //Attempts to crouch if pressing crouch key, not jumping and only if on ground
            if (Player.scriptPlayer.inCover == false)
            {
                if (Input.GetKey(PlayerActions.keyCrouch) && jumping == false && onGround == true)
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
