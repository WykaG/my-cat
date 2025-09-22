using UnityEngine;

public class PlayerControlller : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 8f;
    private bool isGrounded = true;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Reed input horizontal
        float move = Input.GetAxisRaw("Horizontal");

        // Move Cat (X)
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        // -------- Animations --------
        // Run
        animator.SetBool("isRunning", move != 0);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }

        // Flip
        if (move > 0)
            spriteRenderer.flipX = false;
        else if (move < 0)
            spriteRenderer.flipX = true;
    }

    // Detect collision with the ground
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }
}