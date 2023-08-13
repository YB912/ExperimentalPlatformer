
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float horizontalMovementSpeed = 200f;
    
    [SerializeField] PlayerInputActions playerInputActions;
    [SerializeField] LayerMask groundLayerMask;

    private float horizontalMovement;

    private Rigidbody2D rigidBody;
    private new Collider2D collider;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerInputActions = new PlayerInputActions();
        collider = GetComponent<Collider2D>();

        playerInputActions.OnFoot.Move.performed += OnMove;
        playerInputActions.OnFoot.Move.canceled += OnMove;
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
        // Handle flipping the character
        if ((transform.lossyScale.x > 0 && horizontalMovement < 0) || transform.lossyScale.x < 0 && horizontalMovement > 0) { Flip(); }

    }

    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(horizontalMovement * horizontalMovementSpeed * Time.deltaTime, rigidBody.velocity.y);
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

}
