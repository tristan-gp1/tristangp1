using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float climbSpeed;
    public float jumpForce;
    public Transform ceilingCheck;
    public Transform groundCheck;
    public LayerMask groundObjects;
    public float checkRadius;
    public int maxJumpCount;

    private Rigidbody2D rb;
    private bool facingRight = true;
    private float moveDirection;
    private float moveDirectionY;
    private bool isJumping = false;
    private bool isGrounded;
    public int jumpCount;
    private bool isLadder = false;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        jumpCount = maxJumpCount;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        Animate();
    }

    private void FixedUpdate()
    {
        Move();
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);
        if (isGrounded) 
        {
            jumpCount = maxJumpCount;
        }
    }
    private void ProcessInputs()
    {
        moveDirection = Input.GetAxis("Horizontal");
        moveDirectionY = Input.GetAxis("Vertical");
        if (Input.GetButtonDown("Jump") && jumpCount > 0) 
        {
            isJumping = true;
        }
    }
    private void Animate() 
    {
        if (moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
    }
    private void Move()
    {
        if (!isLadder)
        {
            rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        }
        else if (isLadder)
        {
            rb.velocity = new Vector2(moveDirection * moveSpeed, moveDirectionY * climbSpeed);
        }
        if (isJumping && jumpCount > 0) 
        {
            jumpCount--;
            rb.AddForce(new Vector2(0f, jumpForce));
        }
        isJumping = false;
    }
    private void FlipCharacter() 
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    IEnumerator PowerUpSpeed() 
    {
        moveSpeed = 15;
        yield return new WaitForSeconds(5);
        moveSpeed = 5;
    }

    public void SpeedPowerUp() 
    {
        StartCoroutine(PowerUpSpeed());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder") 
        {
            isLadder = true;
            rb.gravityScale = 0f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            isLadder = false;
            rb.gravityScale = 2f;
        }
    }
}
