using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento:")]
    public float speed = 5f;
    private Vector3 movementInput;
    [Header("Rotación de la Cámara")]
    public float rotationSpeed = 100f;  
    private Vector2 lookInput;

    private Rigidbody _rigidbody;
    private Animator _animator;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        float rotationY = lookInput.x * rotationSpeed * Time.deltaTime;
        transform.Rotate(new Vector3(0, rotationY * Time.deltaTime * rotationSpeed));
    }
    public void ReadDirection(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementInput.x, 0, movementInput.y) * speed;
        movement.y = _rigidbody.velocity.y;

        _rigidbody.velocity = movement;

        if (movementInput != Vector3.zero) 
        {
            _animator.SetBool("Running", true);
        }
        else 
        {
            _animator.SetBool("Running", false);
        }
    }
}