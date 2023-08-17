using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    internal class PlayerIdleState : BaseState
    {
        private PlayerMainScript main;
        public override void EnterState(StateMachine stateMachine)
        {
            base.EnterState(stateMachine);
            main = GetComponent<PlayerMainScript>();

            main.inputActions.OnFoot.Move.started += OnMovementAction;
            main.inputActions.OnFoot.Jump.started += OnMovementAction;

            main.HorizontalMovement = 0;
            main.rigidBody.velocity = Vector2.zero;
        }

        private void OnMovementAction(InputAction.CallbackContext context)
        {
            main.HorizontalMovement = context.ReadValue<Vector2>().x;
            if (context.ReadValue<Vector2>().x != 0)
            {
                StateMachine.SetNextState(new PlayerRunningState());
            }
        }

        public override void ExitState()
        {
            base.ExitState();
            main.inputActions.OnFoot.Move.started -= OnMovementAction;
            main.inputActions.OnFoot.Jump.started -= OnMovementAction;
        }

    }
}
