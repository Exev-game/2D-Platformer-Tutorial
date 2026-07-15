using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    bool isFacingRight = true;
    public ParticleSystem smokeFX;

    //Header Movement
    public float moveSpeed = 5f;

    float horizontalMovement;

    //Header Jumping
    public float jumpPower = 10f;
    public int maxJumps = 2;
    private int jumpsRemaining;

    //Header GroundCheck
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;
    bool isGrounded;


    //Header Gravity
    public float baseGravity = 2;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f;

    //Header WallCheck
    public Transform wallCheckPos;
    public Vector2 wallCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask wallLayer;

    //Header WallMovement
    public float wallSlideSpeed = 2;
    bool isWallSliding;
    bool isWallJumping;
    float wallJumpDirection;
    float wallJumpTime = 0.5f;
    float wallJumpTimer;
    public Vector2 wallJumpPower = new Vector2(5f, 10f);


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        //Checks if player has jumps remaining
        GroundCheck();

        //Checks gravity
        Gravity();


        

        //Checks for Wall Sliding and Wall Jumping
        ProcessWallSlide();
        ProcessWallJump();

        //gets the player to move... ep4 made the if to wall jumping and flipping

       if (!isWallJumping)
        {
            rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);
            Flip();        // Flips character
        }

       //update animations in animator ep.#6
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
        animator.SetFloat("magnitude", rb.linearVelocity.magnitude);
        animator.SetBool("isWallSliding", isWallSliding);

    }

    //function handling our gravity
    private void Gravity()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier; //Fall increasingly faster
         rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed)); //Caps the full speed so that player can't go more than fullspeed
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    //input actions
    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    //jumping funtions
    public void Jump(InputAction.CallbackContext context) // enables jumping
    {
        

        if (jumpsRemaining > 0) // can't jump infinately
        {
            if (context.performed)
            {
                //Hold down jump button = full height
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                jumpsRemaining--;
               
                JumpFX(); //animator trigger jump animation
                AudioManager.Instance.PlaySFX("Jump"); //Playes the jump sound
            }
            else if (context.canceled)
            {
                //Light tap of jump button = half the height
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
                jumpsRemaining--;
                
                JumpFX();//animator trigger jump animation
                
            }
            
        }
        //Walljumping
        if(context.performed && wallJumpTimer >0f)
        {
            isWallJumping = true;
            rb.linearVelocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y); //Jump away from wall
            wallJumpTimer = 0;
            //animator trigger jump animation
            JumpFX();

            //force flip
            if (transform.localScale.x != wallJumpDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 ls = transform.localScale;
                ls.x *= -1f;
                transform.localScale = ls;
            }

            Invoke(nameof(CancelWallJump), wallJumpTime + 0.1f); //Wall Jump will last for 0.5f and character can jump again in 0.6f
        }

    }

    //animates jump AND smoke effect
    private void JumpFX()
    {
        animator.SetTrigger("jump");
        smokeFX.Play();
    }

    //Checks if there is Ground (Anything on Ground layer) below player ep.4... later changed to give ability to double jump again
    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        
    }

    // Checks if WallCheck is detecting walls (Anything on Wall Layer)
    private bool WallCheck()
    {
        return Physics2D.OverlapBox(wallCheckPos.position, wallCheckSize, 0, wallLayer);
    }

    //Flip the character when moving
    private void Flip()
    {
        if (isFacingRight && horizontalMovement < 0 || !isFacingRight && horizontalMovement > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;

            //smoke effect
            if(rb.linearVelocity.y == 0)
            {
                smokeFX.Play();
            }
            
        }
    }

    //Enable to slide on walls
    private void ProcessWallSlide()
    {
        if (!isGrounded & WallCheck() & horizontalMovement != 0)
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -wallSlideSpeed)); //caps fall rate while sliding
        }
        else 
        {
            isWallSliding = false;
        }

    }


    //Enable walljump
    private void ProcessWallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpDirection = - transform.localScale.x; //when walljumping, character jumps in opposite direction
            wallJumpTimer = wallJumpTime;

            CancelInvoke(nameof(CancelWallJump));
        }
        else if (wallJumpTimer > 0f)
        {
            wallJumpTimer -= Time.deltaTime; 
        }
    }

    //Stops wall jumping. This funtion will let me use 'invoke' in other places in the code
    private void CancelWallJump()
    {
        isWallJumping = false;
    }

    //To be able to visualise ground check size ep.#3... wall check ep.#4
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(wallCheckPos.position, wallCheckSize);

    }
 
}
