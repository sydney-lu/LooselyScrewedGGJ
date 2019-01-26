using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class AreaCondition : Condition
{
    [SerializeField]
    private GameObject checkObject;
    [SerializeField]
    private bool UsePlayer;

    protected Collider triggerArea;
    protected List<GameObject> bodies = new List<GameObject>();

    private void Awake()
    {
        triggerArea = GetComponent<Collider>();
        triggerArea.isTrigger = true;
    }

    public override void InitCondition() { if(checkObject || (UsePlayer && GameManager.player)) initialized = true; }
    public override bool CheckCondition()
    {
        if (UsePlayer && GameManager.player)
            checkObject = GameManager.player.gameObject;

        if (checkObject)
            for (int i = 0; i < bodies.Count; i++)
                if (bodies[i] == checkObject)
                    return conditionIsMet = true;

        return conditionIsMet = false;
    }
    public override void ResetCondition() { conditionIsMet = false; }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody)
        {
            if (!bodies.Contains(other.attachedRigidbody.gameObject))
            {
                bodies.Add(other.attachedRigidbody.gameObject);
                CheckCondition();
            }
        }
        else
        {
            if (!bodies.Contains(other.gameObject))
            {
                bodies.Add(other.gameObject);
                CheckCondition();
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody)
        {
            if (bodies.Contains(other.attachedRigidbody.gameObject))
            {
                bodies.Remove(other.attachedRigidbody.gameObject);
                CheckCondition();
            }
        }
        else
        {
            if (!bodies.Contains(other.gameObject))
            {
                bodies.Remove(other.gameObject);
                CheckCondition();
            }
        }
    }
}
