using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrainToSpline : MonoBehaviour
{
    public PathSpline path;
    public float splineWeight;

    private void Update()
    {
        Vector3 splinePoint = path.GetPoint(splineWeight);
        Vector3 newPosition = new Vector3(splinePoint.x, transform.position.y, splinePoint.z);
        transform.position = newPosition;
    }
}
