﻿using System.Collections;
using UnityEngine;

public abstract class BaseState
{
    protected StateManager stateManager;

    /// <summary>
    /// True Once Base.EnterState Coroutine Completes.
    /// </summary>
    protected bool hasStarted = false;

    /// <summary>
    /// True Once Base.ExitState Coroutine Completes.
    /// </summary>
    protected bool hasStopped = false;

    protected float elapsedTime;
    protected bool inTransition = false;
    public bool InTransition
    {
        get {return inTransition; }
        set {inTransition = value; }
    }

    //Constructor
    public BaseState(StateManager stateM)
    {
        stateManager = stateM;
        elapsedTime = 0;
    }

    //Transition Functions
    /// <summary>
    /// Called when the State is Entered before first StateUpdate, This is the Current State.
    /// </summary>
    /// <param name="prevState"></param>
    /// <returns></returns>
    public virtual IEnumerator EnterState(BaseState prevState)
    {
        if (this != stateManager.State)
        {
            Debug.LogWarning(stateManager.gameObject + "has a RogueState: " + this + "\t\tCurrent State:" + stateManager.State);
            hasStopped = true;
        }
        else
        {
            hasStarted = true;
            yield return null;
        }
    }

    /// <summary>
    /// Called when the State is Exited after final StateUpdate, This is still the Current State.
    /// </summary>
    /// <param name="prevState"></param>
    /// <returns></returns>
    public virtual IEnumerator ExitState(BaseState nextState)
    {
        hasStopped = true;
        yield return null;
    }

    //State Updates
    public virtual void Update()
    {
        if (hasStarted && !hasStopped)
        {
            if (stateManager.IsPaused)
                UpdatePaused();

            else if (inTransition)
                UpdateTransition();

            else UpdateState();

            elapsedTime += Time.deltaTime;
        }
    }

    public abstract void FixedUpdate();

    /// <summary>
    /// Regular Update, Called from StateManager Update
    /// </summary>
    protected abstract void UpdateState();

    /// <summary>
    /// Paused Update, Update Called when StateManager is Paused
    /// </summary>
    protected abstract void UpdatePaused();

    /// <summary>
    /// Transition Update, Update Called when StateManager is Transitioning
    /// </summary>
    protected abstract void UpdateTransition();

    //Trigger Functions
    public abstract void OnTriggerEnter(Collider collider);
    public abstract void OnTriggerStay(Collider collider);
    public abstract void OnTriggerExit(Collider collider);

    //Colission Functions
    public abstract void OnCollisionEnter(Collision collision);
    public abstract void OnCollisionStay(Collision collision);
    public abstract void OnCollisionExit(Collision collision);
}