using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathSpline : BezierSpline
{
    public List<PathSpline> StartPaths;
    public List<PathSpline> EndPaths;

    private void Start()
    {
        SortPaths();
    }

    private void SortPaths()
    {
        for (int i = 0; i < StartPaths.Count-1; i++)
        {
            if (StartPaths[i].GetPoint(1).y < StartPaths[i + 1].GetPoint(1).y)
            {
                PathSpline temp = StartPaths[i];
                StartPaths[i] = StartPaths[i + 1];
            }
        }

        for (int i = 0; i < EndPaths.Count-1; i++)
        {
            if (EndPaths[i].GetPoint(0).y < EndPaths[i + 1].GetPoint(0).y)
            {
                PathSpline temp = EndPaths[i];
                EndPaths[i] = EndPaths[i + 1];
            }
        }
    }

    public bool HasStartPath()
    {
        return StartPaths.Count > 0;
    }
    public PathSpline NextStartPath(Vector3 point)
    {
        for (int i = 0; i < StartPaths.Count; i++)
        {
            if (point.y > StartPaths[i].GetPoint(0).y)
                return StartPaths[i];
        }
        return StartPaths[StartPaths.Count - 1];
    }
    public bool HasEndPath()
    {
        return EndPaths.Count > 0;
    }
    public PathSpline NextEndPath(Vector3 point)
    {
        for (int i = 0; i < EndPaths.Count; i++)
        {
            if (point.y > EndPaths[i].GetPoint(1).y)
                return EndPaths[i];
        }
        return EndPaths[EndPaths.Count - 1];
    }
}
