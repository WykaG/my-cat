using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpKey : MonoBehaviour
{
    [SerializeField] private InputActionReference KeyPickup; // Input pick up key
    [SerializeField] private PlayerInventory playerInventory; // Player inventory reference

    private bool isNearKey = false;
    private GameObject Key;

    private void OnEnable()
    {
        KeyPickup.action.performed += HandleKeyPickupInput;
        KeyPickup.action.Enable();
    }

    private void OnDisable()
    {
        KeyPickup.action.performed -= HandleKeyPickupInput;
        KeyPickup.action.Disable();
    }

    //Input pick up key
    private void HandleKeyPickupInput(InputAction.CallbackContext context)
    {
        if (isNearKey && playerInventory != null)
        {
            playerInventory.hasKey = true; // Key added to inventory
            Destroy(Key);
        }

    }

    //Player enters key area
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            isNearKey = true;
            Key = other.gameObject;
        }
    }

    //Player exits key area
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            isNearKey = false;
            Key = null;
        }
    }
}
