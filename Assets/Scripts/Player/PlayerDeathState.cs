using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerBaseState
{
    private static readonly int DEATH_ANIMATION_HASH = Animator.StringToHash("Death");
    private readonly float DEATH_TRANSITION_TIME = 0.5f;

    private float deathTransitionDelay = 3f;

    public PlayerDeathState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Alright y'all! We're in death mode now!");

        stateMachine.Animator.CrossFadeInFixedTime(DEATH_ANIMATION_HASH, DEATH_TRANSITION_TIME);
        //For some reason, this worked here, but not when called in PlayerHealth... weird.
    }

    public override void Tick(float deltaTime)
    {
        deathTransitionDelay -= deltaTime;

        if (deathTransitionDelay <= 0f)
        {
            stateMachine.LevelManager.GamePause();
        }
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Goodbye cruel world!");
    }

}
