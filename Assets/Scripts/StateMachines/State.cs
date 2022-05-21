using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    //This is the base State class. This base doesn't need logic. It's just showing what method each object that inherits this class will need.

    public abstract void Enter(); //if you are a state, you have to have an enter method that doesn't take or return anything.

    public abstract void Tick(float deltaTime); //Tick will be called each frame. So pass through the deltaTime float so the object/player can be fps independent.

    public abstract void Exit();
}
