using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    private const float JUMP_VERTICAL_VELOCITY = 7;

    protected PlayerStateMachine stateMachine; //protected means only the classes that inherit this can access it.

    private float verticalVelocity;

    public PlayerBaseState(PlayerStateMachine stateMachine) // is this a Constructor?
    {
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
        stateMachine.InputReader.JumpEvent += InputReader_JumpEvent;
    }

    public override void Tick(float deltaTime)
    {
        Vector3 jump = new Vector3(0f, verticalVelocity, 0f);

        stateMachine.Controller.Move((stateMachine.ForceReceiver.Movement + jump) * deltaTime);

        verticalVelocity += Physics.gravity.y * deltaTime;
    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= InputReader_JumpEvent;
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move(motion * deltaTime);
    }

    private void InputReader_JumpEvent()
    {
        if (verticalVelocity < 0 && stateMachine.Controller.isGrounded)
        {
            verticalVelocity = JUMP_VERTICAL_VELOCITY;
        }
    }
}
