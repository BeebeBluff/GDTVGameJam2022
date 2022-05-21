using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    //I guess we need all of these methods to override the Abstract part of the base class.

    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine)
    {        //Calls base method, does all the normal stuff. But can do extra logic here that is specific to this class.
    }

    public override void Enter()
    {

    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = new Vector3();
        movement.x = stateMachine.InputReader.MovementValue.x;
        movement.y = 0;
        movement.z = stateMachine.InputReader.MovementValue.y;

        stateMachine.Controller.Move(movement * stateMachine.FreeLookMoveSpeed * deltaTime);
    }

    public override void Exit()
    {

    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerTestState(stateMachine));
    }


}
