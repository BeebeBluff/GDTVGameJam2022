using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyIdleState : EnemyBaseState
    {
        private static readonly int IDLE_ANIMATION_HASH = Animator.StringToHash("Idle");
        private float IDLE_TRANSITION_TIME = 0.1f;

        public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.Animator.CrossFadeInFixedTime(IDLE_ANIMATION_HASH, IDLE_TRANSITION_TIME);
        }

        public override void Tick(float deltaTime)
        {
            Vector3 currentPosition = stateMachine.Controller.transform.position;

            if (Vector3.Distance(currentPosition, stateMachine.Player.position) < stateMachine.PlayerDetectionRange)
            {
                stateMachine.SwitchState(new EnemyPursuitState(stateMachine));
                return;
            }
        }

        public override void Exit()
        {
        }
    }
}
