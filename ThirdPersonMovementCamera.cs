using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform target;          // The target the camera follows (camera target on player)
    public float distance = 5.0f;     // Distance from the target
    public float minDistance = 2.0f;  // Minimum zoom distance
    public float maxDistance = 10.0f; // Maximum zoom distance
    public float height = 2.0f;       // Height offset from target
    public float smoothSpeed = 10.0f; // Camera movement smoothing
    
    [Header("Mouse Controls")]
    public float mouseSensitivity = 3.0f;
    public float scrollSensitivity = 2.0f;
    public float minVerticalAngle = -30.0f;
    public float maxVerticalAngle = 60.0f;
    
    // Camera angles
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    
    private Vector3 currentRotation;
    private Vector3 smoothVelocity = Vector3.zero;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("Please assign a target to the camera.");
            return;
        }
        
        // Initialize camera position behind player
        rotationY = target.eulerAngles.y;
        rotationX = 20.0f; // Start with slight downward angle
        
        // Lock and hide cursor for the game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target == null)
            return;
        
        // Get mouse input
        rotationY += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationX -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        // Clamp vertical rotation
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);
        
        // Handle scroll wheel zoom
        distance -= Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
        
        // Calculate rotation
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        
        // Calculate camera position
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = rotation * negDistance + target.position + new Vector3(0, height, 0);
        
        // Apply smoothed position and rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, smoothSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, position, smoothSpeed * Time.deltaTime);
    }
}
