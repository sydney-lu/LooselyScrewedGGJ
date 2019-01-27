using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform[] points;
    public float speed = 2;
    public bool hasPower;
    public bool useRotation;

    private bool moving = false;

    private int index = 0;
    private int nextIndex { get { return (index + 1) % points.Length; } }

    private void Update()
    {
        if (!moving && hasPower && points.Length > 0)
            StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        moving = true;
        float elapsedTime = 0;
        while (elapsedTime < 1)
        {
            elapsedTime += Time.deltaTime / speed;
            transform.position = Vector3.Lerp(points[index].position, points[nextIndex].position, elapsedTime);
            if (useRotation) transform.rotation = Quaternion.Lerp(points[index].rotation, points[nextIndex].rotation, elapsedTime);
            yield return null;
        }
        yield return new WaitForSeconds(2);
        index = nextIndex;
        moving = false;
    }

    private void OnTriggerEnter(Collider collision)
    {   
        collision.gameObject.transform.parent = transform;
    }

    private void OnTriggerExit(Collider collision)
    {
        collision.gameObject.transform.parent = null;
    }

    public void HasPower(bool power)
    {
        hasPower = power;
    }
}
