using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ObjectSpline: MonoBehaviour
{
    [SerializeField]
    private BezierSpline spline;

    [SerializeField,Range(3,100)]
    private int frequency = 3;

    [Space(10)]
    [SerializeField]
    private Transform start;
    [SerializeField]
    private Transform mid_1;
    [SerializeField]
    private Transform mid_2;
    [SerializeField]
    private Transform end;

    public List<Transform> objects;
    public bool update;

#if UNITY_EDITOR
    private void OnEnable()
    {
        UpdateObjects();
    }

    private void Update()
    {
        UpdateObjects();
    }
#endif

    private void UpdateObjects()
    {
        if (spline)
        {
            if (objects.Count != frequency + 1 || update)
            {
                update = false;
                for (int i = 0; i < objects.Count; i++)
                    if (objects[i]) DestroyImmediate(objects[i].gameObject);

                objects = new List<Transform>();
                for (int i = 0; i < frequency + 1; i++)
                    objects.Add(null);

                if (!spline.Loop)
                {
                    setUpObject(Instantiate(start), 0);
                    setUpObject(Instantiate(end), frequency);
                    for (int i = 1; i < objects.Count - 1; i++)
                        setUpObject(Instantiate(mid_1), Instantiate(mid_2), i);
                }
                else
                {
                    for (int i = 0; i < objects.Count; i++)
                        setUpObject(Instantiate(mid_1), Instantiate(mid_2), i);
                }
            }
        }
    }

    private void setUpObject(Transform obj, int index)
    {
        Vector3 position = spline.GetPoint((float)index / frequency);
        obj.transform.position = position;
        obj.transform.LookAt(position + spline.GetDirection(index / frequency));
        obj.transform.parent = transform;
        objects[index] = obj;
    }

    private void setUpObject(Transform obj1, Transform obj2, int index)
    {
        Vector3 position = spline.GetPoint((float)index / frequency);
        obj1.transform.position = position;
        obj1.transform.LookAt(position + spline.GetDirection(index / frequency));
        obj1.transform.parent = transform;
        objects[index] = obj1;

        obj2.transform.position = position;
        obj2.transform.LookAt(position - spline.GetDirection(index / frequency));
        obj2.transform.parent = obj1;
    }
}
