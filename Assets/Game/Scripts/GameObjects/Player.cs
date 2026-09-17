using UnityEngine;
using UnityEngine.InputSystem;

interface IInteractable
{
    public string GetInteractText();
    public void Interact();
}
public class Player : MonoBehaviour
{
    public AudioSource footstepSource;
    public AudioClip[] footstepSounds;

    public float footstepInterval = 0.4f;

    private float footstepTimer = 0f;


    public float movementSpeed = 1f;
    public float jumpHeight = 1f;
    public float gravityValue = -9.81f;

    public Transform cameraTransform;
    public float mouseSensitivity = 0.1f;
    private float xRotation = 0f;
    private float yRotation = 180f;


    
    public float interactRange = 1.5f;
    public LayerMask interactionLayer;
    

    public InputActionReference moveAction;
    //public InputActionReference jumpAction;
    public InputActionReference lookAction;
    public InputActionReference interactAction;

    public CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private IInteractable currentInteractable;

    private bool canMove = false;

    private void OnEnable()
    {
        moveAction.action.Enable();
        //jumpAction.action.Enable();
        lookAction.action.Enable();
        interactAction.action.Enable();

        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnDisable()
    {
        moveAction.action.Disable();
        //jumpAction.action.Disable();
        lookAction.action.Disable();
        interactAction.action.Disable();
    }

    void Update ()
    {
        if ( !canMove ) return;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            G.UIManager.TogglePauseMenu(true);
            SetMovementEnabled(false);
        }

    // движение камерой
        Vector2 mouseDelta = lookAction.action.ReadValue<Vector2>();

        yRotation += mouseDelta.x * mouseSensitivity;
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

        xRotation -= mouseDelta.y * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    // движение игрока
        isGrounded = controller.isGrounded;

        if (isGrounded)
        {
            if (playerVelocity.y < -2f) 
            {
                playerVelocity.y = -2f;
            }
        }

        Vector2 input = moveAction.action.ReadValue<Vector2>();

        bool isMoving = input.magnitude > 0f;

        if (isMoving &&  isGrounded)
        {
            footstepTimer += Time.deltaTime;

            if (footstepTimer >= footstepInterval)
            {
                footstepSource.PlayOneShot(footstepSounds[Random.Range(0, footstepSounds.Length)]);
                footstepTimer = 0f;
            }
        }
        else
        {
            footstepTimer = 0f;
        }

        Vector3 move = transform.right * input.x + transform.forward * input.y;
        move = Vector3.ClampMagnitude(move, 1f);

        //if (isGrounded && jumpAction.action.WasPressedThisFrame())
        //{
        //    playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
        //}

        playerVelocity.y += gravityValue * Time.deltaTime;

        Vector3 finalMove = move * movementSpeed + Vector3.up * playerVelocity.y;
        controller.Move(finalMove * Time.deltaTime);
        // взаимодействие
        IInteractable interactObject = CheckInteractable();
        if (interactObject != currentInteractable)
        {
            currentInteractable = interactObject;
            if (interactObject != null)
            {
                G.UIManager.SetInteractableUI(true, interactObject.GetInteractText());
            }
            else
            {
                G.UIManager.SetInteractableUI(false, string.Empty);
            }
        }
        if (currentInteractable != null && interactAction.action.WasPressedThisFrame())
        {
            interactObject.Interact();
            G.UIManager.SetInteractableUI(true, interactObject.GetInteractText());
        }

    }
    IInteractable CheckInteractable()
    {
        Ray r = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.SphereCast(r, 0.1f, out RaycastHit hitInfo, interactRange, interactionLayer))
        {
            return hitInfo.collider.GetComponentInParent<IInteractable>();
        }
        return null;
    }

    public void ResetCamera(float rotation)
    {
        xRotation = 0f;
        yRotation = rotation;
        transform.rotation = Quaternion.Euler(0f, rotation, 0f);

    }

    public void SetMovementEnabled(bool enabled)
    {
        canMove = enabled;
    }
}
