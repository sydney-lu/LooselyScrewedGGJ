using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class Breakable : MonoBehaviour
{
    HealthComponent hc;

    private void Awake()
    {
        hc = GetComponent<HealthComponent>();
    }

    private void Update()
    {
        if (hc.isDead)
            Break();
    }

    public void Break()
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).parent = null;
        Destroy(gameObject);
    }
}
