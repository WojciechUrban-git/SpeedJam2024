using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] Transform playerCamera;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] bool cursorLock = true;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float Speed = 3.0f;
    [SerializeField] float sprintSpeedMultiplier = 2f; // Speed multiplier while sprinting
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] float gravity = -30f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    public float jumpHeight = 2f;

    [Header("Energy Bar Settings")]
    [SerializeField] Slider energyBar; // Assign a UI Slider for the energy bar
    [SerializeField] float maxEnergy = 100f;
    [SerializeField] float energyDrainRate = 10f; // Energy drained per second while sprinting

    float velocityY;
    bool isGrounded;
    float cameraCap;
    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;
    CharacterController controller;
    Vector2 currentDir;
    Vector2 currentDirVelocity;
    Vector3 velocity;
    float currentEnergy;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }

        // Initialize energy to 75% of maxEnergy
        currentEnergy = maxEnergy * 0.75f;
        energyBar.maxValue = maxEnergy;
        energyBar.value = currentEnergy;
    }

    void Update()
    {
        UpdateMouse();
        UpdateMove();
        UpdateEnergy();
    }

    void UpdateMouse()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraCap -= currentMouseDelta.y * mouseSensitivity;
        cameraCap = Mathf.Clamp(cameraCap, -90.0f, 90.0f);
        playerCamera.localEulerAngles = Vector3.right * cameraCap;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMove()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, ground);

        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        // Adjust speed based on sprinting
        float currentSpeed = Speed;
        if (Input.GetKey(KeyCode.LeftShift) && currentEnergy > 0)
        {
            currentSpeed *= sprintSpeedMultiplier;
            currentEnergy -= energyDrainRate * Time.deltaTime;
        }

        // Ensure energy doesn't drop below 0
        currentEnergy = Mathf.Max(currentEnergy, 0f);

        velocityY += gravity * 2f * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * currentSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (isGrounded! && controller.velocity.y < -1f)
        {
            velocityY = -8f;
        }
    }

    void UpdateEnergy()
    {
        // Update the energy bar UI
        if (energyBar != null)
        {
            energyBar.value = currentEnergy;
        }
    }

    public void AddEnergy(float amount)
    {
        currentEnergy += amount;
        currentEnergy = Mathf.Min(currentEnergy, maxEnergy); // Ensure energy doesn't exceed maxEnergy

        if (energyBar != null)
        {
            energyBar.value = currentEnergy; // Update the energy bar UI
        }
    }
}
