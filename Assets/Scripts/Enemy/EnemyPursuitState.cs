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

            stateMachine.Animator.CrossFadeInFixedTime(RUN_ANIMATION_HASH, RUN_TRANSITION_TIME);
            stateMachine.AudioSource.PlayOneShot(stateMachine.PursuitSound);
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            if (Vector3.Distance(stateMachine.transform.position, stateMachine.Player.position) > stateMachine.PlayerDetectionRange)
            {
                // Player was lost
                stateMachine.SwitchState();
                return;
            }

            if (Vector3.Distance(stateMachine.transform.position, stateMachine.Player.position) < stateMachine.PlayerAttackRange)
            {
                //Player is within attack range
                stateMachine.SwitchState(new EnemyAttackState(stateMachine));
                return;
            }

            stateMachine.transform.rotation = Utilities.LookAt(stateMachine.transform.position,
                stateMachine.Player.position);

            Vector3 movement = stateMachine.transform.forward * stateMachine.RunSpeed * deltaTime;
            movement.y = 0;
            stateMachine.Controller.Move(movement);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
