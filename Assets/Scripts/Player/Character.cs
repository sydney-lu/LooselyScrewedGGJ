using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 1;
    public float jumpForce = 5;

    [Header("Other")]
    public PathSpline path;

    private Rigidbody m_rb;
    private Vector3 moveDirection;
    private Vector3 lookForward;
    private Transform model;

    private float splineWeight;

    private bool grounded;
    private bool flip;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        if (path)
            transform.position = path.GetPoint(splineWeight);
        model = transform.GetChild(0);

        Vector3 splinePoint = path.GetPoint(0);
        transform.position = splinePoint;
        transform.LookAt(splinePoint + path.GetDirection(splineWeight));
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float moveDistance = x * Time.deltaTime * moveSpeed / path.RoughLength();

        if (flip == false && moveDistance < 0)
            flip = true;
        else if (flip == true && moveDistance > 0)
            flip = false;

        bool canMove = true;
        if (flip) canMove = !isBlocked(transform.position + Vector3.up * 0.1f, -transform.forward, 0.6f) && !isBlocked(transform.position + Vector3.up * 1.3f, -transform.forward, 0.6f);
        else canMove = !isBlocked(transform.position + Vector3.up * 0.1f, transform.forward, 0.6f) && !isBlocked(transform.position + Vector3.up * 1.3f, transform.forward, 0.6f);

        if (canMove)
        {
            Vector3 splinePoint = path.GetPoint(splineWeight + moveDistance);
            splineWeight += moveDistance;
            if (splineWeight > 1 && path.HasEndPath())
            {
                splineWeight = 0;
                path = path.NextEndPath(transform.position);
            }
            else if (splineWeight < 0 && path.HasStartPath())
            {
                splineWeight = 1;
                path = path.NextStartPath(transform.position);
            }
            else splineWeight = Mathf.Clamp01(splineWeight);

            //Position
            Vector3 currentPosition = transform.position;
            Vector3 newPosition = new Vector3(splinePoint.x, currentPosition.y, splinePoint.z);
            transform.position = newPosition;

            //Rotation
            transform.LookAt(newPosition + path.GetDirection(splineWeight));
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
        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.collider.gameObject.isStatic)
                return true;
        }
        return false;
    }

    private void Jump()
    {
        grounded = false;
        m_rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}