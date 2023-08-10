using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float horizontalMovementSpeed = 200f;
    [SerializeField] float jumpSpeed = 30f;
    [SerializeField] int jumpTimes;
    [SerializeField] float groundedBoxCastOffset;
    [SerializeField] PlayerInputActions playerInputActions;
    [SerializeField] LayerMask groundLayerMask;
    private float horizontalMovement;
    private int jumpCounter;
    private Rigidbody2D rigidBody;
    private new Collider2D collider;
    private void Awake()
    {
        jumpCounter = 0;
        rigidBody = GetComponent<Rigidbody2D>();
        playerInputActions = new PlayerInputActions();
        collider = GetComponent<Collider2D>();

        playerInputActions.OnFoot.Enable();
        playerInputActions.OnFoot.Jump.performed += OnJump;
        playerInputActions.OnFoot.Move.performed += OnMove;
        playerInputActions.OnFoot.Move.canceled += OnMove;
    }
    void Update()
    {
        // Handle flipping the character
        if ((transform.lossyScale.x > 0 && horizontalMovement < 0) || transform.lossyScale.x < 0 && horizontalMovement > 0) { Flip(); }
    }
    void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(horizontalMovement * horizontalMovementSpeed * Time.deltaTime, rigidBody.velocity.y);
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsGrounded()) { jumpCounter = 0; }
        Debug.Log(IsGrounded());
    }
    private void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (jumpCounter < jumpTimes)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
                Debug.Log(context.duration);
                jumpCounter++;
            }
        }
    }
    private void OnMove(InputAction.CallbackContext context)
    {
        if (context.canceled == false)
        {
            horizontalMovement = context.ReadValue<Vector2>().x;
        } 
        else
        {
            horizontalMovement = 0;
        }
    }
    private void Flip()
    {
        var scale = transform.lossyScale;
        transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, groundedBoxCastOffset);
    }
}
