using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class FlowFieldSteer : MonoBehaviour
{
    public GridController gridController;

    public Rigidbody2D agentRB;
    public float flowFieldSpeed;

    private Vector2 velocity;

    
    public float rotation_speed;

    
    
    public void FlowFieldMovment()
    {
        
        if (gridController.currentFlowField == null) { return; }
        Cell cellBelow = gridController.currentFlowField.GetCellFromWorldPos(agentRB.position);
        Vector2 moveDirection = new Vector2(cellBelow.bestDirection.Vector.x, cellBelow.bestDirection.Vector.y);
        agentRB.AddForce(moveDirection * flowFieldSpeed);


        

    }

    public void RotateToVelocity()
    {
        agentRB.MoveRotation(AngleBetweenVector2(agentRB.position, agentRB.position + agentRB.velocity));
        // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,0,
        //                                      AngleBetweenVector2(agentRB.position, agentRB.position + agentRB.velocity)), rotation_speed * Time.deltaTime);
    }




    public float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
    Vector2 diference = vec2 - vec1;
    float sign = (vec2.y < vec1.y)? -1.0f : 1.0f;
    return (Vector2.Angle(Vector2.right, diference) * sign);
    }

}

