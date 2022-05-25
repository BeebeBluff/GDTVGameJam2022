using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyStateMachine : StateMachine
    {
        [field: SerializeField] public CharacterController Controller { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public ForceReceiver ForceReceiver { get; set; }
        [field: SerializeField] public GroundedRayCast GroundedRayCast { get; set; }

        [field: SerializeField] public Transform[] Waypoints { get; private set; }
        [field: SerializeField] public Transform Player { get; private set; }

        [field: SerializeField] public float WalkSpeed { get; set; }
        [field: SerializeField] public float RunSpeed { get; set; }
        [field: SerializeField] public float PlayerDetectionRange { get; set; }


        void Start()
        {
            SwitchState();
        }

        public void SwitchState()
        {
            if (Waypoints.Length == 0)
            {
                SwitchState(new EnemyIdleState(this));
            }
            else
            {
                SwitchState(new EnemyPatrollingState(this));
            }
        }
    }
}
