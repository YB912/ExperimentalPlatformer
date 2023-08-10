
public interface IBaseState
{
    public void EnterState(StateMachine stateMachine);

    public void ExitState();

    public void Update();

    public void FixedUpdate();

    public void LateUpdate();
}
