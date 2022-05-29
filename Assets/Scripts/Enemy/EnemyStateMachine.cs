using System;
using System.Collections.Generic;
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
        [field: SerializeField] public EnemyHealth EnemyHealth { get; set; }

        [field: SerializeField] public Transform[] Waypoints { get; private set; }
        [field: SerializeField] public Transform Player { get; private set; }
        [field: SerializeField] public AudioSource AudioSource { get; private set; }
        [field: SerializeField] public AudioClip SwordSlash { get; private set; }
        [field: SerializeField] public AudioClip PursuitSound { get; private set; }
        [SerializeField] AudioClip deathSound;

        [field: SerializeField] public float WalkSpeed { get; set; }
        [field: SerializeField] public float RunSpeed { get; set; }
        [field: SerializeField] public float PlayerDetectionRange { get; set; }
        [field: SerializeField] public float PlayerAttackRange { get; set; }

        public bool IsGrounded => GroundedRayCast.IsGrounded;

        void Start()
        {
            //This line and the next are for spawned enemies.
            Waypoints = FindObjectOfType<WaypointManager>().Waypoints;
            Player = FindObjectOfType<PlayerHealth>().transform;

            SwitchState();

            EnemyHealth.DieEvent += EnemyHealth_DieEvent;
            FindObjectOfType<LevelManager>().EnemySpawned();
        }

        private void OnDestroy()
        {
            EnemyHealth.DieEvent -= EnemyHealth_DieEvent;
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

        private void EnemyHealth_DieEvent()
        {
            FindObjectOfType<LevelManager>().EnemyDied();
            AudioSource.PlayOneShot(deathSound);

            SwitchState(new EnemyDeathState(this));

            EnemyHealth.DieEvent -= EnemyHealth_DieEvent;
        }


    }
}
