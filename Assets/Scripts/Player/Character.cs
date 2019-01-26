using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 1;
    public float jumpForce = 5;

    [Header("Other")]
    public BezierSpline surfaceSpline;
    public float splineWeight;

    private Rigidbody m_rb;
    private Vector3 moveDirection;
    private Vector3 lookForward;

    private Transform model;
    private bool grounded;
    private bool flip;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        if (surfaceSpline)
            transform.position = surfaceSpline.GetPoint(splineWeight);
        model = transform.GetChild(0);

        Vector3 splinePoint = surfaceSpline.GetPoint(0);
        transform.position = splinePoint;
        transform.LookAt(splinePoint + surfaceSpline.GetDirection(splineWeight));
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float moveDistance = x * Time.deltaTime * moveSpeed / surfaceSpline.RoughLength();

        if (flip == false && moveDistance < 0)
            flip = true;
        else if (flip == true && moveDistance > 0)
            flip = false;

        bool canMove = true;
        if (flip) canMove = !isBlocked(transform.position + Vector3.up * 0.1f, -transform.forward, 0.6f) && !isBlocked(transform.position + Vector3.up * 1.3f, -transform.forward, 0.6f);
        else canMove = !isBlocked(transform.position + Vector3.up * 0.1f, transform.forward, 0.6f) && !isBlocked(transform.position + Vector3.up * 1.3f, transform.forward, 0.6f);

        Debug.Log(canMove);
        if (canMove)
        {
            Vector3 splinePoint = surfaceSpline.GetPoint(splineWeight + moveDistance);
            splineWeight += moveDistance;
            splineWeight = Mathf.Clamp01(splineWeight);

            //Position
            Vector3 currentPosition = transform.position;
            Vector3 newPosition = new Vector3(splinePoint.x, currentPosition.y, splinePoint.z);
            transform.position = newPosition;

            //Rotation
            transform.LookAt(newPosition + surfaceSpline.GetDirection(splineWeight));
        }
        if (flip) lookForward = Vector3.Lerp(lookForward, -transform.forward, Time.deltaTime * 5);
        else lookForward = Vector3.Lerp(lookForward, transform.forward, Time.deltaTime * 5);

        Quaternion lookRotation = Quaternion.LookRotation(lookForward, Vector3.up);
        model.rotation = lookRotation;

        //Vertical
        m_rb.AddForce(Physics.gravity);
        grounded = isBlocked(transform.position + Vector3.up * 0.5f, Vector3.down, 0.55f);
        if (grounded && Input.GetButton("Jump"))
            Jump();
    }

    private bool isBlocked(Vector3 start, Vector3 direction, float range)
    {
        RaycastHit hit;
        Ray ray = new Ray(start, direction);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        if (Physics.Raycast(ray, out hit, range)) 
        {
            Debug.Log(hit.collider.gameObject.name);
            return true;
        }
        return false;
    }

    private void Jump()
    {
        Debug.Log("Jump");
        grounded = false;
        m_rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}