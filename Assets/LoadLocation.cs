using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLocation : MonoBehaviour
{
    public int InventoryStock;

    void Start()
    { 
    //    if (InventoryStock <= 0) InventoryStock = 1;
    }

    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        //Debug.Log("The AI is at Point A!");

        //if (InventoryStock > 0)
        //{
        //    Debug.Log("The AI took the thing from Point A!");
        //}
    }
}
