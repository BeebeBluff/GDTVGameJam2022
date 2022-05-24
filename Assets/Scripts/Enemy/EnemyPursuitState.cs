﻿using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyPursuitState : EnemyBaseState
    {
        public EnemyPursuitState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log("Entering pursuit state");
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            Vector3 currentPosition = stateMachine.Controller.transform.position;
            currentPosition.y = 0;

            if (Vector3.Distance(currentPosition, stateMachine.Player.position) > stateMachine.PlayerDetectionRange)
            {
                stateMachine.SwitchState(new EnemyPatrollingState(stateMachine));
                return;
            }

            stateMachine.Controller.transform.LookAt(stateMachine.Player.position);

            Vector3 movement = stateMachine.Controller.transform.forward * stateMachine.MovementSpeed * deltaTime;
            movement.y = 0;

            stateMachine.Controller.Move(movement);
        }

        public override void Exit()
        {
            base.Exit();

            Debug.Log("Exiting pursuit state");
        }
    }
}