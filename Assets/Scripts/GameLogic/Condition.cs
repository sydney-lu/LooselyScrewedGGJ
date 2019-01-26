using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Trigger))]
public abstract class Condition : MonoBehaviour
{
    protected Trigger trigger;
    public Trigger Trigger
    {
        get { return trigger; }
        set { trigger = value; }
    }
    
    protected bool conditionIsMet = false;
    public bool ConditionMet
    {
        get { return conditionIsMet; }
        set { conditionIsMet = value; }
    }

    protected bool initialized = false;

    protected void OnEnable()
    {
        if (!trigger) trigger = GetComponent<Trigger>();
        InitCondition();
    }

    protected void OnDisable()
    {
        ResetCondition();
    }

    public abstract void InitCondition();
    public abstract void ResetCondition();
    public abstract bool CheckCondition();
}