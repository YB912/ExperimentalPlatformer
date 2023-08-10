using UnityEngine;


public class MachineGunAttackState : IBaseState
{
    public EnemyMachineGunBehavior machineGun;
    public MachineGunAttackState(EnemyMachineGunBehavior machineGun) 
    {
        this.machineGun = machineGun;
    }

    public void EnterState(StateMachine stateMachine)
    {

    }

    public void ExitState()
    {

    }

    public void Update()
    {
        machineGun.timer += Time.deltaTime;
        if (machineGun.timer > 0.5)
        {
            machineGun.timer = 0;
            machineGun.shot();
        }
    }

    public void FixedUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void LateUpdate()
    {
        throw new System.NotImplementedException();
    }
}
