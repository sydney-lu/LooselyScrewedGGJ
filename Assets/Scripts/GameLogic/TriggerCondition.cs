using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCondition : Condition
{
    [SerializeField]
    private Trigger referenceTrigger;
    [SerializeField]
    private bool isTrue;

    public override void InitCondition() { if(referenceTrigger)initialized = true; }
    public override bool CheckCondition()
    {
        if (!initialized) return true;
        if (referenceTrigger)
            return conditionIsMet = referenceTrigger.ConditionsMet;
        else return true;
    }
    public override void ResetCondition() { conditionIsMet = false; }
}
