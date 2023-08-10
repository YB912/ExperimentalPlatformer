
using UnityEngine;

public class StateMachine : MonoBehaviour, IStateMachine
{
    [SerializeField] BaseState mainState;
    public IBaseState CurrentState { get; private set; }
    private IBaseState nextState;

    void Awake()
    {
        SetMainAsNextState();
    }
    void Update()
    {
        if (nextState != null) { SetState(nextState); }
        CurrentState?.Update();
    }
    void FixedUpdate()
    {
        CurrentState?.FixedUpdate();
    }
    void LateUpdate()
    {
        CurrentState?.LateUpdate();
    }
    public void SetMainAsNextState()
    {
        nextState = mainState;
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
        }
    }
}
