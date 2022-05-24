using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyPatrollingState : EnemyBaseState
    {
        private const float WAYPOINT_MIN_DISTANCE = 1f;

        private int currentWaypointIndex;
        private Vector3 currentWaypointPosition;

        public EnemyPatrollingState(EnemyStateMachine stateMachine) : base(stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();

            currentWaypointIndex = 0;
            currentWaypointPosition = stateMachine.Waypoints[currentWaypointIndex].position;
            currentWaypointPosition.y = 0;
            LookAtWaypoint();
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);


            Vector3 currentPosition = stateMachine.Controller.transform.position;
            currentPosition.y = 0;

            if(Vector3.Distance(currentPosition, stateMachine.Player.position) < stateMachine.PlayerDetectionRange)
            {
                stateMachine.SwitchState(new EnemyPursuitState(stateMachine));
                return;
            }

            float distance = Vector3.Distance(currentPosition, currentWaypointPosition);

            if (distance < WAYPOINT_MIN_DISTANCE)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % stateMachine.Waypoints.Length;
                currentWaypointPosition = stateMachine.Waypoints[currentWaypointIndex].position;
                currentWaypointPosition.y = 0;
                LookAtWaypoint();
            }

            Vector3 movement = stateMachine.Controller.transform.forward * stateMachine.MovementSpeed * deltaTime;
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
