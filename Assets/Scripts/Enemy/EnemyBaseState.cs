using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public abstract class EnemyBaseState : State
    {

        protected EnemyStateMachine stateMachine;

        public EnemyBaseState(EnemyStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public override void Enter()
        {
        }

        public override void Tick(float deltaTime)
        {
            stateMachine.Controller.Move(stateMachine.ForceReceiver.Movement * deltaTime);
        }

        public override void Exit()
        {
        }
    }
}
