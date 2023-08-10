public interface IStateMachine 
{
    public IBaseState CurrentState { get; }

    public void SetNextState(IBaseState nextState);
    public void SetMainAsNextState();

}
