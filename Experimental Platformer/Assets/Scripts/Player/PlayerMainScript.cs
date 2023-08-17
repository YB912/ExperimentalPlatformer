using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    internal class PlayerMainScript : MonoBehaviour
    {
        public float HorizontalMovement;
        public float HorizontalMovementSpeed = 200f;

        public float JumpForce = 20f;
        public int JumpTimes = 2;
        public float GroundedBoxCastOffset = 1f;
        public float MaxJumpDuration = 1f;

        public LayerMask GroundLayerMask;

        [HideInInspector]
        public StateMachine stateMachine;

        [HideInInspector]
        public Animator animator;

        [HideInInspector]
        public PlayerInputActions inputActions;

        [HideInInspector]
        public Rigidbody2D rigidBody;


        private void Awake()
        {
            stateMachine = GetComponent<StateMachine>();
            stateMachine.MainState = new PlayerIdleState();
            animator = GetComponent<Animator>();
            inputActions = new PlayerInputActions();
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void Flip()
        {
            var scale = transform.lossyScale;
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        }
    }
}
