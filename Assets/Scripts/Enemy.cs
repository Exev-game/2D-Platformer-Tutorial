using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player; //so enemy can chase the player
    public float chaseSpeed = 2f;
    public float jumpForce = 2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool shouldJump; //tells the enemy to jump

    public int damage = 1; //How much damage the enemy causes, ep #14

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Is the enemy Grounded?
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);

        //Player Direction
        float direction = Mathf.Sign(player.position.x - transform.position.x);


        //Is Player above the enemy?
        bool isPlayerAbove = Physics2D.Raycast(transform.position, Vector2.up, 5f, 1 << player.gameObject.layer);

        if (isGrounded)
        {
            //Chase player
            rb.linearVelocity = new Vector2(direction * chaseSpeed, rb.linearVelocity.y);

            //Jump if there's a gap ahead AND no ground infront

            //else if there's player above and platform above

            //If Ground
            RaycastHit2D groundInFront = Physics2D.Raycast(transform.position, new Vector2(direction, 0), 2f, groundLayer);

            //If Gap
            RaycastHit2D gapAhead = Physics2D.Raycast(transform.position + new Vector3(direction, 0, 0), Vector2.down, 2f, groundLayer);

            //If Platform
            RaycastHit2D platformAbove = Physics2D.Raycast(transform.position, Vector2.up, 5f, groundLayer);

            if(!groundInFront.collider && !gapAhead.collider)
            {
                shouldJump = true;
            }
            else if (isPlayerAbove && platformAbove.collider)
            {
                shouldJump = true;
            }

        }

    }

    //Gets the enemy jumping
    private void FixedUpdate()
    {
        if (isGrounded && shouldJump)
        {
            shouldJump = false;
            Vector2 direction = (player.position - transform.position).normalized; //"so doens't go faster on the diagonal"

            Vector2 jumpDirection = direction * jumpForce;

            rb.AddForce(new Vector2(jumpDirection.x, jumpForce), ForceMode2D.Impulse);
        }

    }
}
