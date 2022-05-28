using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyAttackState : EnemyBaseState
    {
        private static readonly int ATTACK_ANIMATION_HASH = Animator.StringToHash("Attack");
        private float ATTACK_TRANSITION_TIME = 0.1f;

        public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            stateMachine.WeaponCollider.enabled = true; //turn on sword collider

            stateMachine.Animator.CrossFadeInFixedTime(ATTACK_ANIMATION_HASH, 
                ATTACK_TRANSITION_TIME);
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            if(GetNormalizedAttackAnimTime() <= 1f) { return; }

            CheckPlayerDistance();
        }

        public override void Exit()
        {
            base.Exit();
        }

        private float GetNormalizedAttackAnimTime()
        {
            AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
            AnimatorStateInfo nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);

            if (stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
            {
                return nextInfo.normalizedTime;
            }
            else if (!stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
            {
                return currentInfo.normalizedTime;
            }
            else
            {
                return 0f;
            }
        }

        private void CheckPlayerDistance() //Maybe put this method in Utilities? Or EnemyStateMachine?
        {
            if (Vector3.Distance(stateMachine.transform.position, stateMachine.Player.position) > stateMachine.PlayerAttackRange)
            {
                //Player is slightly too far
                stateMachine.SwitchState(new EnemyPursuitState(stateMachine));
            }
            else if (Vector3.Distance(stateMachine.transform.position, stateMachine.Player.position) < stateMachine.PlayerAttackRange)
            {
                //Player is still within attack range
                stateMachine.SwitchState(new EnemyAttackState(stateMachine));
            }
            else
            {
                //Player is way out of range. (somehow)
                stateMachine.SwitchState();
            }
        }
    }
}
