using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider ThisCol)
    {
        Debug.Log(ThisCol.gameObject.name + (" has entered the collision zone"));
    }

    //void OnTriggerExit(Collider ThisCol)
    //{
    //    Debug.Log(gameObject.name + (" has exited the collision zone"));
    //}
}