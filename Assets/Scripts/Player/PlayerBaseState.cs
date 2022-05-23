using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine; //protected means only the classes that inherit this can access it.

    public PlayerBaseState(PlayerStateMachine stateMachine) // is this a Constructor?
    {
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
    }

    public override void Tick(float deltaTime)
    {
        stateMachine.Controller.Move(stateMachine.ForceReceiver.Movement * deltaTime);
    }

    public override void Exit()
    {
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move(motion * deltaTime);
    }

}
