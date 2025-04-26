using UnityEngine;
public class ThirdPersonMovementScript : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 8f;
    public float gravityMultiplier = 2.5f;
    public float turnSmoothTime = 0.1f;
    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    [Header("Camera")]
    public Transform cameraTarget;  // Empty object that the camera will follow
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float turnSmoothVelocity;
    private Transform mainCamera;
    // Movement state
    private bool isRunning = false;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main.transform;
        
        if (cameraTarget == null)
        {
            Debug.LogError("Camera target is not assigned to the ThirdPersonMovementScript!");
        }
    }
    void Update()
    {
        // Check if grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        // Reset vertical velocity when grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small negative value instead of zero for better grounding
        }
        // Get input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        // Check if running (holding shift)
        isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        // Move the character
        if (direction.magnitude >= 0.1f)
        {
            // Calculate the angle to rotate towards based on camera
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            
            // Rotate the character
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            // Calculate movement direction
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime);
        }
        // Jump when grounded and space pressed
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
        }
        // Apply gravity
        velocity.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        
        // Apply vertical movement
        controller.Move(velocity * Time.deltaTime);
    }
}
