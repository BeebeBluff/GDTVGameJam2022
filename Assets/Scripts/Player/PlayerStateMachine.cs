using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; set; }
    [field: SerializeField] public GroundedRayCast GroundedRayCast { get; set; }
    [field: SerializeField] public float FreeLookMoveSpeed { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public ProjectileHandler ProjectileHandler { get; private set; }
    [field: SerializeField] public PlayerHealth PlayerHealth { get; private set; }
    
    
    public LevelManager LevelManager { get; private set; }

    public bool IsGrounded => GroundedRayCast.IsGrounded;


    public Transform MainCameraTransform { get; private set; }

    void Start()
    {
        Time.timeScale = 1; //No clue why I need this here, but it won't work in LevelManager.

        MainCameraTransform = Camera.main.transform;

        LevelManager = FindObjectOfType<LevelManager>();

        PlayerHealth.PlayerDeathEvent += PlayerHealth_PlayerDeathEvent;

        SwitchState(new PlayerFreeLookState(this));
    }

    private void OnDestroy()
    {
        PlayerHealth.PlayerDeathEvent -= PlayerHealth_PlayerDeathEvent;
    }

    private void PlayerHealth_PlayerDeathEvent()
    {
        SwitchState(new PlayerDeathState(this));

        PlayerHealth.PlayerDeathEvent -= PlayerHealth_PlayerDeathEvent;
    }

}
