using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic : IState
{
    Transform ownerTransform;
    ArcherBlackBoard movementbb;
    MovementStateHandler statehandler;
    CharacterController controller;

    public Basic()
    {

    }
    public void Enter() { }

    public void Execute() { }

    public void Exit() { }
}
