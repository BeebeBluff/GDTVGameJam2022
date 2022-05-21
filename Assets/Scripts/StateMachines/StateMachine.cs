using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    //This stores the current state of the object and has the logic on how to switch to another state.
    //This class is abstract so Unity won't allow us to attach this to a gameobject. We only want to attach subclasses that inherit from this.

    private State currentState;

    void Update()
    {
        currentState?.Tick(Time.deltaTime);     //Null conditional operator: won't work with monobehavior or scriptable objects
    }

    public void SwitchState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
}
