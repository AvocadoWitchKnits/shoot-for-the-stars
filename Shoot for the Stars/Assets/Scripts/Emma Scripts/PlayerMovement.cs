using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Components")]
    private Rigidbody rb;      
    private Animator animator;

    private Vector2 movement;

    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
      
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

       
        float currentSpeed = Mathf.Abs(movement.x) + Mathf.Abs(movement.y);
        animator.SetFloat("Speed", currentSpeed);

        
    }

    
    
}
