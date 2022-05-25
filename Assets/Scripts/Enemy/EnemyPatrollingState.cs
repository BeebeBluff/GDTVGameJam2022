using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyPatrollingState : EnemyBaseState
    {
        private static readonly int WALK_ANIMATION_HASH = Animator.StringToHash("WalkForward");
        private float WALK_TRANSITION_TIME = 0.1f;

        private const float WAYPOINT_MIN_DISTANCE = 1f;

        private int currentWaypointIndex;
        private Vector3 currentWaypointPosition;

        public EnemyPatrollingState(EnemyStateMachine stateMachine) : base(stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();

            stateMachine.Animator.CrossFadeInFixedTime(WALK_ANIMATION_HASH, WALK_TRANSITION_TIME);

            currentWaypointIndex = 0;
            currentWaypointPosition = stateMachine.Waypoints[currentWaypointIndex].position;
            currentWaypointPosition.y = 0;
            LookAtWaypoint();
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            Vector3 currentPosition = stateMachine.Controller.transform.position;

            if (Vector3.Distance(currentPosition, stateMachine.Player.position) < stateMachine.PlayerDetectionRange)
            {
                stateMachine.SwitchState(new EnemyPursuitState(stateMachine));
                return;
            }

            // Ignore y for waypoints
            currentPosition.y = 0;
            float distance = Vector3.Distance(currentPosition, currentWaypointPosition);

            if (distance < WAYPOINT_MIN_DISTANCE)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % stateMachine.Waypoints.Length;
                currentWaypointPosition = stateMachine.Waypoints[currentWaypointIndex].position;
                currentWaypointPosition.y = 0;
                LookAtWaypoint();
            }

            Vector3 movement = stateMachine.Controller.transform.forward * stateMachine.WalkSpeed * deltaTime;
            movement.y = 0;

            stateMachine.Controller.Move(movement);
        }

        public override void Exit()
        {
            base.Exit();
        }

        private void LookAtWaypoint()
        {
            stateMachine.Controller.transform.LookAt(currentWaypointPosition);
        }
    }
}
