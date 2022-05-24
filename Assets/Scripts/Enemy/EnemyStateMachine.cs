using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyStateMachine : StateMachine
    {
        [field: SerializeField] public CharacterController Controller { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public Transform[] Waypoints { get; private set; }

        [field: SerializeField] public float MovementSpeed { get; set; }

        void Start()
        {
            SwitchState(new EnemyPatrollingState(this));
        }
        
    }
}
