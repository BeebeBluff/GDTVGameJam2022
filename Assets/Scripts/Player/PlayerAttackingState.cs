using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private static readonly int ATTACK_ANIMATOR_HASH = Animator.StringToHash("Attack");
    private float ATTACK_TRANSITION_TIME = 0.1f;


    public PlayerAttackingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Switching to attack mode");

        stateMachine.Animator.CrossFadeInFixedTime(ATTACK_ANIMATOR_HASH, ATTACK_TRANSITION_TIME);
    }

    public override void Tick(float deltaTime)
    {
        float normalizedTime = GetNormalizedTime();

        if (normalizedTime >= 1f)
        {
            Debug.Log(stateMachine.Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"));

            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }

    public override void Exit()
    {
        Debug.Log("Leaving attack mode");
    }

    private float GetNormalizedTime()
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
