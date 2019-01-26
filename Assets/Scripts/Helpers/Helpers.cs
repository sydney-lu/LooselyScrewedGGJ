using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    public static List<T> CleanList<T>(List<T> list, bool removeDuplicates = true)
    {
        List<T> cleanList = new List<T>();
        for (int i = 0; i < list.Count; i++)
        {
            if (!list[i].Equals(null) && list[i] != null)
            {
                if (removeDuplicates && cleanList.Contains(list[i]))
                    continue;
                cleanList.Add(list[i]);
            }
        }
        return cleanList;
    }

    public static Transform Closest(List<Transform> list, Vector3 point)
    {
        Transform closest = list[0];
        for (int i = 0; i < list.Count; i++)
        {
            float distance = (list[i].position - point).magnitude;
            if (distance < (closest.position - point).magnitude)
                closest = list[i];
        }
        return closest;
    }

    public static T Closest<T>(List<T> list, Vector3 point) where T : MonoBehaviour
    {
        List<Transform> transforms = new List<Transform>();
        for (int i = 0; i < list.Count; i++)
            transforms.Add((list[i] as MonoBehaviour).transform);

        int closest = 0;
        for (int i = 0; i < list.Count; i++)
        {
            float distance = (transforms[i].position - point).magnitude;
            if (distance < (transforms[closest].position - point).magnitude)
                closest = i;
        }
        if (list.Count > 0)
            return list[closest];
        else return null;
    }
}
