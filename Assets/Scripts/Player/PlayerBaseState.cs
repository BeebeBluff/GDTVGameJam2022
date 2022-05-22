using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    private const float JUMP_VERTICAL_VELOCITY = 6;

    protected PlayerStateMachine stateMachine; //protected means only the classes that inherit this can access it.

    private float verticalVelocity;

    public PlayerBaseState(PlayerStateMachine stateMachine) // is this a Constructor?
    {
        this.stateMachine = stateMachine;

        stateMachine.InputReader.JumpEvent += InputReader_JumpEvent;
    }

    private void InputReader_JumpEvent()
    {
        if (stateMachine.Controller.isGrounded)
        {
            verticalVelocity = JUMP_VERTICAL_VELOCITY;
        }
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        Vector3 jump = new Vector3(0f, verticalVelocity, 0f);

        stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement + jump) * deltaTime);

        verticalVelocity += Physics.gravity.y * deltaTime;
    }
}
