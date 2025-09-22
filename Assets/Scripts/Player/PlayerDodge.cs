using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerDodge : MonoBehaviour
{
    [Header("Input Action")]
    [SerializeField] private InputActionReference DodgeInputAction; // Button
    [SerializeField] private PlayerController player;

    [Header("Dodge Settings")]
    [SerializeField] private float DodgeDistance = 0.6f;
    [SerializeField] private float DodgeDuration = 0.2f;
    [SerializeField] private float DodgeCooldown = 1f;

    private Rigidbody2D rb;
    private float nextDodgeTime = 0f;
    private Vector2 dodgeDirection;

    public bool IsDodging { get; private set; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerController>();

        DodgeInputAction.action.Enable();
        DodgeInputAction.action.performed += HandleDodgeInput;
    }

    void HandleDodgeInput(InputAction.CallbackContext context)
    {
        if (!IsDodging && Time.time >= nextDodgeTime)
        {
            StartCoroutine(DodgeCoroutine());
            nextDodgeTime = Time.time + DodgeCooldown;
        }
    }

    private IEnumerator DodgeCoroutine()
    {
        IsDodging = true;

        Vector2 startVelocity = rb.linearVelocity;

        // Dirección del dodge
        if (player != null)
            dodgeDirection = player.lastRunDirection;

        rb.linearVelocity = dodgeDirection.normalized * (DodgeDistance / DodgeDuration);

        yield return new WaitForSeconds(DodgeDuration);

        rb.linearVelocity = startVelocity;
        IsDodging = false;
    }
}
