using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyPursuitState : EnemyBaseState
    {
        private static readonly int RUN_ANIMATION_HASH = Animator.StringToHash("Running");
        private float RUN_TRANSITION_TIME = 0.1f;

        public EnemyPursuitState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Entering pursuit state");

            stateMachine.Animator.CrossFadeInFixedTime(RUN_ANIMATION_HASH, RUN_TRANSITION_TIME);
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            Vector3 currentPosition = stateMachine.Controller.transform.position;
            currentPosition.y = 0;
            Vector3 playerPosition = stateMachine.Player.position;
            playerPosition.y = 0;

            if (Vector3.Distance(currentPosition, playerPosition) > stateMachine.PlayerDetectionRange)
            {
                stateMachine.SwitchState();
                return;
            }

            stateMachine.Controller.transform.LookAt(playerPosition);

            Vector3 movement = stateMachine.Controller.transform.forward * stateMachine.RunSpeed * deltaTime;
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
