using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    //I guess we need all of these methods to override the Abstract part of the base class.
    private static readonly int FREE_LOOK_ANIMATOR_HASH = Animator.StringToHash("FreeLookSpeed");
    private const float FREE_LOOK_ANIMATOR_DAMP_TIME = .1f;
    private const float JUMP_SPEED = .15f;


    private float verticalVelocity = 0;
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {        //Calls base method, does all the normal stuff. But can do extra logic here that is specific to this class.
        stateMachine.InputReader.JumpEvent += InputReader_JumpEvent;
    }

    private void InputReader_JumpEvent()
    {
        if (stateMachine.Controller.isGrounded)
        {
            verticalVelocity = JUMP_SPEED;
        }
    }

    public override void Enter()
    {

    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();

        Move(movement * stateMachine.FreeLookMoveSpeed, deltaTime);

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FREE_LOOK_ANIMATOR_HASH, 0, FREE_LOOK_ANIMATOR_DAMP_TIME, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat(FREE_LOOK_ANIMATOR_HASH, 1, FREE_LOOK_ANIMATOR_DAMP_TIME, deltaTime);
        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {

    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        //movement.y = 0;
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation,
            Quaternion.LookRotation(movement), deltaTime * stateMachine.RotationDamping);
    }

    private Vector3 CalculateMovement()
    {
        Vector3 right = stateMachine.MainCameraTransform.right;
        right.y = 0;
        right.Normalize();
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        forward.y = 0;
        forward.Normalize();

        return forward * stateMachine.InputReader.MovementValue.y +
            right * stateMachine.InputReader.MovementValue.x;
    }
}
