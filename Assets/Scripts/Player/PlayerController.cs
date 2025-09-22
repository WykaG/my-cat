using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static bool gameStarted = true;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference RunInputAction;  // 1D Axis Composite
    [SerializeField] private InputActionReference JumpInputAction; // Button

    [Header("Ground & Movement")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float RunSpeed = 7f;
    [SerializeField] private float JumpForce = 7f;
    [SerializeField] private float RayDist = 1f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private PlayerDodge dodge;

    public bool IsJumping { get; private set; }
    public bool IsGrounded { get; private set; }
    public bool IsRunning => Mathf.Abs(RunInputAction.action.ReadValue<float>()) > 0.01f;

    public Vector2 lastRunDirection = Vector2.right;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        dodge = GetComponent<PlayerDodge>();

        RunInputAction.action.Enable();
        JumpInputAction.action.Enable();
        JumpInputAction.action.performed += HandleJump;
    }

    void HandleJump(InputAction.CallbackContext context)
    {
        if (IsGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Reinicia velocidad vertical
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            IsJumping = true;
            IsGrounded = false;
        }
    }

    void FixedUpdate()
    {
        // Ground check
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, RayDist, groundLayer);
        bool wasGrounded = IsGrounded;
        IsGrounded = hit.collider != null;

        // Solo considerar que dej� de saltar si estaba en el aire y toc� el suelo
        if (!wasGrounded && IsGrounded)
            IsJumping = false;
    }

    void Update()
    {
        if (dodge != null && dodge.IsDodging) return;

        // Movimiento horizontal
        float horizontal = RunInputAction.action.ReadValue<float>();
        rb.linearVelocity = new Vector2(horizontal * RunSpeed, rb.linearVelocity.y);

        if (horizontal != 0)
            lastRunDirection = new Vector2(horizontal, 0);

        // Flip de sprite
        if (horizontal > 0.01f) sr.flipX = false;
        else if (horizontal < -0.01f) sr.flipX = true;
    }
}
