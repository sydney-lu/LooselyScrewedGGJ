using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [Range(1,10)]
    public float zoom = 1;

    private void Start()
    {
        if (target)
        {
            transform.position = target.position + Quaternion.FromToRotation(Vector3.forward, target.right) * offset * zoom;
            transform.LookAt(target);
        }
    }

    void FixedUpdate()
    {
        if (target)
        {
            transform.position = Vector3.Lerp
                (
                    transform.position,
                    target.position + offset,
                    Time.deltaTime * 8
                );
            transform.rotation = Quaternion.Lerp
                (
                    transform.rotation,
                    Quaternion.LookRotation(target.position - transform.position, Vector3.up),
                    Time.deltaTime * 3
                );
        }
    }
}