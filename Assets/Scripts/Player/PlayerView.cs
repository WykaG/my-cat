using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private PlayerController player;
    private PlayerDodge dodge;

    void Start()
    {
        player = GetComponent<PlayerController>();
        dodge = GetComponent<PlayerDodge>();
    }

    void Update()
    {
        if (animator == null || player == null) return;

        // Prioridad: Dodge > Jump > Run > Idle
        animator.SetBool("IsDodging", dodge != null && dodge.IsDodging);
        animator.SetBool("IsJumping", player.IsJumping);
        animator.SetBool("IsRunning", player.IsRunning && player.IsGrounded);
        animator.SetBool("IsGrounded", player.IsGrounded && !player.IsJumping);
    }
}
