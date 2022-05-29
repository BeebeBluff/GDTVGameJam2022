using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public event Action JumpEvent; //Must add System Namespace
    public event Action DodgeEvent;
    public event Action AttackEvent;

    public Vector2 MovementValue { get; private set; } //Not an event because we need to continually read the value.
    
    private Controls controls;

    private void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this); //I think this connects the OnJump methods to the unity controls script.

        controls.Player.Enable();
    }

    private void OnDestroy() //this is only called when the level is reloaded or closed.
    {
        controls.Player.Disable();
    }

    public void DisableControls() //to pause the game.
    {
        controls.Player.Disable();
    }

    public void OnJump(InputAction.CallbackContext context) //context contains info like, duration of press, when pressed, when released
    {
        if (!context.performed) { return; }

        JumpEvent?.Invoke(); //null conditional operator because there will be an error if no one is subscribed to the event.
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        DodgeEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        AttackEvent?.Invoke();
    }

    public void OnGoToMenu(InputAction.CallbackContext context)
    {
        FindObjectOfType<LevelManager>().ShowReturnToMenuScreen();
    }
}
