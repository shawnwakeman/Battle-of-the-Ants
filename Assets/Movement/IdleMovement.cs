using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleMovement : MonoBehaviour
{

    public bool setTrigger = false;


    public PhysicsMaterial2D inIdleMovementMaterial;
    public Vector2 closestPoint;
    public bool readyForOrbit = false;
    // public GridController gridController;

    void Update()
    {

        

    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "idlecollider")
        {
            GameObject other = collision.gameObject;
            EdgeCollider2D rigidbody = other.GetComponent<EdgeCollider2D>();
            Vector2 closestPoint = rigidbody.ClosestPoint(GetComponent<Rigidbody2D>().position);
            Debug.Log(closestPoint);

        }

    }

    //     public void orbit(float radius)
    // {

    //     if (flowFieldSteer.setInitalAngle == false)
    //     {
    //         flowFieldSteer.angle = AngleBetweenVector20(agentRB.position, gridController.worldPosition);
    //         flowFieldSteer.angle = Mathf.Abs(flowFieldSteer.angle);
    //         flowFieldSteer.setInitalAngle = true;
            


    //     }
    //     else
    //     {
    //         float x = radius * Mathf.Cos(flowFieldSteer.angle) + gridController.worldPosition.x;
    //         float y = radius * Mathf.Sin(flowFieldSteer.angle) + gridController.worldPosition.y;

    //         flowFieldSteer.angle += -1 * Time.deltaTime;

    //         // Apply the force to the Rigidbody2D
    //         agentRB.MovePosition(new Vector2(x, y));
         
    //     }


    // }
}



