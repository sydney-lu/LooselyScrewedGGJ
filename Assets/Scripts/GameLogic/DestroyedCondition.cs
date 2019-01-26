using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedCondition : Condition
{
    [SerializeField]
    private Object checkObject;
    [SerializeField]
    private bool isAlive;

    public override void InitCondition() { if (checkObject) initialized = true; }
    public override bool CheckCondition()
    {
        return conditionIsMet = (isAlive && checkObject != null) || (!isAlive && checkObject == null);
    }
    public override void ResetCondition() { conditionIsMet = false; }
}
