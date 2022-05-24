using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyIdleState : EnemyBaseState
    {
        public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
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
