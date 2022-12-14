using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class TransitionMovement : MonoBehaviour
{
    public GridController gridController;

    public Collider2D collider1;
    public GameObject party;
    public UnitState unitState;
    public Rigidbody2D agentRB;
    public float flowFieldSpeed;
    public float maxTransitionSpeed;
    public float rotation_speed;

    private void FixedUpdate()
    {

        FlowFieldMovment();
        if (gridController != null)
        {
            if (Vector2.Distance(gridController.worldPosition, agentRB.position) > unitState.orbitLevel)
            {
                if (unitState.currentStateInt == UnitState.uState.transition || unitState.currentStateInt == UnitState.uState.inTargetRange)
                {
                    Quaternion angle = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0,
                                                    AngleBetweenVector2(agentRB.position, agentRB.position + agentRB.velocity)), rotation_speed * Time.deltaTime);
                    agentRB.MoveRotation(angle);

                }
                speedLimiter(agentRB);   
                       
            }            
        }

    }

    public void FlowFieldMovment()
    {
        
        if (gridController == null || unitState.currentStateInt == UnitState.uState.calculatingIdle) { return; }

        if (Vector2.Distance(gridController.worldPosition, agentRB.position) < unitState.orbitLevel)
        {
            gridController = null;
            gameObject.transform.parent = gameObject.transform.parent.parent;
            unitState.intarget = UnitState.uState.inTargetRange;
            return;
        }


        if (unitState.currentStateInt == UnitState.uState.transition || unitState.currentStateInt == UnitState.uState.inTargetRange)
        {
            
            Cell cellBelow = gridController.currentFlowField.GetCellFromWorldPos(agentRB.position);
            Vector2 moveDirection = new Vector2(cellBelow.bestDirection.Vector.x, cellBelow.bestDirection.Vector.y);
            agentRB.AddForce(moveDirection * flowFieldSpeed);
           
        }



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

    private void speedLimiter(Rigidbody2D agentRB)
    {
        if (Mathf.Abs(agentRB.velocity.sqrMagnitude) >= maxTransitionSpeed*maxTransitionSpeed)
        {
            agentRB.velocity = agentRB.velocity.normalized * maxTransitionSpeed;
            
        }
        
    }
}

