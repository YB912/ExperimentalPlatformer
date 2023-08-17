
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Player
{
    internal class PlayerRunningState : BaseState
    {
        private PlayerMainScript main;
        private UnityEngine.Transform transform;
        public override void EnterState(StateMachine stateMachine)
        {
            base.EnterState(stateMachine);
            main = GetComponent<PlayerMainScript>();
            transform = GetComponent<UnityEngine.Transform>();

            main.animator.SetBool("IsRunning", true);
            main.inputActions.OnFoot.Move.performed += OnMovementAction;
            main.inputActions.OnFoot.Move.canceled += OnMovementAction;
        }

        public override void Update()
        {
            base.Update();
            if (Input.anyKey == false)
            {
                StateMachine.SetNextState(new PlayerIdleState());
            }
            if ((transform.lossyScale.x > 0 && main.HorizontalMovement < 0) || transform.lossyScale.x < 0 && main.HorizontalMovement > 0) { main.Flip(); }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            main.rigidBody.velocity = new Vector2(main.HorizontalMovement * main.HorizontalMovementSpeed * Time.deltaTime, main.rigidBody.velocity.y);
        }

        public override void ExitState()
        {
            base.ExitState();
            main.animator.SetBool("IsRunning", false);

            main.inputActions.OnFoot.Move.performed -= OnMovementAction;
            main.inputActions.OnFoot.Move.canceled -= OnMovementAction;
        }

        private void OnMovementAction(InputAction.CallbackContext context)
        {
            if (context.canceled == false)
            {
                main.HorizontalMovement = context.ReadValue<Vector2>().x;
            }
        }
    }
}
