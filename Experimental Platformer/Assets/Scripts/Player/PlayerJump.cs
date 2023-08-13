
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] float jumpForce = 20f;
    [SerializeField] int jumpTimes = 2;
    [SerializeField] float groundedBoxCastOffset = 1f;
    [SerializeField] float maxJumpDuration = 1f;

    [SerializeField] PlayerInputActions playerInputActions;
    [SerializeField] LayerMask groundLayerMask;

    private int jumpCounter;
    private float jumpDuration;
    private bool isJumping;

    private Rigidbody2D rigidBody;
    private new Collider2D collider;

    private void Awake()
    {
        jumpCounter = 0;
        rigidBody = GetComponent<Rigidbody2D>();
        playerInputActions = new PlayerInputActions();
        collider = GetComponent<Collider2D>();

        playerInputActions.OnFoot.Jump.started += OnJump;
        playerInputActions.OnFoot.Jump.canceled += OnJump;
    }

    private void OnEnable()
    {
        playerInputActions.OnFoot.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.OnFoot.Disable();
    }

    private void Update()
    {
        if (isJumping)
        {
            jumpDuration += Time.deltaTime;
            SetVerticalVelocity();
            if (jumpDuration >= maxJumpDuration) 
            {
                CancelJumping();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsGrounded())
        {
            jumpCounter = 0;
            CancelJumping();
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (jumpCounter < jumpTimes)
            {
                isJumping = true;
                jumpCounter++;
                Debug.Log(jumpCounter);
            }
        } 
        else if (context.canceled)
        {
            CancelJumping();
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, groundedBoxCastOffset);
    }

    private void SetVerticalVelocity()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
    }

    private void CancelJumping()
    {
        isJumping = false;
        jumpDuration = 0;
    }
}
