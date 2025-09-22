using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory; // Player reference to check if has the key
    [SerializeField] private InputActionReference OpenChest;   // Input for opening the chest

    private Animator animator;
    private bool isNearChest = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        OpenChest.action.performed += HandleOpenChestInput;
        OpenChest.action.Enable();
    }

    private void OnDisable()
    {
        OpenChest.action.performed -= HandleOpenChestInput;
        OpenChest.action.Disable();
    }

    //Input open chest
    private void HandleOpenChestInput(InputAction.CallbackContext context)
    {
        if (isNearChest && playerInventory != null && playerInventory.hasKey)
        {
            animator.SetBool("chestOpen", true);
        }
    }
    // Detect player enter the chest area

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isNearChest = true;
    }

    // Detect  player exits the chest area
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isNearChest = false;
    }
}
