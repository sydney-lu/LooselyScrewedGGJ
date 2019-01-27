using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrainToSpline : MonoBehaviour
{
    public PathSpline path;
    public float splineWeight;

    private void Update()
    {
        Vector3 pathpoint = path.GetPoint(splineWeight);
        transform.position = new Vector3(pathpoint.x, transform.position.y, pathpoint.z);
    }
}
