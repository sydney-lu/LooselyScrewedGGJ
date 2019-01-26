using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedCondition : Condition
{
    [SerializeField]
    private float timerAmount = 1;
    [SerializeField]
    private bool loop;
    private bool isCounting;

    IEnumerator StartTimer()
    {
        isCounting = true;
        yield return new WaitForSeconds(timerAmount);
        ConditionMet = true;
        if (loop)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            isCounting = false;
        }
    }

    public override void InitCondition() { initialized = true; }
    public override bool CheckCondition()
    {
        if (!initialized) return true;
        if (!isCounting)
        {
            StartCoroutine(StartTimer());
            ConditionMet = false;
        }
        return conditionIsMet;
    }

    public override void ResetCondition()
    {
        conditionIsMet = false;
        StopAllCoroutines();
        isCounting = false;
    }
}