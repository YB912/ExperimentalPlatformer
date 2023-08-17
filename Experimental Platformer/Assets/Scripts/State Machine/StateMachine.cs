
using UnityEngine;

public class StateMachine : MonoBehaviour, IStateMachine
{
    public BaseState MainState { get; set; }
    public IBaseState CurrentState { get; private set; }
    private IBaseState nextState;

    private void Start()
    {
        SetMainAsNextState();
        Debug.Log("The main state is: " + MainState.GetType());
    }
    private void Update()
    {
        if (nextState != null) { SetState(nextState); }
        CurrentState?.Update();
    }
    private void FixedUpdate()
    {
        CurrentState?.FixedUpdate();
    }
    private void LateUpdate()
    {
        CurrentState?.LateUpdate();
    }
    public void SetMainAsNextState()
    {
        nextState = MainState;
    }
    public void SetNextState(IBaseState nextState)
    {
        if (nextState != null)
        {
            this.nextState = nextState;
        }
    }
    private void SetState(IBaseState newState)
    {
        if (newState != null)
        {
            nextState = null;
            CurrentState?.ExitState();
            CurrentState = newState;
            CurrentState.EnterState(this);
            Debug.Log("Current state is: " + CurrentState.GetType());
        }
    }
}
