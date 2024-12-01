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
    [SerializeField] float sprintSpeedMultiplier = 2f;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] float gravity = -30f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    public float jumpHeight = 2f;

    [Header("Boost Settings")]
    [SerializeField] Slider boostBar; // UI Slider for the boost effect
    [SerializeField] float boostDuration = 5f; // Duration of the boost effect
    [SerializeField] float boostSpeedMultiplier = 1.5f; // Speed multiplier during boost
    [SerializeField] float boostJumpMultiplier = 1.5f; // Jump height multiplier during boost

    private float velocityY;
    private bool isGrounded;
    private float cameraCap;
    private Vector2 currentMouseDelta;
    private Vector2 currentMouseDeltaVelocity;
    private CharacterController controller;
    private Vector2 currentDir;
    private Vector2 currentDirVelocity;
    private Vector3 velocity;
    private float boostTimeRemaining; // Time remaining for the boost effect
    private bool isBoostActive;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }

        // Initialize boost bar
        if (boostBar != null)
        {
            boostBar.gameObject.SetActive(false);
            boostBar.maxValue = boostDuration;
        }
    }

    void Update()
    {
        UpdateMouse();
        UpdateMove();
        UpdateBoost();
    }

void UpdateMouse()
{
    if (Cursor.lockState == CursorLockMode.Locked)
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraCap -= currentMouseDelta.y * mouseSensitivity;
        cameraCap = Mathf.Clamp(cameraCap, -90.0f, 90.0f);
        playerCamera.localEulerAngles = Vector3.right * cameraCap;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }
}


    void UpdateMove()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, ground);

        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        float currentSpeed = Speed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= sprintSpeedMultiplier;
        }

        // Apply boost effects
        if (isBoostActive)
        {
            currentSpeed *= boostSpeedMultiplier;
        }

        velocityY += gravity * 2f * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * currentSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            float jump = isBoostActive ? jumpHeight * boostJumpMultiplier : jumpHeight;
            velocityY = Mathf.Sqrt(jump * -2f * gravity);
        }

        if (isGrounded && controller.velocity.y < -1f)
        {
            velocityY = -8f;
        }
    }

    void UpdateBoost()
    {
        if (isBoostActive)
        {
            boostTimeRemaining -= Time.deltaTime;

            if (boostBar != null)
            {
                boostBar.value = boostTimeRemaining;
            }

            if (boostTimeRemaining <= 0)
            {
                isBoostActive = false;

                if (boostBar != null)
                {
                    boostBar.gameObject.SetActive(false);
                }
            }
        }
    }

    public void ActivateBoost()
    {
        isBoostActive = true;
        boostTimeRemaining = boostDuration;

        if (boostBar != null)
        {
            boostBar.gameObject.SetActive(true);
            boostBar.value = boostDuration;
        }
    }

    // Method to dynamically set mouse sensitivity
    public void SetMouseSensitivity(float sensitivity)
    {
        mouseSensitivity = sensitivity;
    }
}
