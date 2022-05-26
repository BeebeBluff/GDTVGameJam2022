using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyDeathState : EnemyBaseState
    {
        public EnemyDeathState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Tick(float deltaTime)
        {
            //base.Tick(deltaTime);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
