using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 1;
    public float jumpForce = 5;

    [Header("Other"),SerializeField]
    private PathSpline path;
    public PathSpline Path
    {
        get { return path; }
        set
        {
            if (path != value)
            {
                path = value;
                pathLength = path.RoughLength(50);
            }
        }
    }
    [Range(0,1)]
    public float splineWeight;
    private float pathLength;

    private Animator anim;
    private Rigidbody m_rb;
    private Vector3 lookForward;
    private Transform model;

    private float moveDistance;

    private Vector3 pushDirection;
    private bool pushing = false;
    private Transform pushObject;

    private bool grounded;
    private bool flip;

    private void Start()
    {
        if (path)
        {
            m_rb = GetComponent<Rigidbody>();
            anim = GetComponentInChildren<Animator>();
            if (path)
            {
                transform.position = path.GetPoint(splineWeight);
                pathLength = path.RoughLength(50);
            }
            model = transform.GetChild(0);

            Vector3 splinePoint = path.GetPoint(splineWeight);
            transform.position = splinePoint;
            transform.LookAt(splinePoint + path.GetDirection(splineWeight));
        }
        else enabled = false;
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        anim.SetFloat("Movment", Mathf.Abs(x));

        moveDistance = x * Time.deltaTime * moveSpeed / pathLength;

        if (!pushing)
        {
            if (flip == false && moveDistance < 0)
                flip = true;
            else if (flip == true && moveDistance > 0)
                flip = false;
        }

        bool canMove = true;
        if (flip) canMove = !isBlocked(transform.position + Vector3.up * 0.1f, -transform.forward, 0.8f) && !isBlocked(transform.position + Vector3.up * 2, -transform.forward, 0.8f);
        else canMove = !isBlocked(transform.position + Vector3.up * 0.1f, transform.forward, 0.8f) && !isBlocked(transform.position + Vector3.up * 2, transform.forward, 0.8f);

        if (!grounded) moveDistance *= 0.75f;
        if (pushing)
        {
            float dot = Vector3.Dot(transform.forward, pushDirection);
            anim.SetFloat("Pull", dot * x);
            moveDistance *= 0.5f;
            if (!grounded || Mathf.Abs(transform.position.y - pushObject.position.y) > 3)
                StopPushing();
        }
        
        grounded = GroundCheck(transform.position + Vector3.up * 0.5f + Vector3.forward * 0.2f, 0.55f)
            && GroundCheck(transform.position + Vector3.up * 0.5f - Vector3.forward * 0.2f, 0.55f);
            
        anim.SetBool("Grounded", grounded);
        if (canMove)
        {
            Vector3 splinePoint = path.GetPoint(splineWeight + moveDistance);
            splineWeight += moveDistance;
            if (splineWeight > 1 && path.HasEndPath())
            {
                splineWeight = 0;
                Path = path.NextEndPath(transform.position);
            }
            else if (splineWeight < 0 && path.HasStartPath())
            {
                splineWeight = 1;
                Path = path.NextStartPath(transform.position);
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
        model.localPosition = Vector3.zero;

        //Physics
        m_rb.AddForce(Physics.gravity);
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
            if (hit.transform != pushObject)
                return true;
        }
        return false;
    }

    private bool GroundCheck(Vector3 start, float range)
    {
        RaycastHit hit;
        Ray ray = new Ray(start, Vector3.down);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        if (Physics.Raycast(ray, out hit, range))
            return true;
        return false;
    }

    private void Jump()
    {
        grounded = false;
        m_rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        anim.SetTrigger("Jump");
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Pushable"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (pushing) StopPushing();
                else StartPushing(other.transform);
            }
        }
    }

    private void StartPushing(Transform other)
    {
        Debug.Log("StartPushing");
        pushDirection = transform.forward;
        pushObject = other;
        pushObject.parent = transform;
        pushing = true;
        anim.SetBool("Push", true);
    }

    private void StopPushing()
    {
        Debug.Log("StopPushing");
        pushObject.parent = null;
        pushing = false;
        anim.SetBool("Push", false);
    }
}