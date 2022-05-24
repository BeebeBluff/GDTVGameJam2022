using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    //I guess we need all of these methods to override the Abstract part of the base class.
    private static readonly int IDLE_RUN_BLEND_ANIMATOR_HASH = Animator.StringToHash("FreeLookSpeed");
    private static readonly int FREE_LOOK_ANIMATOR_HASH = Animator.StringToHash("FreeLookBlendTree");
    private const float IDLE_TRANSITION_TIME = 0.1f;
    private const float FREE_LOOK_ANIMATOR_DAMP_TIME = .1f;

    private const float JUMP_TO_LANDING_TRANSITION_TIME = .1f;

    private static readonly int JUMP_ANIMATOR_HASH = Animator.StringToHash("Jump");
    private const float JUMP_VERTICAL_VELOCITY = 7;

    private float verticalVelocity;

    private bool previousTickGrounded = true;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {        //Calls base method, does all the normal stuff. But can do extra logic here that is specific to this class.
    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.InputReader.AttackEvent += InputReader_AttackEvent;
        stateMachine.InputReader.JumpEvent += InputReader_JumpEvent;


        stateMachine.Animator.CrossFadeInFixedTime(FREE_LOOK_ANIMATOR_HASH, IDLE_TRANSITION_TIME);
    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);

        Vector3 movement = CalculateMovement();

        Vector3 jump = new Vector3(0f, verticalVelocity, 0f);

        

        Move((movement * stateMachine.FreeLookMoveSpeed) + jump, deltaTime);


        verticalVelocity += Physics.gravity.y * deltaTime;

        if (!previousTickGrounded && stateMachine.IsGrounded)
        {
            // Just landed
            stateMachine.Animator.CrossFadeInFixedTime(FREE_LOOK_ANIMATOR_HASH, JUMP_TO_LANDING_TRANSITION_TIME);
        }

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(IDLE_RUN_BLEND_ANIMATOR_HASH, 0, FREE_LOOK_ANIMATOR_DAMP_TIME, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat(IDLE_RUN_BLEND_ANIMATOR_HASH, 1, FREE_LOOK_ANIMATOR_DAMP_TIME, deltaTime);
        FaceMovementDirection(movement, deltaTime);

        previousTickGrounded = stateMachine.IsGrounded;
    }

    public override void Exit()
    {
        base.Exit();

        stateMachine.InputReader.AttackEvent -= InputReader_AttackEvent;
        stateMachine.InputReader.JumpEvent -= InputReader_JumpEvent;

    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
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

    private void InputReader_AttackEvent()
    {
        //if (stateMachine.Controller.isGrounded)
        //{
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine));
        //}
    }


    private void InputReader_JumpEvent()
    {
        if (verticalVelocity < 0 && stateMachine.IsGrounded)
        {
            verticalVelocity = JUMP_VERTICAL_VELOCITY;
            stateMachine.Animator.CrossFadeInFixedTime(JUMP_ANIMATOR_HASH, .1f);
        }
    }
}
