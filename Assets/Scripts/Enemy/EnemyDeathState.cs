using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyDeathState : EnemyBaseState
    {
        float timer;
        float decaySpeed = 10f;
        bool decayBegun = false;

        public EnemyDeathState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            timer = decaySpeed;
        }

        public override void Tick(float deltaTime)
        {
            
            timer -= deltaTime;

            if (!decayBegun && timer <= 0f)
            {
                stateMachine.EnemyHealth.BeginDisintegration();
                stateMachine.gameObject.transform.position = -Vector3.up * deltaTime;
                decayBegun = true;

                timer = decaySpeed;
            }
            else if (decayBegun && timer <= 0f)
            { 
                //Destroy the object
            }

        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
