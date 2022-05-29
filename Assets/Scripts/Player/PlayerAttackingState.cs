using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private static readonly int ATTACK_ANIMATOR_HASH = Animator.StringToHash("Attack");
    private float ATTACK_TRANSITION_TIME = 0.1f;

    private bool fired = false;


    public PlayerAttackingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.Animator.CrossFadeInFixedTime(ATTACK_ANIMATOR_HASH, ATTACK_TRANSITION_TIME);

        LookAtMouseClick();

        stateMachine.ProjectileHandler.LoadArrow();
        stateMachine.AudioSource.PlayOneShot(stateMachine.BowSound);
    }

    private void LookAtMouseClick()
    {
        Vector3 MousePos = stateMachine.MainCameraTransform.forward;
        MousePos.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(MousePos);
    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);

        float normalizedTime = GetNormalizedAttackAnimTime();

        if (normalizedTime >= 0.5f && !fired)
        {
            stateMachine.ProjectileHandler.ShootArrow();
            fired = true;
        }

        if (normalizedTime >= 1f)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    private float GetNormalizedAttackAnimTime()
    {
        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);

        if (stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        else if (!stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
}
