using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    //I guess we need all of these methods to override the Abstract part of the base class.
    private const string FREE_LOOK_NAME = "FreeLookSpeed";

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

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FREE_LOOK_NAME, 0, .1f, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat(FREE_LOOK_NAME, 1, .1f, deltaTime);
        stateMachine.transform.rotation = Quaternion.LookRotation(movement);

    }

    public override void Exit()
    {

    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerTestState(stateMachine));
    }


}
