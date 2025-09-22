using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private Chest chest;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerInventory playerinventory;
    [SerializeField] private PlayerDodge playerDodge;
    [SerializeField] private PlayerView playerView;
    [SerializeField] private PickUpKey pickUpKey;
    [SerializeField] private FloatingItem floatingItem;
    [SerializeField] private GameObject startScreen;

    [SerializeField] private InputActionReference inputActionReference; // Renamed for clarity
    private bool inputAction = false;

    void Awake()
    {
        chest.enabled = false;
        playerView.enabled = false;
        playerController.enabled = false;
        playerDodge.enabled = false;
        playerinventory.enabled = false;
        pickUpKey.enabled = false;
        floatingItem.enabled = false;
    }

    void Start()
    {
        inputActionReference.action.performed += HandleInputAction;
    }

    void HandleInputAction(InputAction.CallbackContext context)
    {
        inputAction = true;
        Debug.Log(inputAction);
    }

    void Update()
    {
        if (inputAction == true)
        {

            chest.enabled = true;
            playerView.enabled = true;
            playerController.enabled = true;
            playerDodge.enabled = true;
            playerinventory.enabled = true;
            pickUpKey.enabled = true;
            floatingItem.enabled = true;

            Destroy(startScreen);
        }
    }
}
