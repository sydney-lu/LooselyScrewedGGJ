using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRotate : MonoBehaviour
{
    public float RotationSpeed;

    void Start()
    {
        if (RotationSpeed <= 0f)
        {
            RotationSpeed = 1.0f;
        }
    }

    void Update()
    {
        transform.Rotate(RotationSpeed, 0, 0);   
    }
}
