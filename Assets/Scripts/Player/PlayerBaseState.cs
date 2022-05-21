using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine; //protected means only the classes that inherit this can access it.

    public PlayerBaseState(PlayerStateMachine stateMachine) // is this a Constructor?
    {
        this.stateMachine = stateMachine;
    }
}
