using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType
{
    Pressed,
    Held,
    Released
}

public class InputCondition : Condition
{
    [SerializeField]
    private string button;
    [SerializeField]
    private InputType type;

    public override void InitCondition() { if(button != null) initialized = true; }
    public override bool CheckCondition()
    {
        if (type == InputType.Pressed)
            return conditionIsMet = Input.GetButtonDown(button);

        else if (type == InputType.Held)
        {
            Debug.Log(Input.GetButton(button));
            return conditionIsMet = Input.GetButton(button);
        }

        else if (type == InputType.Released)
            return conditionIsMet = Input.GetButtonUp(button);

        else return false;
    }
    public override void ResetCondition() { conditionIsMet = false; }
}
