using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override void Exit()
        {
        }

        public override void Tick(float deltaTime)
        {
        }
    }
}
