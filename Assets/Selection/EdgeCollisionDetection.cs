using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeCollisionDetection : MonoBehaviour
{
    List<Collider2D> triggerList = new List<Collider2D>();

    private void Update() 
    {
        Debug.Log(triggerList.Count);
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        
    }



}
