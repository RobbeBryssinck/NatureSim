using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : MonoBehaviour
{
    protected enum MovementState
    {
        Idle,
        Walking,
    }

    public float WalkSpeed;

    protected MovementState CurrentMovementState;

    public IIdleBehavior IdleBehavior { get; set; }
    public void PerformIdle()
    {
        IdleBehavior.StandStill();
    }

    public IWalkBehavior WalkBehavior { get; set; }
    public bool PerformWalk()
    {
        return WalkBehavior.Move();
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
        switch (CurrentMovementState)
        {
            case MovementState.Idle:
                PerformIdle();
                break;
            case MovementState.Walking:
                if (PerformWalk())
                    CurrentMovementState = MovementState.Idle;
                break;
            default:
                break;
        }
    }
}
