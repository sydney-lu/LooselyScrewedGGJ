using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public float horizontalSpeed;
    public float verticalSpeed;
    public int LineOfSightLength;
    public int InventoryStock;

    private int DestinationPoint = 0;

    public bool jump;
    public Transform LatchLocation;
    public Transform Hand;

    public GameObject ThePackage;

    private RaycastHit hit;
    public Animator animator;
    public Transform[] PatrolPoints;
    private UnityEngine.AI.NavMeshAgent agent;
    //int layerMask = 1 << 6;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.autoBraking = false;
        PatrolNextPoint();

        if (LineOfSightLength <= 0) LineOfSightLength = 15;
        if (horizontalSpeed <= 0.0f) horizontalSpeed = 2.0f;
        if (verticalSpeed <= 0.0f) verticalSpeed = 2.0f;
        if (InventoryStock <= 0) InventoryStock = 1;
    }

    void Update()
    {
        // Updates next patrol point upon reached X distance to the current patrol point
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            PatrolNextPoint();

        DrawToLatch(); 
        // Test Animator and variables for further implementation
        jump = Input.GetButtonDown("Jump");

        animator.SetBool("bIsJump", jump);
        if (jump == true)
            Debug.Log("Animation Jump");

        // Mimic perception
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, fwd, out hit, LineOfSightLength))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                Debug.Log("The AI saw the player");
            }
            //Debug.Log(hit.collider.tag);
            // Implement Player Chase here 
        }
        else 
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * LineOfSightLength, Color.red);
    }

    void PatrolNextPoint() //AI Patrols to points
    {   // If a value is not set for number of patrol points
        if (PatrolPoints.Length == 0)
        {
            Debug.Log("Please set the patrol points by setting a value for # of Patrol Points, and the transforms for the patrol points");
            return;
        }

        // Move to the destination, and complete the rotation
        agent.destination = PatrolPoints[DestinationPoint].position;
        DestinationPoint = (DestinationPoint + 1) % PatrolPoints.Length;
        //Debug.Log("Patrolling to: " + DestinationPoint);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "LatchTarget")
        {
            Debug.Log("Latched onto an surface you are able to grip.");
            //transform.Translate(LatchLocation.transform);
        }

        if (col.gameObject.name == "PickupLocation")
        {
            Debug.Log("The AI is at Point A!");

            if (InventoryStock > 0)
            {
                Debug.Log("The AI took the thing from Point A!");
                PickUpPackage();
                --InventoryStock;
            }
        }
    }

    void DrawToLatch()
    {
        Vector3 LatchForward = LatchLocation.TransformDirection(Vector3.forward) * 10; 
        //Debug.DrawRay(LatchLocation.position, transform.position, Color.yellow);
        Debug.DrawRay(LatchLocation.position, Hand.position, Color.yellow);
    }

    void PickUpPackage()
    {
        GameObject PackageInTransport = Instantiate(ThePackage, Hand.position, Hand.rotation);
        PackageInTransport.transform.parent = GameObject.Find("PrototypeAI").transform;

        //Destroy(PackageInTransport, 1);
    }

    //void OnColliisionEnter(Collision col)
    //{
    //    Debug.Log("Successfully noted a collision");
    //    if (col.gameObject.name == "RotatingSphere")
    //    {
    //        Debug.Log("Latched onto an surface you are able to grip.");
    //    }
    //}
}
