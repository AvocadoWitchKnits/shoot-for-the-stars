using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class FPController : MonoBehaviour

{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;     // Controls the downward force applied to the player. The value is negative because gravity pulls the player down.
    public float jumpHeight = 1.5f;

    [Header("Look Settings")]
    public Transform cameraTransform;
    public float lookSensitivity = 2f;
    public float verticalLookLimit = 90f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform gunPoint;
    public float bulletForce = 700f;

    [Header("Crouch Settings")]
    public float crouchHeight = 1f;
    public float standHeight = 2f;
    public float crouchSpeed = 2.5f;
    private float originalMoveSpeed;

    [Header("Toggle Menu")]
    public Toggle ToggleMenu;

    [Header("Dialogue")]
    public float interactRange = 5f;
    public LayerMask npcLayer;
    public GameObject dialogueUI;
    public TMP_Text dialogueText;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private float verticalRotation = 0f;

    // Awake runs once when the GameObject is first loaded.
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        originalMoveSpeed = moveSpeed;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void Update()
    {
        HandleMovement();
        HandleLook();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Reads the movement input as a Vector2.
        // For example, WASD or the left analogue stick.
        moveInput = context.ReadValue<Vector2>();
    }

    // This method is called by the Input System when look input changes.
    public void OnLook(InputAction.CallbackContext context)
    {
        // Reads the look input as a Vector2.
        // For example, mouse movement or the right analogue stick.
        lookInput = context.ReadValue<Vector2>();
    }

    // Handles the player's movement and gravity.
    public void HandleMovement()
    {
        // Creates the horizontal movement direction.
        Vector3 move =
            transform.right * moveInput.x +
            transform.forward * moveInput.y;

        // Moves the player horizontally.
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Keeps the player connected to the ground.
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Gravity must run every frame, not only while grounded.
        velocity.y += gravity * Time.deltaTime;

        // Applies the vertical movement for jumping and falling.
        controller.Move(velocity * Time.deltaTime);
    }


    // Handles the player's camera and body rotation.
    public void HandleLook()
    {
        // Calculates horizontal camera movement using the look input
        // and the selected sensitivity.
        float mouseX = lookInput.x * lookSensitivity;

        // Calculates vertical camera movement using the look input
        // and the selected sensitivity.
        float mouseY = lookInput.y * lookSensitivity;

        // Subtracts the vertical mouse movement from the camera rotation.
        // Subtraction makes moving the mouse upwards look upwards.
        verticalRotation -= mouseY;

        // Limits the vertical camera rotation so that the player
        // cannot rotate the camera completely over their head.
        verticalRotation = Mathf.Clamp(
            verticalRotation,
            -verticalLookLimit,
            verticalLookLimit
        );

        // Rotates only the camera up and down.
        cameraTransform.localRotation =
            Quaternion.Euler(verticalRotation, 0f, 0f);

        // Rotates the entire player GameObject left and right.
        transform.Rotate(Vector3.up * mouseX);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded) // Check that the Jump action was successfully performed and that the player is currently standing on the ground.
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Calculates the upward speed needed for the player to reach the chosen jump height while accounting for gravity.
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Shoot();
        }
    }

    public void OnDialogue(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Dialogue();
        }
    }
    

    public void OnToggleMenu(InputAction.CallbackContext context)
    {
        if (context.performed && ToggleMenu != null)
        {
            ToggleMenu.isOn = !ToggleMenu.isOn;
        }
    }
   

    private NPC currentNPC;
    private int dialogueIndex = 0;
    private bool inDialogue = false;

    private void Dialogue()
    {
        if (!inDialogue)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, interactRange, npcLayer);
            if (hits.Length > 0)
            {
                NPC npc = hits[0].GetComponent<NPC>();
                if (npc != null)
                {
                    currentNPC = npc;
                    dialogueIndex = 0;
                    inDialogue = true;

                    if (dialogueUI != null)
                    {
                        dialogueUI.SetActive(true);
                    }

                    ShowLine();
                }
            }
        }
        else
        {
            if (currentNPC == null)
            {
                inDialogue = false;
                if (dialogueUI != null)
                {
                    dialogueUI.SetActive(false);
                }
                return;
            }

            dialogueIndex++;
            if (dialogueIndex >= currentNPC.dialogueLines.Length)
            {
                inDialogue = false;
                if (dialogueUI != null)
                {
                    dialogueUI.SetActive(false);
                }
            }
            else
            {
                ShowLine();
            }
        }
    }

    private void ShowLine()
    {
        if (currentNPC == null || dialogueText == null)
            return;

        if (dialogueIndex < 0 || dialogueIndex >= currentNPC.dialogueLines.Length)
            return;

        DialogueLine line = currentNPC.dialogueLines[dialogueIndex];
        dialogueText.text = $"{line.SpeakerName}: {line.text}";
    }

    private void Shoot()
    {
        

        if (bulletPrefab != null && gunPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddForce(gunPoint.forward * bulletForce);
            }
        }
    }
}