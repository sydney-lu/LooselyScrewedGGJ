using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class DamageObject : MonoBehaviour
{
    [Header("Damage settings")]
    [SerializeField] private float damage;
    [SerializeField] private bool singleUse;
    [SerializeField] private LayerMask hitmask;
    [SerializeField] private StatusEffect[] StatusEffects;

    [Header("AOE settings")]
    [SerializeField] private float radius;
    [SerializeField] private float farDamage;

    [Header("DOT settings")]
    [SerializeField] private float tickAmount;
    [SerializeField] private float tickInterval;
    [SerializeField] private float tickDamage;

    private Actor owner;
    public Actor Owner
    {
        get { return owner; }
        set { owner = value; }
    }

    private Collider col;
    public Collider Collider { get { return col; } }

    private void Awake()
    {
        col = GetComponent<Collider>();
        Rigidbody rb = GetComponent<Rigidbody>();
        radius = Mathf.Max(0.1f, radius);
    }

    private void OnEnable()
    {
        col.enabled = true;
    }

    private void OnDisable()
    {
        col.enabled = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, hitmask);
        List<HealthComponent> hitHCs = new List<HealthComponent>();

        Rigidbody rb;
        HealthComponent hc;

        for (int i = 0; i < colliders.Length; i++)
        {
            rb = colliders[i].attachedRigidbody;
            if (rb)
            {
                hc = rb.GetComponent<HealthComponent>();
                if (hc) hitHCs.Add(hc);
            }
        }
        for (int i = 0; i < hitHCs.Count; i++)
        {
            float distance = (transform.position - colliders[i].transform.position).magnitude;
            float falloffDamage = Mathf.Lerp(damage, farDamage, distance / radius);

            hitHCs[i].TakeDamage(falloffDamage);
            hitHCs[i].ApplyStatusEffect(StatusEffects);
            hitHCs[i].StartCoroutine(hitHCs[i].TakeTickDamage(tickAmount, tickInterval, tickDamage));
        }
        if (singleUse) Destroy(this);
    }
}
