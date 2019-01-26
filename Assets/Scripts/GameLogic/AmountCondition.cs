using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FindType
{
    Tag,
    Layer,
    ByType
}

public enum CompareType
{
    LessThan,
    GreaterThan,
    EqualTo,
    LessThanOrEqual,
    GreaterThanOrEqual,
}

public class AmountCondition : AreaCondition
{
    [SerializeField,HideInInspector]
    public FindType findBy;
    [SerializeField, HideInInspector]
    private CompareType compareBy;

    [SerializeField, HideInInspector]
    private string checkTag;
    [SerializeField, HideInInspector]
    private LayerMask layer;
    [SerializeField, HideInInspector]
    private Object typeTemplate;

    [SerializeField, HideInInspector]
    private int amount;
    [SerializeField, HideInInspector]
    private int currentObjects = 0;

    public override void InitCondition() { initialized = true; }
    public override bool CheckCondition()
    {
        currentObjects = bodies.Count;

        // - Compare Tests - 
        if (compareBy == CompareType.LessThan && currentObjects < amount)
            return true;

        else if (compareBy == CompareType.GreaterThan && currentObjects > amount)
            return true;

        else if (compareBy == CompareType.EqualTo && currentObjects == amount)
            return true;

        else if (compareBy == CompareType.LessThanOrEqual && currentObjects <= amount)
            return true;

        else if (compareBy == CompareType.GreaterThanOrEqual && currentObjects >= amount)
            return true;

        return false;
    }
    public override void ResetCondition() { conditionIsMet = false; }

    private bool CheckBase(Collider other)
    {
        if (findBy == FindType.Tag && other.attachedRigidbody.tag == checkTag)
            return true;

        else if (findBy == FindType.Layer && other.attachedRigidbody.gameObject.layer == layer)
            return true;

        else if (findBy == FindType.ByType && typeTemplate != null && other.GetComponentsInChildren(typeTemplate.GetType()) != null)
            return true;

        else return false;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (CheckBase(other)) base.OnTriggerEnter(other);
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (CheckBase(other)) base.OnTriggerExit(other);
    }
}
