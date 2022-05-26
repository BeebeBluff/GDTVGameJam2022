using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyStateMachine : StateMachine
    {
        [field: SerializeField] public CharacterController Controller { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public ForceReceiver ForceReceiver { get; set; }
        [field: SerializeField] public GroundedRayCast GroundedRayCast { get; set; }
        [field: SerializeField] public Collider WeaponCollider { get; set; } //May need to change for other enemy types

        [field: SerializeField] public Transform[] Waypoints { get; private set; }
        [field: SerializeField] public Transform Player { get; private set; }

        [field: SerializeField] public float WalkSpeed { get; set; }
        [field: SerializeField] public float RunSpeed { get; set; }
        [field: SerializeField] public float PlayerDetectionRange { get; set; }
        [field: SerializeField] public float PlayerAttackRange { get; set; }

        public bool IsGrounded => GroundedRayCast.IsGrounded;

        void Start()
        {
            SetLimbPhysics(true);

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

        private void SetLimbPhysics(bool isAlive)
        {
            Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
            Collider[] colliders = GetComponentsInChildren<Collider>();

            foreach(Rigidbody rigidbody in rigidbodies) //True if alive
            { rigidbody.isKinematic = isAlive; } //Kinematic means not affected by gravity

            foreach (Collider collider in colliders)
            { collider.enabled = !isAlive; } // False if alive

            GetComponent<CharacterController>().enabled = isAlive;
            GetComponent<Collider>().enabled = isAlive;

        }
    }
}
